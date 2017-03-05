using Testura.Android.PageObjectCreator.Views;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Views
{
    [TestFixture]
    public class DeviceViewTests
    {
        private DeviceView deviceView;

        [SetUp]
        public void SetUp()
        {
            deviceView = new DeviceView();
        }
    }
}