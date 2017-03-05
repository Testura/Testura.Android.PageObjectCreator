using Testura.Android.PageObjectCreator.Views;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Views
{
    [TestFixture]
    public class HierarchyViewTests
    {
        private HierarchyView hierarchyView;

        [SetUp]
        public void SetUp()
        {
            hierarchyView = new HierarchyView();
        }
    }
}