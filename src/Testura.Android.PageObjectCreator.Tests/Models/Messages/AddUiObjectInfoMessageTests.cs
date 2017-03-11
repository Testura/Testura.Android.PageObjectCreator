using Testura.Android.PageObjectCreator.Models.Messages;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Models.Messages
{
    [TestFixture]
    public class AddUiObjectInfoMessageTests
    {
        private AddUiObjectInfoMessage _addUiObjectInfoMessage;

        [SetUp]
        public void SetUp()
        {
            _addUiObjectInfoMessage = new AddUiObjectInfoMessage();
        }
    }
}