using System.Collections.Generic;
using Testura.Android.Device.Ui.Nodes.Data;
using Testura.Android.PageObjectCreator.Models;

namespace Testura.Android.PageObjectCreator.Services
{
    public interface IDialogService
    {
        /// <summary>
        /// Show the enter name dialog
        /// </summary>
        /// <returns>The wanted name</returns>
        string ShowNameDialog();

        /// <summary>
        /// Show the edit with dialog
        /// </summary>
        /// <param name="uiObjectInfo">UiObjected to edit withs for</param>
        void ShowWithDialog(UiObjectInfo uiObjectInfo, IList<Node> nodes);

        /// <summary>
        /// Show an error dialog
        /// </summary>
        /// <param name="errorMessage">Error message to show</param>
        void ShowErrorDialog(string errorMessage);

        /// <summary>
        /// Show the about dialog
        /// </summary>
        void ShowAboutDialog();
    }
}
