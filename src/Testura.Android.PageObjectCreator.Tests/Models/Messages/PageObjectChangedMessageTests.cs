using Testura.Android.PageObjectCreator.Models.Messages;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Models.Messages
{
    [TestFixture]
    public class PageObjectChangedMessageTests
    {
        private PageObjectChangedMessage _pageObjectChangedMessage;

        [SetUp]
        public void SetUp()
        {
            _pageObjectChangedMessage = new PageObjectChangedMessage();
        }
    }
}