using Testura.Android.PageObjectCreator.ViewModels;
using NUnit.Framework;
using Testura.Android.PageObjectCreator.Services;
using Moq;

namespace Testura.Android.PageObjectCreator.Tests.ViewModels
{
    [TestFixture]
    public class MainViewModelTests
    {
        private Mock<IDialogService> _dialogServiceMock;
        private MainViewModel _mainViewModel;

        [SetUp]
        public void SetUp()
        {
            _dialogServiceMock = new Mock<IDialogService>();
            _mainViewModel = new MainViewModel(_dialogServiceMock.Object);
        }
    }
}