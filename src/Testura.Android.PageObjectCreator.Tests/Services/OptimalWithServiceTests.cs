using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using NUnit.Framework;
using Testura.Android.Device.Ui.Nodes.Data;
using Testura.Android.PageObjectCreator.Services;
using Testura.Android.Util;

namespace Testura.Android.PageObjectCreator.Tests.Services
{
    [TestFixture]
    public class OptimalWithServiceTests
    {
        private IUniqueWithFinderService _uniqueWithFinderService;

        [SetUp]
        public void SetUp()
        {
            _uniqueWithFinderService = new UniqueWithFinderService();
        }

        [Test]
        public void GetOptimalWith_WhenOnlyHavingOneNodeAndResourceId_ShouldGetResourceId()
        {
            var node = new Node(new XElement("node", new XAttribute("resource-id", "test")), null);
            var withs = _uniqueWithFinderService.GetUniqueWiths(node, new List<Node>() { node }, false);
            Assert.AreEqual(1, withs.Withs.Count);
            Assert.AreEqual(AttributeTags.ResourceId, withs.Withs.First());
        }

        [Test]
        public void GetOptimalWith_WhenOnlyHavingOneNodeResourceIdIsEmpty_ShouldNotGetResourceId()
        {
            var node = new Node(new XElement("node", new XAttribute("resource-id", ""), new XAttribute("package", "test")), null);
            var withs = _uniqueWithFinderService.GetUniqueWiths(node, new List<Node>() { node }, false);
            Assert.AreEqual(1, withs.Withs.Count);
            Assert.AreEqual(AttributeTags.Package, withs.Withs.First());
        }

        [Test]
        public void GetOptimalWith_WhenOnlyHavingTwoWithSameResoruceId_ShouldGetPackage()
        {
            var node = new Node(new XElement("node", new XAttribute("resource-id", "test"), new XAttribute("package", "test")), null);
            var secondNode = new Node(new XElement("node", new XAttribute("resource-id", "test")), null);
            var withs = _uniqueWithFinderService.GetUniqueWiths(node, new List<Node>() { node, secondNode }, false);
            Assert.AreEqual(1, withs.Withs.Count);
            Assert.AreEqual(AttributeTags.Package, withs.Withs.First());
        }

        [Test]
        public void GetOptimalWith_WhenOnlyHavingSameResourceIdAndPackageAsTwoOther_ShouldGetCombinationResourceIdAndPackage()
        {
            var node = new Node(new XElement("node", new XAttribute("resource-id", "test"), new XAttribute("package", "test")), null);
            var secondNode = new Node(new XElement("node", new XAttribute("resource-id", "test")), null);
            var thirdNode = new Node(new XElement("node", new XAttribute("package", "test")), null);
            var withs = _uniqueWithFinderService.GetUniqueWiths(node, new List<Node>() { node, secondNode, thirdNode }, false);
            Assert.AreEqual(2, withs.Withs.Count);
            Assert.AreEqual(AttributeTags.ResourceId, withs.Withs.First());
            Assert.AreEqual(AttributeTags.Package, withs.Withs.Last());
        }

        [Test]
        public void GetOptimalWith_WhenNoUniqueValuesAndHaveToUseParent_ShouldGetParent()
        {
            var parent = new Node(new XElement("node", new XAttribute("resource-id", "hej"), new XAttribute("package", "bleh")), null);
            var node = new Node(new XElement("node", new XAttribute("resource-id", "test"), new XAttribute("package", "test")), parent);
            parent.Children.Add(node);
            var secondNode = new Node(new XElement("node", new XAttribute("package", "test"), new XAttribute("resource-id", "test")), null);
            var thirdNode = new Node(new XElement("node", new XAttribute("resource-id", "test"), new XAttribute("package", "test")), null);
            var withs = _uniqueWithFinderService.GetUniqueWiths(node, new List<Node>() { node, secondNode, thirdNode, parent }, false);
            Assert.AreEqual(1, withs.Withs.Count);
            Assert.IsNotNull(withs.Parent);
            Assert.AreEqual(AttributeTags.ResourceId, withs.Withs.First());
        }

        [Test]
        public void GetOptimalWith_WhenNoUniqueValuesAndHaveToUseParentAndParentsOtherChildrenHaveSameResourceId_ShouldGetParentAndUsePackage()
        {
            var parent = new Node(new XElement("node", new XAttribute("resource-id", "hej"), new XAttribute("package", "bleh")), null);
            var node = new Node(new XElement("node", new XAttribute("resource-id", "test"), new XAttribute("package", "test")), parent);
            var otherChildNode = new Node(new XElement("node", new XAttribute("resource-id", "test")), parent);
            parent.Children.Add(node);
            parent.Children.Add(otherChildNode);
            var secondNode = new Node(new XElement("node", new XAttribute("package", "test"), new XAttribute("resource-id", "test")), null);
            var thirdNode = new Node(new XElement("node", new XAttribute("resource-id", "test"), new XAttribute("package", "test")), null);
            var withs = _uniqueWithFinderService.GetUniqueWiths(node, new List<Node>() { node, secondNode, thirdNode, parent, otherChildNode}, false);
            Assert.AreEqual(1, withs.Withs.Count);
            Assert.IsNotNull(withs.Parent);
            Assert.AreEqual(AttributeTags.Package, withs.Withs.First());
        }
    }
}