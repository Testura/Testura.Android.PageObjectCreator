using System.Windows;
using MahApps.Metro.Controls;

namespace Testura.Android.PageObjectCreator.Dialogs
{
    /// <summary>
    /// Interaction logic for NameDialog.xaml
    /// </summary>
    public partial class NameDialog : MetroWindow
    {
        public NameDialog()
        {
            InitializeComponent();
            TxtUiObjectName.Text = string.Empty;
        }

        public string UiObjectName { get; set; }

        private void BtnOkClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            UiObjectName = TxtUiObjectName.Text;
            Close();
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
