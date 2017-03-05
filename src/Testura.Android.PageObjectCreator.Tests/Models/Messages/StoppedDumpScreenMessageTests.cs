using Testura.Android.PageObjectCreator.Models.Messages;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Models.Messages
{
    [TestFixture]
    public class StoppedDumpScreenMessageTests
    {
        private StoppedDumpScreenMessage stoppedDumpScreenMessage;

        [SetUp]
        public void SetUp()
        {
            stoppedDumpScreenMessage = new StoppedDumpScreenMessage();
        }
    }
}