using Testura.Android.PageObjectCreator.Models.Messages;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Models.Messages
{
    [TestFixture]
    public class AddNodeMessageTests
    {
        private AddNodeMessage _addNodeMessage;

        [SetUp]
        public void SetUp()
        {
            _addNodeMessage = new AddNodeMessage();
        }
    }
}