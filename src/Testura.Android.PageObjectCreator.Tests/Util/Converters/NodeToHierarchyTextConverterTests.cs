using Testura.Android.PageObjectCreator.Util.Converters;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Util.Converters
{
    [TestFixture]
    public class NodeToHierarchyTextConverterTests
    {
        private NodeToHierarchyTextConverter _nodeToHierarchyTextConverter;

        [SetUp]
        public void SetUp()
        {
            _nodeToHierarchyTextConverter = new NodeToHierarchyTextConverter();
        }
    }
}