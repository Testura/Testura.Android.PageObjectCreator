using Testura.Android.PageObjectCreator.Views;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Views
{
    [TestFixture]
    public class PageObjectViewTests
    {
        private PageObjectView pageObjectView;

        [SetUp]
        public void SetUp()
        {
            pageObjectView = new PageObjectView();
        }
    }
}