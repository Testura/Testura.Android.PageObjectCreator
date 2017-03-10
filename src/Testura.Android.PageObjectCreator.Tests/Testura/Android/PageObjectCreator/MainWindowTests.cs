using Testura.Android.PageObjectCreator;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests
{
    [TestFixture]
    public class MainWindowTests
    {
        private MainWindow _mainWindow;

        [SetUp]
        public void SetUp()
        {
            _mainWindow = new MainWindow();
        }
    }
}