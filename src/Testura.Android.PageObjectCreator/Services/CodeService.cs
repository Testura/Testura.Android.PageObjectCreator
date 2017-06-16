using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Android.Device;
using Testura.Android.Device.Ui.Nodes.Data;
using Testura.Android.Device.Ui.Objects;
using Testura.Android.PageObjectCreator.Models;
using Testura.Android.Util;
using Testura.Code;
using Testura.Code.Builders;
using Testura.Code.Generators.Class;
using Testura.Code.Generators.Common;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;
using Testura.Code.Generators.Common.BinaryExpressions;
using Testura.Code.Models;
using Testura.Code.Models.References;
using Testura.Code.Saver;
using Testura.Code.Statements;
using Attribute = Testura.Code.Models.Attribute;

namespace Testura.Android.PageObjectCreator.Services
{
    public class CodeService : ICodeService
    {
        private const string DeviceName = "device";
        private readonly CodeSaver _codeSaver;

        public CodeService()
        {
            _codeSaver = new CodeSaver();
        }

        /// <summary>
        /// Generate the class, fields and constructor for a page object
        /// </summary>
        /// <param name="pageObejctName">Name of the new page object</param>
        /// <param name="namespace">Name of the namespace to generate</param>
        /// <param name="uiObjects">UiObject inside the class</param>
        /// <param name="useAttributes">If we should generate ui objects as attributes</param>
        /// <returns>The generated code as a string</returns>
        public string GeneratePageObject(string pageObejctName, string @namespace, IEnumerable<UiObjectInfo> uiObjects, bool useAttributes)
        {
            var fields = new List<Field>();
            var statements = new List<StatementSyntax>();

            fields.Add(new Field(DeviceName, typeof(IAndroidDevice), new[] { Modifiers.Private }));
            statements.Add(Statement.Declaration.Assign(new VariableReference("this", new MemberReference(DeviceName)), new VariableReference("device")));

            foreach (var pageObjectUiNode in uiObjects)
            {
                var generatedUiObject = GenerateUiObject(pageObjectUiNode, useAttributes);
                fields.Add(generatedUiObject.field);
                if (generatedUiObject.statement != null)
                {
                    statements.Add(generatedUiObject.statement);
                }
            }

            var classBuilder = new ClassBuilder(pageObejctName, @namespace)
                .WithUsings("Testura.Android.Device", "Testura.Android.Device.Ui.Objects", "Testura.Android.Device.Ui.Search")
                .WithFields(fields.ToArray())
                .WithConstructor(ConstructorGenerator.Create(
                    pageObejctName,
                    BodyGenerator.Create(statements.ToArray()),
                    modifiers: new[] { Modifiers.Public },
                    parameters: new List<Parameter> { new Parameter("device", typeof(IAndroidDevice)) }))
                .Build();
            return _codeSaver.SaveCodeAsString(classBuilder);
        }

        private(Field field, StatementSyntax statement) GenerateUiObject(UiObjectInfo pageObjectUiNode, bool useAttribute)
        {
            var attributes = new List<Attribute>();
            StatementSyntax statement = null;

            if (useAttribute && pageObjectUiNode.FindWith.Count == 1)
            {
                attributes.Add(GenerateAttribute(pageObjectUiNode));
            }
            else
            {
                statement = Statement.Declaration.Assign(
                pageObjectUiNode.Name,
                new VariableReference(DeviceName, new MemberReference("Ui", new MethodReference("CreateUiObject", GenerateWithArgument(pageObjectUiNode)))));
            }

            var field = new Field(pageObjectUiNode.Name, typeof(UiObject), new[] { Modifiers.Private }, attributes);
            return (field, statement);
        }

        private Attribute GenerateAttribute(UiObjectInfo pageObjectUiNode)
        {
            var with = pageObjectUiNode.FindWith[0];
            var name = Enum.GetName(typeof(AttributeTags), with);
            return new Attribute("Create",
                new List<IArgument>()
                {
                    new VariableArgument($"AttributeTags.{name}", "with"),
                    new ValueArgument(GetNodeValue(pageObjectUiNode.Node, with), StringType.Normal, "value")
                });
        }

