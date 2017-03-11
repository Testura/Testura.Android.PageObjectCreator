using Testura.Android.PageObjectCreator.Util;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Util
{
    [TestFixture]
    public class TerminalTests
    {
        private Terminal _terminal;

        [SetUp]
        public void SetUp()
        {
            _terminal = new Terminal();
        }
    }
}