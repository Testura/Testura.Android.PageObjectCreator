using Testura.Android.PageObjectCreator.Dialogs;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Dialogs
{
    [TestFixture]
    public class NameDialogTests
    {
        private NameDialog nameDialog;

        [SetUp]
        public void SetUp()
        {
            nameDialog = new NameDialog();
        }
    }
}