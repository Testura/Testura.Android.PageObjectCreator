using Testura.Android.PageObjectCreator.ViewModels;
using NUnit.Framework;
using Testura.Android.PageObjectCreator.Services;
using Moq;

namespace Testura.Android.PageObjectCreator.Tests.ViewModels
{
    [TestFixture]
    public class DeviceViewModelTests
    {
        private Mock<IDeviceService> deviceServiceMock;
        private DeviceViewModel deviceViewModel;

        [SetUp]
        public void SetUp()
        {
            deviceServiceMock = new Mock<IDeviceService>();
            deviceViewModel = new DeviceViewModel(deviceServiceMock.Object);
        }
    }
}