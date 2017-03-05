using Testura.Android.PageObjectCreator;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests
{
    [TestFixture]
    public class AppTests
    {
        private App app;

        [SetUp]
        public void SetUp()
        {
            app = new App();
        }
    }
}