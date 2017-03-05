using Testura.Android.PageObjectCreator.Models.Messages;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Models.Messages
{
    [TestFixture]
    public class RequestDumpMessageTests
    {
        private RequestDumpMessage requestDumpMessage;

        [SetUp]
        public void SetUp()
        {
            requestDumpMessage = new RequestDumpMessage();
        }
    }
}