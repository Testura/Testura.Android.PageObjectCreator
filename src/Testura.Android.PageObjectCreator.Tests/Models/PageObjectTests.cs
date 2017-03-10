using Testura.Android.PageObjectCreator.Models;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Models
{
    [TestFixture]
    public class PageObjectTests
    {
        private PageObject _pageObject;

        [SetUp]
        public void SetUp()
        {
            _pageObject = new PageObject();
        }
    }
}