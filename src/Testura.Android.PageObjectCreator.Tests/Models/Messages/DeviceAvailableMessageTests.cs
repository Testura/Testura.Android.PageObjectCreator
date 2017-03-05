using Testura.Android.PageObjectCreator.Models.Messages;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Models.Messages
{
    [TestFixture]
    public class DeviceAvailableMessageTests
    {
        private DeviceAvailableMessage deviceAvailableMessage;

        [SetUp]
        public void SetUp()
        {
            deviceAvailableMessage = new DeviceAvailableMessage();
        }
    }
}