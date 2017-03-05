using Testura.Android.PageObjectCreator.Models.Messages;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Models.Messages
{
    [TestFixture]
    public class SelectedHierarchyNodeMesssageTests
    {
        private SelectedHierarchyNodeMesssage selectedHierarchyNodeMesssage;

        [SetUp]
        public void SetUp()
        {
            selectedHierarchyNodeMesssage = new SelectedHierarchyNodeMesssage();
        }
    }
}