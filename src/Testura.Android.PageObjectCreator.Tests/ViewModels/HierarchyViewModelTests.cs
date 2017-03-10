using Testura.Android.PageObjectCreator.ViewModels;
using NUnit.Framework;
using Testura.Android.PageObjectCreator.Services;
using Moq;

namespace Testura.Android.PageObjectCreator.Tests.ViewModels
{
    [TestFixture]
    public class HierarchyViewModelTests
    {
        private HierarchyViewModel _hierarchyViewModel;

        [SetUp]
        public void SetUp()
        {
            _hierarchyViewModel = new HierarchyViewModel();
        }
    }
}