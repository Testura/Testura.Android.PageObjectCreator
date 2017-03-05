using Testura.Android.PageObjectCreator.Dialogs;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Dialogs
{
    [TestFixture]
    public class AboutDialogTests
    {
        private AboutDialog aboutDialog;

        [SetUp]
        public void SetUp()
        {
            aboutDialog = new AboutDialog();
        }
    }
}