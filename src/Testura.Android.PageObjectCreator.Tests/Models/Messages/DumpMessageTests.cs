using Testura.Android.PageObjectCreator.Models.Messages;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Models.Messages
{
    [TestFixture]
    public class DumpMessageTests
    {
        private DumpMessage dumpMessage;

        [SetUp]
        public void SetUp()
        {
            dumpMessage = new DumpMessage();
        }
    }
}