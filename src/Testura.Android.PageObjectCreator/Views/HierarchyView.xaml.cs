using System.Windows;
using System.Windows.Controls;

namespace Testura.Android.PageObjectCreator.Views
{
    /// <summary>
    /// Interaction logic for XmlView.xaml
    /// </summary>
    public partial class HierarchyView : UserControl
    {
        public HierarchyView()
        {
            InitializeComponent();
        }

        private void TreeViewSelectedItemChanged(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = sender as TreeViewItem;
            if (item != null)
            {
                item.BringIntoView();
                e.Handled = true;
            }
        }
    }
}
