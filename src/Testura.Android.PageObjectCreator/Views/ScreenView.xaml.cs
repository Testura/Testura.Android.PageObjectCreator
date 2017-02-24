using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Testura.Android.Device.Ui.Nodes.Data;
using Testura.Android.PageObjectCreator.Models;
using Testura.Android.PageObjectCreator.ViewModels;

namespace Testura.Android.PageObjectCreator.Views
{
    /// <summary>
    /// Interaction logic for ScreenView.xaml
    /// </summary>
    public partial class ScreenView : UserControl
    {
        private readonly ScreenViewModel _viewModel;
        private readonly IDictionary<Node, UIElement> _savedNodes;
        private AndroidDumpInfo _dumpInfo;
        private BitmapImage _lastImage;
        private Rectangle _lastSelectedNodeRectangle;
        private Rectangle _lastTemporaryHierarchyNode;

        public ScreenView()
        {
            _savedNodes = new Dictionary<Node, UIElement>();
            InitializeComponent();
            _viewModel = DataContext as ScreenViewModel;
            _viewModel.LoadImage += OnLoadImage;
            _viewModel.NewTemporaryHierarchyNode += OnNewTemporaryHierarchyNode;
            _viewModel.HierarchyNodeAdded += OnHierarchyNodeAdded;
            _viewModel.NodeRemoved += OnNodeRemoved;
        }

        private void OnNodeRemoved(object sender, Node node)
        {
            if (_savedNodes.ContainsKey(node))
            {
                DeviceCanvas.Children.Remove(_savedNodes[node]);
                _savedNodes.Remove(node);
            }
        }

        private void OnHierarchyNodeAdded(object sender, Node node)
        {
            _lastTemporaryHierarchyNode.Stroke = new SolidColorBrush(Colors.Red);
            _savedNodes.Add(node, _lastTemporaryHierarchyNode);
            _lastTemporaryHierarchyNode = null;
        }

        private void OnNewTemporaryHierarchyNode(object sender, Node node)
        {
            if (_lastTemporaryHierarchyNode != null)
            {
                DeviceCanvas.Children.Remove(_lastTemporaryHierarchyNode);
            }

            CreateTemporaryRectangle(node);
            _lastTemporaryHierarchyNode = DeviceCanvas.Children[DeviceCanvas.Children.Count - 1] as Rectangle;
        }

        private void OnLoadImage(object sender, AndroidDumpInfo dumpInfo)
        {
            _savedNodes.Clear();
            _dumpInfo = dumpInfo;
            _lastImage = null;
            var image = new BitmapImage();
            using (var stream = File.OpenRead(dumpInfo.ScreenshotPath))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
            }

            _lastImage = image;
            var fixedImage = new Image
            {
                Width = 350,
                Source = _lastImage,
            };
            DeviceCanvas.Children.Add(fixedImage);
        }

        private void DeviceCanvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (DeviceCanvas.Children.Count == 0)
            {
                return;
            }

            if (_lastSelectedNodeRectangle != null && _lastSelectedNodeRectangle != _lastTemporaryHierarchyNode)
            {
                DeviceCanvas.Children.Remove(_lastSelectedNodeRectangle);
            }

            var node = MarkNode();

            if (e.RightButton == MouseButtonState.Pressed && node != null)
            {
                DeviceCanvas.Children.Remove(_lastSelectedNodeRectangle);
                _lastSelectedNodeRectangle = null;
                _viewModel.ShowNodeDetails(node);
                return;
            }

            if (e.LeftButton == MouseButtonState.Pressed && node != null)
            {
                var rec = _lastSelectedNodeRectangle;
                _lastSelectedNodeRectangle = null;
                rec.Stroke = new SolidColorBrush(Colors.Red);
                if (_viewModel.AddNode(node))
                {
                    _savedNodes.Add(node, rec);
                }
                else
                {
                    DeviceCanvas.Children.Remove(rec);
                }
            }
        }

        private Node MarkNode()
        {
            var imageScale = GetImageScale();
            var mousePosition = Mouse.GetPosition(DeviceCanvas);
            var node = _viewModel.GetNodes(new Point((int)(mousePosition.X * imageScale.XScale), (int)(mousePosition.Y * imageScale.YScale)), _dumpInfo.DumpPath);

            if (node == null)
            {
                return null;
            }

            CreateTemporaryRectangle(node);
            return node;
        }

        private ImageScale GetImageScale()
        {
            var image = DeviceCanvas.Children[0] as Image;

            var ah = image.ActualHeight;
            var aw = image.ActualWidth;

            var imageHeight = _lastImage.Height;
            var imageWidth = _lastImage.Width;

            return new ImageScale { XScale = imageWidth / aw, YScale = imageHeight / ah };
        }

        private void CreateTemporaryRectangle(Node node)
        {
            var imageScale = GetImageScale();
            var bounds = node.GetNodeBounds();

            _lastSelectedNodeRectangle = new Rectangle();
            _lastSelectedNodeRectangle.Stroke = new SolidColorBrush(Colors.Blue);
            _lastSelectedNodeRectangle.Fill = new SolidColorBrush(Colors.Transparent);
            _lastSelectedNodeRectangle.Width = (bounds[1].X - bounds[0].X) / imageScale.XScale;
            _lastSelectedNodeRectangle.Height = (bounds[1].Y - bounds[0].Y) / imageScale.YScale;
            Canvas.SetLeft(_lastSelectedNodeRectangle, bounds[0].X / imageScale.XScale);
            Canvas.SetTop(_lastSelectedNodeRectangle, bounds[0].Y / imageScale.YScale);
            DeviceCanvas.Children.Add(_lastSelectedNodeRectangle);
        }

        private void DeviceCanvas_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (_lastSelectedNodeRectangle != null)
            {
                DeviceCanvas.Children.Remove(_lastSelectedNodeRectangle);
            }
        }
    }
}