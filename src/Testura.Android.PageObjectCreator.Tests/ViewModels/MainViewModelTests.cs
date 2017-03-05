using Testura.Android.PageObjectCreator.ViewModels;
using NUnit.Framework;
using Testura.Android.PageObjectCreator.Services;
using Moq;

namespace Testura.Android.PageObjectCreator.Tests.ViewModels
{
    [TestFixture]
    public class MainViewModelTests
    {
        private Mock<IDialogService> dialogServiceMock;
        private MainViewModel mainViewModel;

        [SetUp]
        public void SetUp()
        {
            dialogServiceMock = new Mock<IDialogService>();
            mainViewModel = new MainViewModel(dialogServiceMock.Object);
        }
    }
}