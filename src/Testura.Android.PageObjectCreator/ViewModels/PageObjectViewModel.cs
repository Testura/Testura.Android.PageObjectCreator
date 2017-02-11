using System.ComponentModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using Testura.Android.PageObjectCreator.Models;
using Testura.Android.PageObjectCreator.Models.Messages;
using Testura.Android.PageObjectCreator.Services;

namespace Testura.Android.PageObjectCreator.ViewModels
{
    [ImplementPropertyChanged]
    public class PageObjectViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;

        public PageObjectViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            MessengerInstance.Register<DumpMessage>(this, OnDump);
            MessengerInstance.Register<AddUiObjectInfoMessage>(this, OnAddUiObjectInfo);
            EditWithsCommand = new RelayCommand<UiObjectInfo>(EditWiths);
            UiInfoChangedCommand = new RelayCommand(OnUiInfoChanged);
            PageObject = new PageObject { Name = "MyClass", Namespace = "MyNamespace" };
            ((INotifyPropertyChanged)PageObject).PropertyChanged += OnPropertyChanged;
        }

        public RelayCommand<UiObjectInfo> EditWithsCommand { get; set; }

        public PageObject PageObject { get; set; }

        public RelayCommand UiInfoChangedCommand { get; set; }

        private void OnAddUiObjectInfo(AddUiObjectInfoMessage message)
        {
            PageObject.UiObjectInfos.Add(message.UiNodeInfo);
            MessengerInstance.Send(new PageObjectChangedMessage { PageObject = PageObject });
        }

        private void OnDump(DumpMessage message)
        {
            PageObject.Activity = message.DumpInfo.Activity;
            PageObject.Package = message.DumpInfo.Package;
        }

        private void EditWiths(UiObjectInfo obj)
        {
            _dialogService.ShowWithDialog(obj);
            MessengerInstance.Send(new PageObjectChangedMessage { PageObject = PageObject });
        }

        private void OnUiInfoChanged()
        {
            MessengerInstance.Send(new PageObjectChangedMessage { PageObject = PageObject });
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            MessengerInstance.Send(new PageObjectChangedMessage { PageObject = PageObject });
        }
    }
}
