using Testura.Android.PageObjectCreator.Models.Messages;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Models.Messages
{
    [TestFixture]
    public class UiObjectInfoRemovedMessageTests
    {
        private UiObjectInfoRemovedMessage _uiObjectInfoRemovedMessage;

        [SetUp]
        public void SetUp()
        {
            _uiObjectInfoRemovedMessage = new UiObjectInfoRemovedMessage();
        }
    }
}