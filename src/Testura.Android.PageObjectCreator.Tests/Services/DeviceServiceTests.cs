using Testura.Android.PageObjectCreator.Services;
using NUnit.Framework;
using Testura.Android.PageObjectCreator.Util;
using Moq;

namespace Testura.Android.PageObjectCreator.Tests.Services
{
    [TestFixture]
    public class DeviceServiceTests
    {
        private Mock<ITerminal> _terminalMock;
        private DeviceService _deviceService;

        [SetUp]
        public void SetUp()
        {
            _terminalMock = new Mock<ITerminal>();
            _deviceService = new DeviceService(_terminalMock.Object);
        }
    }
}