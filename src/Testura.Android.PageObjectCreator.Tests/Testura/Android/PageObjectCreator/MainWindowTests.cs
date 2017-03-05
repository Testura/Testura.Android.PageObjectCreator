using Testura.Android.PageObjectCreator;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests
{
    [TestFixture]
    public class MainWindowTests
    {
        private MainWindow mainWindow;

        [SetUp]
        public void SetUp()
        {
            mainWindow = new MainWindow();
        }
    }
}