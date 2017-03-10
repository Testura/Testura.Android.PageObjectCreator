using Testura.Android.PageObjectCreator.Views;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Views
{
    [TestFixture]
    public class HierarchyViewTests
    {
        private HierarchyView _hierarchyView;

        [SetUp]
        public void SetUp()
        {
            _hierarchyView = new HierarchyView();
        }
    }
}