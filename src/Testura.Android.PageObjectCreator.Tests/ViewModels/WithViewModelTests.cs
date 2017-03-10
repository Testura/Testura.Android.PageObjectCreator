using Moq;
using Testura.Android.PageObjectCreator.ViewModels;
using NUnit.Framework;
using Testura.Android.PageObjectCreator.Services;

namespace Testura.Android.PageObjectCreator.Tests.ViewModels
{
    [TestFixture]
    public class WithViewModelTests
    {
        private Mock<IOptimalWithService> _optimalWithServiceMock;
        private WithViewModel withViewModel;

        [SetUp]
        public void SetUp()
        {
            _optimalWithServiceMock = new Mock<IOptimalWithService>();
            withViewModel = new WithViewModel(_optimalWithServiceMock.Object);
        }
    }
}