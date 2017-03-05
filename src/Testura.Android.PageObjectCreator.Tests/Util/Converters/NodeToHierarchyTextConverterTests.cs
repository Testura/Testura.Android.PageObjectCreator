using Testura.Android.PageObjectCreator.Util.Converters;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Util.Converters
{
    [TestFixture]
    public class NodeToHierarchyTextConverterTests
    {
        private NodeToHierarchyTextConverter nodeToHierarchyTextConverter;

        [SetUp]
        public void SetUp()
        {
            nodeToHierarchyTextConverter = new NodeToHierarchyTextConverter();
        }
    }
}