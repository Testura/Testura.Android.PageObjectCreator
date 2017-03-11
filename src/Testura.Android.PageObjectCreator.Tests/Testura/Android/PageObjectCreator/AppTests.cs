using Testura.Android.PageObjectCreator;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests
{
    [TestFixture]
    public class AppTests
    {
        private App _app;

        [SetUp]
        public void SetUp()
        {
            _app = new App();
        }
    }
}