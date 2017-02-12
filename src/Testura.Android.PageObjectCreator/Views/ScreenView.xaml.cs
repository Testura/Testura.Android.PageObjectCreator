using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Testura.Android.PageObjectCreator.Models;
using Testura.Android.PageObjectCreator.ViewModels;

namespace Testura.Android.PageObjectCreator.Views
{
    /// <summary>
    /// Interaction logic for ScreenView.xaml
    /// </summary>
    public partial class ScreenView : UserControl
    {
        private readonly IList<UIElement> _savedElements;
        private AndroidDumpInfo _dumpInfo;
        private BitmapImage _lastImage;
        private Rectangle _lastTemporaryHierarchyElement;

        public ScreenView()
        {
            _savedElements = new List<UIElement>();
            InitializeComponent();
            ((ScreenViewModel) DataContext).LoadImage += OnLoadImage;
            ((ScreenViewModel) DataContext).NewTemporaryHierarchyNode += OnNewTemporaryHierarchyNode;
            ((ScreenViewModel)DataContext).HierarchyNodeAdded += HierarchyNodeAdded;
        }

        private void HierarchyNodeAdded(object sender, AndroidElement e)
        {
            _lastTemporaryHierarchyElement.Stroke = new SolidColorBrush(Colors.Red);
            _savedElements.Add(_lastTemporaryHierarchyElement);
            _lastTemporaryHierarchyElement = null; 
        }

        private void OnNewTemporaryHierarchyNode(object sender, AndroidElement element)
        {
            CreateTemporaryRectangle(element);
            if (_lastTemporaryHierarchyElement != null)
            {
                DeviceCanvas.Children.Remove(_lastTemporaryHierarchyElement);
            }
            _lastTemporaryHierarchyElement = DeviceCanvas.Children[DeviceCanvas.Children.Count - 1] as Rectangle;
        }

        private void OnLoadImage(object sender, AndroidDumpInfo dumpInfo)
        {
            _savedElements.Clear();
            _dumpInfo = dumpInfo;
            _lastImage = null;
            var image = new BitmapImage();
            using (FileStream stream = File.OpenRead(dumpInfo.ScreenshotPath))
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
            _savedElements.Add(fixedImage);
        }

        private void DeviceCanvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (DeviceCanvas.Children.Count > 0)
            {
                if (!_savedElements.Contains(DeviceCanvas.Children[DeviceCanvas.Children.Count - 1]) && DeviceCanvas.Children[DeviceCanvas.Children.Count - 1] != _lastTemporaryHierarchyElement)
                {
                    DeviceCanvas.Children.RemoveAt(DeviceCanvas.Children.Count - 1);
                }

                var element = MarkElement();
                if (e.LeftButton == MouseButtonState.Pressed && element != null)
                {
                    var rect = DeviceCanvas.Children[DeviceCanvas.Children.Count - 1] as System.Windows.Shapes.Rectangle;
                    rect.Stroke = new SolidColorBrush(Colors.Red);
                    _savedElements.Add(rect);
                    var viewModel = (ScreenViewModel) DataContext;
                    if (!viewModel.AddElement(element))
                    {
                        _savedElements.Remove(DeviceCanvas.Children[DeviceCanvas.Children.Count - 1]);
                    }
                }
            }
        }

        private AndroidElement MarkElement()
        {
            var imageScale = GetImageScale();
            var p = Mouse.GetPosition(DeviceCanvas);

            var viewModel = (ScreenViewModel) DataContext;

            var element = viewModel.GetElements(new Point((int) (p.X * imageScale.XScale), (int) (p.Y * imageScale.YScale)),
                _dumpInfo.DumpPath);

            if (element == null)
            {
                return null;
            }

            CreateTemporaryRectangle(element);
            return element;
        }

        private ImageScale GetImageScale()
        {
            var image = DeviceCanvas.Children[0] as Image;

            var ah = image.ActualHeight;
            var aw = image.ActualWidth;

            var imageHeight = _lastImage.Height;
            var imageWidth = _lastImage.Width;

         
            return new ImageScale {XScale = imageWidth / aw, YScale = imageHeight / ah};
        }

        private void CreateTemporaryRectangle(AndroidElement element)
        {
            var imageScale = GetImageScale();
            var bounds = element.GetElementBounds();

            System.Windows.Shapes.Rectangle rect;
            rect = new System.Windows.Shapes.Rectangle();
            rect.Stroke = new SolidColorBrush(Colors.Blue);
            rect.Fill = new SolidColorBrush(Colors.Transparent);
            rect.Width = (bounds[1].X - bounds[0].X) / imageScale.XScale;
            rect.Height = (bounds[1].Y - bounds[0].Y) / imageScale.YScale;
            Canvas.SetLeft(rect, bounds[0].X / imageScale.XScale);
            Canvas.SetTop(rect, bounds[0].Y / imageScale.YScale);
            DeviceCanvas.Children.Add(rect);
        }

        private void DeviceCanvas_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (DeviceCanvas.Children.Count == 0)
            {
                return;
            }

            var lastChild = DeviceCanvas.Children[DeviceCanvas.Children.Count - 1];

            if (_savedElements.Contains(lastChild))
            {
                return;
            }

            DeviceCanvas.Children.Remove(lastChild);
        }
    }
}