using Testura.Android.PageObjectCreator.Views;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Views
{
    [TestFixture]
    public class PageObjectViewTests
    {
        private PageObjectView _pageObjectView;

        [SetUp]
        public void SetUp()
        {
            _pageObjectView = new PageObjectView();
        }
    }
}