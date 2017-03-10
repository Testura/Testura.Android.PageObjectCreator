using Testura.Android.PageObjectCreator.Models.Messages;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Models.Messages
{
    [TestFixture]
    public class StartedDumpScreenMessageTests
    {
        private StartedDumpScreenMessage _startedDumpScreenMessage;

        [SetUp]
        public void SetUp()
        {
            _startedDumpScreenMessage = new StartedDumpScreenMessage();
        }
    }
}