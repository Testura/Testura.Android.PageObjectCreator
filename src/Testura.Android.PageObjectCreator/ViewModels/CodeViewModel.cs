using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PropertyChanged;
using Testura.Android.Device;
using Testura.Android.Device.Ui.Objects;
using Testura.Android.PageObjectCreator.Models;
using Testura.Android.PageObjectCreator.Models.Messages;
using Testura.Android.Util;
using Testura.Code;
using Testura.Code.Builders;
using Testura.Code.Generators.Class;
using Testura.Code.Generators.Common;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;
using Testura.Code.Models;
using Testura.Code.Models.References;
using Testura.Code.Saver;
using Testura.Code.Statements;

namespace Testura.Android.PageObjectCreator.ViewModels
{
    [ImplementPropertyChanged]
    public class CodeViewModel : ViewModelBase
    {
        private const string DeviceName = "device";

        public CodeViewModel()
        {
            Code = string.Empty;
            MessengerInstance.Register<PageObjectChangedMessage>(this, OnPageObjectChanged);
        }

        public string Code { get; set; }

        private void OnPageObjectChanged(PageObjectChangedMessage message)
        {
            var fields = new List<Field>();
            var statements = new List<StatementSyntax>();

            fields.Add(new Field(DeviceName, typeof(IAndroidDevice), new[] { Modifiers.Private }));
            statements.Add(Statement.Declaration.Assign(new VariableReference("this", new MemberReference(DeviceName)), new VariableReference("device")));

            foreach (var pageObjectUiNode in message.PageObject.UiObjectInfos)
            {
                GenerateUiObject(fields, statements, pageObjectUiNode);
            }

            var classBuilder = new ClassBuilder(message.PageObject.Name, message.PageObject.Namespace)
                .WithFields(fields.ToArray())
                .WithConstructor(ConstructorGenerator.Create(message.PageObject.Name, BodyGenerator.Create(statements.ToArray()), modifiers: new[] { Modifiers.Public }, parameters: new List<Parameter> { new Parameter("device", typeof(IAndroidDevice)) }))
                .Build();
            Code = new CodeSaver().SaveCodeAsString(classBuilder);
        }

        private void GenerateUiObject(List<Field> fields, List<StatementSyntax> statements, UiObjectInfo pageObjectUiNode)
        {
            fields.Add(new Field(pageObjectUiNode.Name, typeof(UiObject), new[] { Modifiers.Private }));
            statements.Add(Statement.Declaration.Assign(pageObjectUiNode.Name, new VariableReference(DeviceName, new MemberReference("Ui", new MethodReference("CreateUiObject", GenerateWithArgument(pageObjectUiNode))))));
        }

        private IEnumerable<IArgument> GenerateWithArgument(UiObjectInfo uiObjectInfo)
        {
            var arguments = new List<IArgument>();

            foreach (var with in uiObjectInfo.FindWith)
            {
                arguments.Add(new ReferenceArgument(new VariableReference("With", new MethodReference(with.ToString(), new[] { new ValueArgument(GetNodeValue(uiObjectInfo, with)) }))));
            }

            return arguments;
        }

        private string GetNodeValue(UiObjectInfo uiObjectInfo, AttributeTags with)
        {
            switch (with)
            {
                case AttributeTags.Text:
                    return uiObjectInfo.AndroidElement.Text;
                case AttributeTags.ResourceId:
                    return uiObjectInfo.AndroidElement.ResourceId;
                case AttributeTags.ContentDesc:
                    return uiObjectInfo.AndroidElement.ContentDesc;
                case AttributeTags.Class:
                    return uiObjectInfo.AndroidElement.Class;
                case AttributeTags.Package:
                    return uiObjectInfo.AndroidElement.Package;
                case AttributeTags.Index:
                    return uiObjectInfo.AndroidElement.Index;
                default:
                    throw new ArgumentOutOfRangeException(nameof(with), with, null);
            }
        }
    }
}
