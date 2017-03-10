using Testura.Android.PageObjectCreator.Models.Messages;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Models.Messages
{
    [TestFixture]
    public class DeviceAvailableMessageTests
    {
        private DeviceAvailableMessage _deviceAvailableMessage;

        [SetUp]
        public void SetUp()
        {
            _deviceAvailableMessage = new DeviceAvailableMessage();
        }
    }
}