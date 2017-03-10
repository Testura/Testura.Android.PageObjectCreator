using Testura.Android.PageObjectCreator.Models.Messages;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Models.Messages
{
    [TestFixture]
    public class SelectedDeviceChangedMessageTests
    {
        private SelectedDeviceChangedMessage _selectedDeviceChangedMessage;

        [SetUp]
        public void SetUp()
        {
            _selectedDeviceChangedMessage = new SelectedDeviceChangedMessage();
        }
    }
}