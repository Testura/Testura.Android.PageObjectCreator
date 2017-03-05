using Testura.Android.PageObjectCreator.Models.Messages;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Models.Messages
{
    [TestFixture]
    public class AddNodeMessageTests
    {
        private AddNodeMessage addNodeMessage;

        [SetUp]
        public void SetUp()
        {
            addNodeMessage = new AddNodeMessage();
        }
    }
}