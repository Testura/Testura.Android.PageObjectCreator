using Testura.Android.PageObjectCreator.Models.Messages;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Models.Messages
{
    [TestFixture]
    public class StoppedDumpScreenMessageTests
    {
        private StoppedDumpScreenMessage _stoppedDumpScreenMessage;

        [SetUp]
        public void SetUp()
        {
            _stoppedDumpScreenMessage = new StoppedDumpScreenMessage();
        }
    }
}