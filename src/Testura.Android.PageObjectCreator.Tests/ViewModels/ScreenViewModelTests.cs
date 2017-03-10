using Testura.Android.PageObjectCreator.ViewModels;
using NUnit.Framework;
using Testura.Android.PageObjectCreator.Services;
using Moq;

namespace Testura.Android.PageObjectCreator.Tests.ViewModels
{
    [TestFixture]
    public class ScreenViewModelTests
    {
        private Mock<IScreenService> _screenServiceMock;
        private Mock<IDialogService> _dialogServiceMock;
        private Mock<IAutoSelectedWithFinderService> _optimalWithServiceMock; 
        private ScreenViewModel _screenViewModel;

        [SetUp]
        public void SetUp()
        {
            _screenServiceMock = new Mock<IScreenService>();
            _dialogServiceMock = new Mock<IDialogService>();
            _optimalWithServiceMock = new Mock<IAutoSelectedWithFinderService>();
            _screenViewModel = new ScreenViewModel(_screenServiceMock.Object, _dialogServiceMock.Object, _optimalWithServiceMock.Object);
        }
    }
}