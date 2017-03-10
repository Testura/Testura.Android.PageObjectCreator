using Moq;
using Testura.Android.PageObjectCreator.ViewModels;
using NUnit.Framework;
using Testura.Android.PageObjectCreator.Services;

namespace Testura.Android.PageObjectCreator.Tests.ViewModels
{
    [TestFixture]
    public class WithViewModelTests
    {
        private Mock<IAutoSelectedWithFinderService> _optimalWithServiceMock;
        private WithViewModel _withViewModel;

        [SetUp]
        public void SetUp()
        {
            _optimalWithServiceMock = new Mock<IAutoSelectedWithFinderService>();
            _withViewModel = new WithViewModel(_optimalWithServiceMock.Object);
        }
    }
}