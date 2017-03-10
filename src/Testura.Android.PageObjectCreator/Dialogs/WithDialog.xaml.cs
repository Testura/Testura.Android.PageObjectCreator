using System;
using System.Collections.Generic;
using MahApps.Metro.Controls;
using Testura.Android.Device.Ui.Nodes.Data;
using Testura.Android.PageObjectCreator.Models;
using Testura.Android.PageObjectCreator.ViewModels;

namespace Testura.Android.PageObjectCreator.Dialogs
{
    /// <summary>
    /// Interaction logic for NameDialog.xaml
    /// </summary>
    public partial class WithDialog : MetroWindow
    {
        public WithDialog(UiObjectInfo uiObjectInfo, IList<Node> nodes)
        {
            InitializeComponent();
            var viewModel = DataContext as WithViewModel;
            viewModel.SetCurrentUiObjectInfo(uiObjectInfo, nodes);
            viewModel.CloseWindow += CloseWindow;
        }

        private void CloseWindow(object sender, EventArgs eventArgs)
        {
            var viewModel = DataContext as WithViewModel;
            viewModel.CloseWindow -= CloseWindow;
            Close();
        }
    }
}
