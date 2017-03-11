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
        private UiObjectInfo _uiObjectInfo;
        private WithDialog _withDialog;

        [SetUp]
        public void SetUp()
        {
            _uiObjectInfo = new UiObjectInfo();
            _withDialog = new WithDialog(_uiObjectInfo, new List<Node>());
        }
    }
}