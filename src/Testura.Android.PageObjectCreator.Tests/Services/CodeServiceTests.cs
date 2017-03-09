﻿using System.Collections.Generic;
using System.Xml.Linq;
using NUnit.Framework;
using Testura.Android.Device.Ui.Nodes.Data;
using Testura.Android.PageObjectCreator.Models;
using Testura.Android.PageObjectCreator.Services;
using Testura.Android.Util;

namespace Testura.Android.PageObjectCreator.Tests.Services
{
    [TestFixture]
    public class CodeServiceTests
    {
        private ICodeService _codeService;

        [SetUp]
        public void SetUp()
        {
            _codeService = new CodeService();
        }

        [Test]
        public void GeneratePageObject_WhenHavingUiObjectWithTwoAttributes_ShouldGenerateCorrectAssignStatement()
        {
            var pageObjectInfo = new UiObjectInfo
            {
                Name = "myObject",
                Node = new Node(new XElement("node", new XAttribute("class", "myClass"), new XAttribute("resource-id", "myResourceId")), null),
                FindWith = new List<AttributeTags>
                {
                    AttributeTags.Class,
                    AttributeTags.ResourceId
                }
            };

            var code = _codeService.GeneratePageObject("test", "test", new List<UiObjectInfo> {pageObjectInfo});
            Assert.IsTrue(code.Contains("CreateUiObject(With.Class(\"myClass\"), With.ResourceId(\"myResourceId\"));"));
        }

        [Test]
        public void GeneratePageObject_WhenHavingUiObjectWithOptimalThatJustHaveAttributes_ShouldGenerateCorrectAssignStatement()
        {
            var pageObjectInfo = new UiObjectInfo
            {
                Name = "myObject",
                Node = new Node(new XElement("node", new XAttribute("class", "myClass"), new XAttribute("resource-id", "myResourceId")), null),
                Optimal = new OptimalWith { Withs = new List<AttributeTags>
                {
                    AttributeTags.Class,
                    AttributeTags.ResourceId
                }
                }
            };

            var code = _codeService.GeneratePageObject("test", "test", new List<UiObjectInfo> { pageObjectInfo });
            Assert.IsTrue(code.Contains("CreateUiObject(With.Class(\"myClass\"), With.ResourceId(\"myResourceId\"));"));
        }

        [Test]
        public void GeneratePageObject_WhenHavingUiObjectWithOptimalThatHaveParent_ShouldGenerateCorrectAssignStatement()
        {
            var parentNode = new Node(new XElement("node", new XAttribute("class", "myClass"), new XAttribute("package", "package")), null);
            var mainNode = new Node(new XElement("node", new XAttribute("class", "myClass"), new XAttribute("resource-id", "myResourceId")), parentNode);
            

            var pageObjectInfo = new UiObjectInfo
            {
                Name = "myObject",
                Node = new Node(new XElement("node", new XAttribute("class", "myClass"), new XAttribute("resource-id", "myResourceId")), null),
                Optimal = new OptimalWith
                {
                    Node = mainNode,
                    Withs = new List<AttributeTags>
                {
                    AttributeTags.Class,
                    AttributeTags.ResourceId
                },
                    Parent = new OptimalWith
                    {
                        Node = parentNode,
                        Withs = new List<AttributeTags>
                        {
                            AttributeTags.Package
                        }
                    }
                }
            };

            var code = _codeService.GeneratePageObject("test", "test", new List<UiObjectInfo> { pageObjectInfo });
            Assert.IsTrue(code.Contains("CreateUiObject(With.Class(\"myClass\"), With.ResourceId(\"myResourceId\"));"));
        }

        [Test]
        public void GeneratePageObject_WhenHavingUiObjectWithOptimalThatHaveTwoParent_ShouldGenerateCorrectAssignStatement()
        {
            var superParentNode = new Node(new XElement("node", new XAttribute("class", "oldi"), new XAttribute("package", "package")), null);
            var parentNode = new Node(new XElement("node", new XAttribute("class", "myClass"), new XAttribute("package", "package")), superParentNode);
            var mainNode = new Node(new XElement("node", new XAttribute("class", "myClass"), new XAttribute("resource-id", "myResourceId")), parentNode);


            var pageObjectInfo = new UiObjectInfo
            {
                Name = "myObject",
                Node = new Node(new XElement("node", new XAttribute("class", "myClass"), new XAttribute("resource-id", "myResourceId")), null),
                Optimal = new OptimalWith
                {
                    Node = mainNode,
                    Withs = new List<AttributeTags>
                {
                    AttributeTags.Class,
                    AttributeTags.ResourceId
                },
                    Parent = new OptimalWith
                    {
                        Node = parentNode,
                        Withs = new List<AttributeTags>
                        {
                            AttributeTags.Package
                        },
                        Parent = new OptimalWith
                        {
                            Node = superParentNode,
                            Withs = new List<AttributeTags>
                            {
                                AttributeTags.Class
                            }
                        }
                    }
                }
            };

            var code = _codeService.GeneratePageObject("test", "test", new List<UiObjectInfo> { pageObjectInfo });
            Assert.IsTrue(code.Contains("CreateUiObject(With.Class(\"myClass\"), With.ResourceId(\"myResourceId\"));"));
        }
    }
}