using Testura.Android.PageObjectCreator.ViewModels;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.ViewModels
{
    [TestFixture]
    public class ViewModelLocatorTests
    {
        private ViewModelLocator _viewModelLocator;

        [SetUp]
        public void SetUp()
        {
            _viewModelLocator = new ViewModelLocator();
        }
    }
}