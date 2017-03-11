using System.Windows;
using MahApps.Metro.Controls;

namespace Testura.Android.PageObjectCreator.Dialogs
{
    /// <summary>
    /// Interaction logic for ClearDialog.xaml
    /// </summary>
    public partial class ClearDialog : MetroWindow
    {
        public ClearDialog()
        {
            InitializeComponent();
        }

        private void YesButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void NoButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
