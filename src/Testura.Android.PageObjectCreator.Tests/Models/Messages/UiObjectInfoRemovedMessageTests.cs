using Testura.Android.PageObjectCreator.Models.Messages;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Models.Messages
{
    [TestFixture]
    public class UiObjectInfoRemovedMessageTests
    {
        private UiObjectInfoRemovedMessage uiObjectInfoRemovedMessage;

        [SetUp]
        public void SetUp()
        {
            uiObjectInfoRemovedMessage = new UiObjectInfoRemovedMessage();
        }
    }
}