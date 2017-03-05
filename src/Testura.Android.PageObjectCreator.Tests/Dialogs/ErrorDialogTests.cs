using Testura.Android.PageObjectCreator.Dialogs;
using NUnit.Framework;
using System;

namespace Testura.Android.PageObjectCreator.Tests.Dialogs
{
    [TestFixture]
    public class ErrorDialogTests
    {
        private ErrorDialog errorDialog;

        [SetUp]
        public void SetUp()
        {
            errorDialog = new ErrorDialog(string.Empty);
        }
    }
}