        private IEnumerable<IArgument> GenerateWithArgument(UiObjectInfo uiObjectInfo)
        {
            var arguments = new List<IArgument>();
            IList<AttributeTags> withs;

            if (uiObjectInfo.AutoSelectedWith != null)
            {
                if (uiObjectInfo.AutoSelectedWith.Parent == null)
                {
                    withs = uiObjectInfo.AutoSelectedWith.Withs;
                }
                else
                {
                    return GenerateAutoSelectedWiths(uiObjectInfo.AutoSelectedWith);
                }
            }
            else
            {
                withs = uiObjectInfo.FindWith;
            }

            foreach (var with in withs)
            {
                var value = GetNodeValue(uiObjectInfo.Node, with);
                var valueArgument = with == AttributeTags.Index ? new ValueArgument(int.Parse(value)) : new ValueArgument(value);
                arguments.Add(new ReferenceArgument(new VariableReference("With", new MethodReference(with.ToString(), new[] { valueArgument }))));
            }

            return arguments;
        }

        private IEnumerable<IArgument> GenerateAutoSelectedWiths(AutoSelectedWith autoSelected)
        {
            // If it's only one parent create a simple lambda expression without block
            if (autoSelected.Parent.Parent == null)
            {
                return new List<IArgument>
                {
                    new LambdaArgument(
                        new AndBinaryExpression(
                            GetBinaryExpression(autoSelected.Parent, true, 1),
                            GetBinaryExpression(autoSelected, false, 0)).GetBinaryExpression(),
                            "n")
                };
            }

            var generatedIf = Statement.Selection.If(
                GetBinaryExpression(autoSelected, false, 0),
                BodyGenerator.Create(Statement.Jump.ReturnTrue()));

            return new List<IArgument>
            {
                new LambdaArgument(BodyGenerator.Create(GenerateParantIfs(autoSelected.Parent, generatedIf, 1), Statement.Jump.ReturnFalse()), "n")
            };
        }

        private StatementSyntax GenerateParantIfs(AutoSelectedWith autoSelectedWith, StatementSyntax blockStatement, int parentDepth)
        {
            var generatedIf = Statement.Selection.If(GetBinaryExpression(autoSelectedWith, true, parentDepth), BodyGenerator.Create(blockStatement));

            if (autoSelectedWith.Parent != null)
            {
                return GenerateParantIfs(autoSelectedWith.Parent, generatedIf, parentDepth + 1);
            }

            return generatedIf;
        }

        private IBinaryExpression GetBinaryExpression(AutoSelectedWith autoSelected, bool isParent, int parentDepth)
        {
            var binaryExpressions = new List<ConditionalBinaryExpression>();

            foreach (var attributeTagse in autoSelected.Withs)
            {
                VariableReference leftReference;
                if (isParent)
                {
                    var name = new StringBuilder();
                    for (int n = 0; n < parentDepth; n++)
                    {
                        name.Append("Parent?.");
                    }

                    name = name.Remove(name.Length - 1, 1);

                    leftReference = new VariableReference(
                        "n",
                        new MemberReference(name.ToString(), new MemberReference(Enum.GetName(typeof(AttributeTags), attributeTagse))));
                }
                else
                {
                    leftReference = new VariableReference("n", new MemberReference(Enum.GetName(typeof(AttributeTags), attributeTagse)));
                }

                binaryExpressions.Add(new ConditionalBinaryExpression(
                    leftReference,
                    new ConstantReference(GetNodeValue(autoSelected.Node, attributeTagse)),
                    ConditionalStatements.Equal));
            }

            IBinaryExpression finalBinaryExpression;

            if (binaryExpressions.Count > 1)
            {
                finalBinaryExpression = new AndBinaryExpression(binaryExpressions.First(), binaryExpressions.Last());
            }
            else
            {
                finalBinaryExpression = binaryExpressions.First();
            }

            return finalBinaryExpression;
        }

        private string GetNodeValue(Node node, AttributeTags with)
        {
            switch (with)
            {
                case AttributeTags.Text:
                    return node.Text;
                case AttributeTags.ResourceId:
                    return node.ResourceId;
                case AttributeTags.ContentDesc:
                    return node.ContentDesc;
                case AttributeTags.Class:
                    return node.Class;
                case AttributeTags.Package:
                    return node.Package;
                case AttributeTags.Index:
                    return node.Index;
                default:
                    throw new ArgumentOutOfRangeException(nameof(with), with, null);
            }
        }
    }
}