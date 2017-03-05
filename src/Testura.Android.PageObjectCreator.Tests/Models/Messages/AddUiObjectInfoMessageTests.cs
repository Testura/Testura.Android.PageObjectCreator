using Testura.Android.PageObjectCreator.Models.Messages;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Models.Messages
{
    [TestFixture]
    public class AddUiObjectInfoMessageTests
    {
        private AddUiObjectInfoMessage addUiObjectInfoMessage;

        [SetUp]
        public void SetUp()
        {
            addUiObjectInfoMessage = new AddUiObjectInfoMessage();
        }
    }
}