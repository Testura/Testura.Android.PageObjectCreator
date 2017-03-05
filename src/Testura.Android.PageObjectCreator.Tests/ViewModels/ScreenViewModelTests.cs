using Testura.Android.PageObjectCreator.ViewModels;
using NUnit.Framework;
using Testura.Android.PageObjectCreator.Services;
using Moq;

namespace Testura.Android.PageObjectCreator.Tests.ViewModels
{
    [TestFixture]
    public class ScreenViewModelTests
    {
        private Mock<IFileService> fileServiceMock;
        private Mock<IScreenService> screenServiceMock;
        private Mock<IDialogService> dialogServiceMock;
        private ScreenViewModel screenViewModel;

        [SetUp]
        public void SetUp()
        {
            fileServiceMock = new Mock<IFileService>();
            screenServiceMock = new Mock<IScreenService>();
            dialogServiceMock = new Mock<IDialogService>();
            screenViewModel = new ScreenViewModel(fileServiceMock.Object, screenServiceMock.Object, dialogServiceMock.Object);
        }
    }
}