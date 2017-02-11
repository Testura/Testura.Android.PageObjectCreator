using System.Windows;
using MahApps.Metro.Controls;

namespace Testura.Android.PageObjectCreator.Dialogs
{
    /// <summary>
    /// Interaction logic for ErrorDialog.xaml
    /// </summary>
    public partial class ErrorDialog : MetroWindow
    {
        public ErrorDialog(string errorMessage)
        {
            InitializeComponent();
            ErrorMessage.Content = errorMessage;
        }

        private void OnOkClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
