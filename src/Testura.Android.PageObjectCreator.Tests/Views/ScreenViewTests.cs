using Testura.Android.PageObjectCreator.Views;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Views
{
    [TestFixture]
    public class ScreenViewTests
    {
        private ScreenView _screenView;

        [SetUp]
        public void SetUp()
        {
            _screenView = new ScreenView();
        }
    }
}