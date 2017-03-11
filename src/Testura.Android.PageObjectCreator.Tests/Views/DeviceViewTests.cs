using Testura.Android.PageObjectCreator.Views;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Views
{
    [TestFixture]
    public class DeviceViewTests
    {
        private DeviceView _deviceView;

        [SetUp]
        public void SetUp()
        {
            _deviceView = new DeviceView();
        }
    }
}