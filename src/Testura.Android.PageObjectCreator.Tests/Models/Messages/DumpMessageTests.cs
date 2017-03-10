using Testura.Android.PageObjectCreator.Models.Messages;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Models.Messages
{
    [TestFixture]
    public class DumpMessageTests
    {
        private DumpMessage _dumpMessage;

        [SetUp]
        public void SetUp()
        {
            _dumpMessage = new DumpMessage();
        }
    }
}