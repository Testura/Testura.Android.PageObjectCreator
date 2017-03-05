using Testura.Android.PageObjectCreator.Services;
using NUnit.Framework;
using Testura.Android.PageObjectCreator.Util;
using Moq;

namespace Testura.Android.PageObjectCreator.Tests.Services
{
    [TestFixture]
    public class DumpServiceTests
    {
        private Mock<ITerminal> terminalMock;
        private DumpService dumpService;

        [SetUp]
        public void SetUp()
        {
            terminalMock = new Mock<ITerminal>();
            dumpService = new DumpService(terminalMock.Object);
        }
    }
}