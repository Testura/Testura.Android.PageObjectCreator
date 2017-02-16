using System;
using System.Globalization;
using System.Windows.Data;
using Testura.Android.Device.Ui.Nodes.Data;

namespace Testura.Android.PageObjectCreator.Util.Converters
{
    public class NodeToHierarchyTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var node = (Node) value;

            if (!string.IsNullOrEmpty(node.Text))
            {
                return $"({node.Index}){node.Class}: {node.Text}";
            }
            else if (!string.IsNullOrEmpty(node.ContentDesc))
            {
                return $"({node.Index}){node.Class}: {{{node.ContentDesc}}}";
            }

            return $"({node.Index}){node.Class}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
