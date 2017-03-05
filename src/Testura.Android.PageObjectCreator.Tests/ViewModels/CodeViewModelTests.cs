using Testura.Android.PageObjectCreator.ViewModels;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.ViewModels
{
    [TestFixture]
    public class CodeViewModelTests
    {
        private CodeViewModel codeViewModel;

        [SetUp]
        public void SetUp()
        {
            codeViewModel = new CodeViewModel();
        }
    }
}