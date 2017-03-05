using Testura.Android.PageObjectCreator.ViewModels;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.ViewModels
{
    [TestFixture]
    public class ViewModelLocatorTests
    {
        private ViewModelLocator viewModelLocator;

        [SetUp]
        public void SetUp()
        {
            viewModelLocator = new ViewModelLocator();
        }
    }
}