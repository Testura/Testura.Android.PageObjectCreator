using Testura.Android.PageObjectCreator.Dialogs;
using Testura.Android.PageObjectCreator.Models;

namespace Testura.Android.PageObjectCreator.Services
{
    public class DialogService : IDialogService
    {
        /// <summary>
        /// Show the enter name dialog
        /// </summary>
        /// <returns>The wanted name</returns>
        public string ShowNameDialog()
        {
            var dialog = new NameDialog();
            return dialog.ShowDialog().Value ? dialog.UiObjectName : null;
        }

        /// <summary>
        /// Show the edit with dialog
        /// </summary>
        /// <param name="uiObjectInfo">UiObjected to edit withs for</param>
        public void ShowWithDialog(UiObjectInfo uiObjectInfo)
        {
            var dialog = new WithDialog(uiObjectInfo);
            dialog.ShowDialog();
        }

        /// <summary>
        /// Show an error dialog
        /// </summary>
        /// <param name="errorMessage">Error message to show</param>
        public void ShowErrorDialog(string errorMessage)
        {
            var dialog = new ErrorDialog(errorMessage);
            dialog.ShowDialog();
        }

        /// <summary>
        /// Show the about dialog
        /// </summary>
        public void ShowAboutDialog()
        {
           var dialog = new AboutDialog();
            dialog.ShowDialog();
        }
    }
}
