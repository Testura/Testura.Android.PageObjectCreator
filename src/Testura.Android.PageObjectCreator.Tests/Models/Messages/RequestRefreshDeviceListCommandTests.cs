using Testura.Android.PageObjectCreator.Models.Messages;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Models.Messages
{
    [TestFixture]
    public class RequestRefreshDeviceListCommandTests
    {
        private RequestRefreshDeviceListCommand requestRefreshDeviceListCommand;

        [SetUp]
        public void SetUp()
        {
            requestRefreshDeviceListCommand = new RequestRefreshDeviceListCommand();
        }
    }
}