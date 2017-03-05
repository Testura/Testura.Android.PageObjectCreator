using Testura.Android.PageObjectCreator.ViewModels;
using NUnit.Framework;
using Testura.Android.PageObjectCreator.Services;
using Moq;

namespace Testura.Android.PageObjectCreator.Tests.ViewModels
{
    [TestFixture]
    public class PageObjectViewModelTests
    {
        private Mock<IDialogService> dialogServiceMock;
        private PageObjectViewModel pageObjectViewModel;

        [SetUp]
        public void SetUp()
        {
            dialogServiceMock = new Mock<IDialogService>();
            pageObjectViewModel = new PageObjectViewModel(dialogServiceMock.Object);
        }
    }
}