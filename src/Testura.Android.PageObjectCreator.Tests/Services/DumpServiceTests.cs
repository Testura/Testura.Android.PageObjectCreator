using Testura.Android.PageObjectCreator.Services;
using NUnit.Framework;
using Testura.Android.PageObjectCreator.Util;
using Moq;

namespace Testura.Android.PageObjectCreator.Tests.Services
{
    [TestFixture]
    public class DumpServiceTests
    {
        private Mock<ITerminal> _terminalMock;
        private DumpService _dumpService;

        [SetUp]
        public void SetUp()
        {
            _terminalMock = new Mock<ITerminal>();
            _dumpService = new DumpService(_terminalMock.Object);
        }
    }
}