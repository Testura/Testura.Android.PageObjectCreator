using Testura.Android.PageObjectCreator.Models.Messages;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Models.Messages
{
    [TestFixture]
    public class RequestRefreshDeviceListCommandTests
    {
        private RequestRefreshDeviceListCommand _requestRefreshDeviceListCommand;

        [SetUp]
        public void SetUp()
        {
            _requestRefreshDeviceListCommand = new RequestRefreshDeviceListCommand();
        }
    }
}