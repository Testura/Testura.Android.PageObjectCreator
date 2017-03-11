using Testura.Android.PageObjectCreator.Views;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Views
{
    [TestFixture]
    public class CodeViewTests
    {
        private CodeView _codeView;

        [SetUp]
        public void SetUp()
        {
            _codeView = new CodeView();
        }
    }
}