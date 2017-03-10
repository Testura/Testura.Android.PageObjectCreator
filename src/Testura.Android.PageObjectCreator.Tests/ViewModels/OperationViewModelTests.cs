using Testura.Android.PageObjectCreator.ViewModels;
using NUnit.Framework;
using Testura.Android.PageObjectCreator.Services;
using Moq;

namespace Testura.Android.PageObjectCreator.Tests.ViewModels
{
    [TestFixture]
    public class OperationViewModelTests
    {
        private Mock<IDumpService> _dumpServiceMock;
        private Mock<IFileService> _fileServiceMock;
        private Mock<IDialogService> _dialogServiceMock;
        private OperationViewModel _operationViewModel;

        [SetUp]
        public void SetUp()
        {
            _dumpServiceMock = new Mock<IDumpService>();
            _dialogServiceMock = new Mock<IDialogService>();
            _fileServiceMock = new Mock<IFileService>();
            _operationViewModel = new OperationViewModel(_dumpServiceMock.Object, _fileServiceMock.Object, _dialogServiceMock.Object);
        }
    }
}