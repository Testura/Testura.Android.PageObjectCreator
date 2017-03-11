using Testura.Android.PageObjectCreator.Dialogs;
using NUnit.Framework;
using System;

namespace Testura.Android.PageObjectCreator.Tests.Dialogs
{
    [TestFixture]
    public class ErrorDialogTests
    {
        private ErrorDialog _errorDialog;

        [SetUp]
        public void SetUp()
        {
            _errorDialog = new ErrorDialog(string.Empty);
        }
    }
}