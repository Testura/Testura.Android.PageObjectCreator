﻿using System.ComponentModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using Testura.Android.Device.Ui.Nodes.Data;
using Testura.Android.PageObjectCreator.Models;
using Testura.Android.PageObjectCreator.Models.Messages;
using Testura.Android.PageObjectCreator.Services;
using Testura.Android.PageObjectCreator.Util.Extensions;

namespace Testura.Android.PageObjectCreator.ViewModels
{
    [ImplementPropertyChanged]
    public class PageObjectViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private Node _topNode;

        public PageObjectViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            MessengerInstance.Register<DumpMessage>(this, OnDump);
            MessengerInstance.Register<AddUiObjectInfoMessage>(this, OnAddUiObjectInfo);
            MessengerInstance.Register<StartedDumpScreenMessage>(this, OnStartDumpScreen);
            EditWithsCommand = new RelayCommand<UiObjectInfo>(EditWiths);
            UiInfoChangedCommand = new RelayCommand(SendPageObjectChanged);
            DeleteUiObjectInfoCommand = new RelayCommand<UiObjectInfo>(DeleteUiObjectInfo);
            PageObject = new PageObject { Name = "MyClass", Namespace = "MyNamespace" };
            ((INotifyPropertyChanged)PageObject).PropertyChanged += OnPropertyChanged;
        }

        public PageObject PageObject { get; set; }

        public RelayCommand<UiObjectInfo> EditWithsCommand { get; set; }

        public RelayCommand UiInfoChangedCommand { get; set; }

        public RelayCommand<UiObjectInfo> DeleteUiObjectInfoCommand { get; set; }

        private void OnAddUiObjectInfo(AddUiObjectInfoMessage message)
        {
            PageObject.UiObjectInfos.Add(message.UiNodeInfo);
            MessengerInstance.Send(new PageObjectChangedMessage { PageObject = PageObject });
        }

        private void OnDump(DumpMessage message)
        {
            PageObject.Activity = message.DumpInfo.Activity;
            PageObject.Package = message.DumpInfo.Package;
            _topNode = message.TopNode;
        }

        private void OnStartDumpScreen(StartedDumpScreenMessage obj)
        {
            if (PageObject.UiObjectInfos.Any())
            {
                if (_dialogService.ShowClearUiObjectsDialog())
                {
                    PageObject.UiObjectInfos.Clear();
                }
            }
        }

        private void EditWiths(UiObjectInfo obj)
        {
            _dialogService.ShowWithDialog(obj, _topNode.AllNodes());
            SendPageObjectChanged();
        }

        private void DeleteUiObjectInfo(UiObjectInfo uiObjectInfo)
        {
            PageObject.UiObjectInfos.Remove(uiObjectInfo);
            MessengerInstance.Send(new UiObjectInfoRemovedMessage { UiObjectInfo = uiObjectInfo });
            SendPageObjectChanged();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            SendPageObjectChanged();
        }

        private void SendPageObjectChanged()
        {
            MessengerInstance.Send(new PageObjectChangedMessage { PageObject = PageObject });
        }
    }
}
