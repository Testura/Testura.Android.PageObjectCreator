using Testura.Android.PageObjectCreator.Services;
using NUnit.Framework;
using Moq;

namespace Testura.Android.PageObjectCreator.Tests.Services
{
    [TestFixture]
    public class ScreenServiceTests
    {
        private Mock<IDumpService> dumpServiceMock;
        private ScreenService screenService;

        [SetUp]
        public void SetUp()
        {
            dumpServiceMock = new Mock<IDumpService>();
            screenService = new ScreenService(dumpServiceMock.Object);
        }
    }
}