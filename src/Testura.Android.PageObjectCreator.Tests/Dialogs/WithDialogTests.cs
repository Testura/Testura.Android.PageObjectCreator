using System.Collections.Generic;
using Testura.Android.PageObjectCreator.Dialogs;
using NUnit.Framework;
using Testura.Android.Device.Ui.Nodes.Data;
using Testura.Android.PageObjectCreator.Models;

namespace Testura.Android.PageObjectCreator.Tests.Dialogs
{
    [TestFixture]
    public class WithDialogTests
    {
        private UiObjectInfo uiObjectInfo;
        private WithDialog withDialog;

        [SetUp]
        public void SetUp()
        {
            withDialog = new WithDialog(null, new List<Node>());
        }
    }
}