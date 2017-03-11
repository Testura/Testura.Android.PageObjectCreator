using Moq;
using Testura.Android.PageObjectCreator.ViewModels;
using NUnit.Framework;
using Testura.Android.PageObjectCreator.Services;

namespace Testura.Android.PageObjectCreator.Tests.ViewModels
{
    [TestFixture]
    public class CodeViewModelTests
    {
        private Mock<ICodeService> _codeServiceMock;
        private CodeViewModel _codeViewModel;

        [SetUp]
        public void SetUp()
        {
            _codeServiceMock = new Mock<ICodeService>();
            _codeViewModel = new CodeViewModel(_codeServiceMock.Object);
        }
    }
}