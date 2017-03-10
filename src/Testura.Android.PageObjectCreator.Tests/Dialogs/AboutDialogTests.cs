using Testura.Android.PageObjectCreator.Dialogs;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Dialogs
{
    [TestFixture]
    public class AboutDialogTests
    {
        private AboutDialog _aboutDialog;

        [SetUp]
        public void SetUp()
        {
            _aboutDialog = new AboutDialog();
        }
    }
}