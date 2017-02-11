using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Testura.Android.PageObjectCreator.Models;
using Testura.Android.PageObjectCreator.ViewModels;

namespace Testura.Android.PageObjectCreator.Views
{
    /// <summary>
    /// Interaction logic for ScreenView.xaml
    /// </summary>
    public partial class ScreenView : UserControl
    {
        private AndroidDumpInfo _dumpInfo;
        private BitmapImage _lastImage;
        private IList<UIElement> _safeList;

        public ScreenView()
        {
            _safeList = new List<UIElement>();
            InitializeComponent();
            ((ScreenViewModel)DataContext).LoadImage += OnLoadImage;
        }

        private void OnLoadImage(object sender, AndroidDumpInfo dumpInfo)
        {
            _safeList.Clear();
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
            _safeList.Add(fixedImage);
        }

        private void DeviceCanvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (DeviceCanvas.Children.Count > 0)
            {
                if (!_safeList.Contains(DeviceCanvas.Children[DeviceCanvas.Children.Count - 1]))
                {
                    DeviceCanvas.Children.RemoveAt(DeviceCanvas.Children.Count - 1);
                }

                var element = MarkElement();
                if (e.LeftButton == MouseButtonState.Pressed && element != null)
                {
                    _safeList.Add(DeviceCanvas.Children[DeviceCanvas.Children.Count - 1]);
                    var viewModel = (ScreenViewModel)DataContext;
                    if (!viewModel.AddElement(element))
                    {
                        _safeList.Remove(DeviceCanvas.Children[DeviceCanvas.Children.Count - 1]);
                    }
                }
            }
        }

        private AndroidElement MarkElement()
        {
            var image = DeviceCanvas.Children[0] as Image;

            var ah = image.ActualHeight;
            var aw = image.ActualWidth;

            var imageHeight = _lastImage.Height;
            var imageWidth = _lastImage.Width;

            var p = Mouse.GetPosition(DeviceCanvas);

            var xScale = imageWidth / aw;
            var yScale = imageHeight / ah;

            var viewModel = (ScreenViewModel)DataContext;

            var element = viewModel.GetElements(new Point((int)(p.X * xScale), (int)(p.Y * yScale)), _dumpInfo.DumpPath);

            if (element == null)
            {
                return null;
            }

            var bounds = element.GetElementBounds();

            System.Windows.Shapes.Rectangle rect;
            rect = new System.Windows.Shapes.Rectangle();
            rect.Stroke = new SolidColorBrush(Colors.Red);
            rect.Fill = new SolidColorBrush(Colors.Transparent);
            rect.Width = (bounds[1].X - bounds[0].X) / xScale;
            rect.Height = (bounds[1].Y - bounds[0].Y) / yScale;
            Canvas.SetLeft(rect, bounds[0].X / xScale);
            Canvas.SetTop(rect, bounds[0].Y / yScale);
            DeviceCanvas.Children.Add(rect);
            return element;
        }

        private void DeviceCanvas_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (DeviceCanvas.Children.Count == 0)
            {
                return;
            }

            var lastChild = DeviceCanvas.Children[DeviceCanvas.Children.Count - 1];

            if (_safeList.Contains(lastChild))
            {
                return;
            }

            DeviceCanvas.Children.Remove(lastChild);
        }
    }
}
