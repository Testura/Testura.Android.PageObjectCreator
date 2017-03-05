using Testura.Android.PageObjectCreator.Services;
using NUnit.Framework;
using Testura.Android.PageObjectCreator.Util;
using Moq;

namespace Testura.Android.PageObjectCreator.Tests.Services
{
    [TestFixture]
    public class DeviceServiceTests
    {
        private Mock<ITerminal> terminalMock;
        private DeviceService deviceService;

        [SetUp]
        public void SetUp()
        {
            terminalMock = new Mock<ITerminal>();
            deviceService = new DeviceService(terminalMock.Object);
        }
    }
}