using Testura.Android.PageObjectCreator.ViewModels;
using NUnit.Framework;
using Testura.Android.PageObjectCreator.Services;
using Moq;

namespace Testura.Android.PageObjectCreator.Tests.ViewModels
{
    [TestFixture]
    public class OperationViewModelTests
    {
        private Mock<IDumpService> dumpServiceMock;
        private Mock<IDialogService> dialogServiceMock;
        private OperationViewModel operationViewModel;

        [SetUp]
        public void SetUp()
        {
            dumpServiceMock = new Mock<IDumpService>();
            dialogServiceMock = new Mock<IDialogService>();
            operationViewModel = new OperationViewModel(dumpServiceMock.Object, dialogServiceMock.Object);
        }
    }
}