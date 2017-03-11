using Testura.Android.PageObjectCreator.ViewModels;
using NUnit.Framework;
using Testura.Android.PageObjectCreator.Services;
using Moq;

namespace Testura.Android.PageObjectCreator.Tests.ViewModels
{
    [TestFixture]
    public class DeviceViewModelTests
    {
        private Mock<IDeviceService> _deviceServiceMock;
        private DeviceViewModel _deviceViewModel;

        [SetUp]
        public void SetUp()
        {
            _deviceServiceMock = new Mock<IDeviceService>();
            _deviceViewModel = new DeviceViewModel(_deviceServiceMock.Object);
        }
    }
}