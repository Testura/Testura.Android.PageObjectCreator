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

namespace Testura.Android.PageObjectCreator.Services
{
    public class CodeService : ICodeService
    {
        private const string DeviceName = "device";
        private CodeSaver _codeSaver;

        public CodeService()
        {
            _codeSaver = new CodeSaver();
        }

        public string GeneratePageObject(string pageObejctName, string @namespace, IEnumerable<UiObjectInfo> uiObjects)
        {
            var fields = new List<Field>();
            var statements = new List<StatementSyntax>();

            fields.Add(new Field(DeviceName, typeof(IAndroidDevice), new[] {Modifiers.Private}));
            statements.Add(Statement.Declaration.Assign(new VariableReference("this", new MemberReference(DeviceName)),
                new VariableReference("device")));

            foreach (var pageObjectUiNode in uiObjects)
            {
                GenerateUiObject(fields, statements, pageObjectUiNode);
            }

            var classBuilder = new ClassBuilder(pageObejctName, @namespace)
                .WithUsings("Testura.Android.Device", "Testura.Android.Device.Ui.Objects",
                    "Testura.Android.Device.Ui.Search")
                .WithFields(fields.ToArray())
                .WithConstructor(ConstructorGenerator.Create(pageObejctName,
                    BodyGenerator.Create(statements.ToArray()), modifiers: new[] {Modifiers.Public},
                    parameters: new List<Parameter> {new Parameter("device", typeof(IAndroidDevice))}))
                .Build();
            return _codeSaver.SaveCodeAsString(classBuilder);
        }

        private void GenerateUiObject(List<Field> fields, List<StatementSyntax> statements,
            UiObjectInfo pageObjectUiNode)
        {
            fields.Add(new Field(pageObjectUiNode.Name, typeof(UiObject), new[] {Modifiers.Private}));
            statements.Add(Statement.Declaration.Assign(pageObjectUiNode.Name,
                new VariableReference(DeviceName,
                    new MemberReference("Ui",
                        new MethodReference("CreateUiObject", GenerateWithArgument(pageObjectUiNode))))));
        }

        private IEnumerable<IArgument> GenerateWithArgument(UiObjectInfo uiObjectInfo)
        {
            var arguments = new List<IArgument>();

            IList<AttributeTags> withs = null;

            if (uiObjectInfo.Optimal != null)
            {
                if (uiObjectInfo.Optimal.Parent == null)
                {
                    withs = uiObjectInfo.Optimal.Withs;
                }
                else
                {
                    return GenerateOptimalWiths(uiObjectInfo.Optimal);
                }
            }
            else
            {
                withs = uiObjectInfo.FindWith;
            }

            foreach (var with in withs)
            {
                var value = GetNodeValue(uiObjectInfo.Node, with);
                ValueArgument valueArgument;
                if (with == AttributeTags.Index)
                {
                    valueArgument = new ValueArgument(int.Parse(value));
                }
                else
                {
                    valueArgument = new ValueArgument(value);
                }

                arguments.Add(
                    new ReferenceArgument(new VariableReference("With",
                        new MethodReference(with.ToString(), new[] {valueArgument}))));
            }

            return arguments;
        }

        private IEnumerable<IArgument> GenerateOptimalWiths(OptimalWith optimal)
        {
            if (optimal.Parent.Parent == null)
            {
                return new List<IArgument>
                {
                    new LambdaArgument(
                        new AndBinaryExpression(GetBinaryExpression(optimal.Parent, true, 1),
                            GetBinaryExpression(optimal, false, 0)).GetBinaryExpression(), "n")
                };
            }



            var mainIf = Statement.Selection.If(GetBinaryExpression(optimal, false, 0),
                BodyGenerator.Create(Statement.Jump.ReturnTrue()));

            return new List<IArgument>
            {
                new LambdaArgument(BodyGenerator.Create(GenerateParantIfs(optimal.Parent, mainIf, 1), Statement.Jump.ReturnFalse()), "n")
            };
        }

        private StatementSyntax GenerateParantIfs(OptimalWith optimalWith, StatementSyntax child, int depth)
        {
            var myIf = Statement.Selection.If(GetBinaryExpression(optimalWith, true, depth), BodyGenerator.Create(child));

            if (optimalWith.Parent != null)
            {
                return GenerateParantIfs(optimalWith.Parent, myIf, depth + 1);
            }

            return myIf;
        }

        private IBinaryExpression GetBinaryExpression(OptimalWith optimal, bool isParent, int depth)
        {
            var binaryExpressions = new List<ConditionalBinaryExpression>();


            foreach (var attributeTagse in optimal.Withs)
            {
                var leftReference = new VariableReference("n", new MemberReference(Enum.GetName(typeof(AttributeTags), attributeTagse)));
                if (isParent)
                {
                    var name = new StringBuilder();
                    for (int n = 0; n < depth; n++)
                    {
                        name.Append("Parent?.");
                    }

                    name = name.Remove(name.Length - 1, 1);

                    leftReference = new VariableReference("n",
    new MemberReference(name.ToString(),
        new MemberReference(Enum.GetName(typeof(AttributeTags), attributeTagse))));
                }


                binaryExpressions.Add(new ConditionalBinaryExpression(leftReference,
                    new ConstantReference(GetNodeValue(optimal.Node, attributeTagse)), ConditionalStatements.Equal));
            }

            IBinaryExpression finalBinaryExpression = null;

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