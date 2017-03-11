using Testura.Android.PageObjectCreator.Dialogs;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Dialogs
{
    [TestFixture]
    public class NameDialogTests
    {
        private NameDialog _nameDialog;

        [SetUp]
        public void SetUp()
        {
            _nameDialog = new NameDialog();
        }
    }
}