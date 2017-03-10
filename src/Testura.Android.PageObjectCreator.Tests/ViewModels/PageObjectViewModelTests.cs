using Testura.Android.PageObjectCreator.ViewModels;
using NUnit.Framework;
using Testura.Android.PageObjectCreator.Services;
using Moq;

namespace Testura.Android.PageObjectCreator.Tests.ViewModels
{
    [TestFixture]
    public class PageObjectViewModelTests
    {
        private Mock<IDialogService> _dialogServiceMock;
        private PageObjectViewModel _pageObjectViewModel;

        [SetUp]
        public void SetUp()
        {
            _dialogServiceMock = new Mock<IDialogService>();
            _pageObjectViewModel = new PageObjectViewModel(_dialogServiceMock.Object);
        }
    }
}