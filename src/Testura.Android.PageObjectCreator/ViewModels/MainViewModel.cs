using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using Testura.Android.PageObjectCreator.Models.Messages;
using Testura.Android.PageObjectCreator.Services;

namespace Testura.Android.PageObjectCreator.ViewModels
{
    [ImplementPropertyChanged]
    public class MainViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;

        public MainViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            DumpScreenCommand = new RelayCommand(() => MessengerInstance.Send(new RequestDumpMessage()));
            RefreshDeviceListCommand = new RelayCommand(() => MessengerInstance.Send(new RequestRefreshDeviceListCommand()));
            ShowAboutCommand = new RelayCommand(() => _dialogService.ShowAboutDialog());
            MessengerInstance.Register<DumpMessage>(this, (message) => IsEditEnabled = true);
        }

        public RelayCommand DumpScreenCommand { get; set; }

        public RelayCommand RefreshDeviceListCommand { get; set; }

        public RelayCommand ShowAboutCommand { get; set; }

        public bool IsEditEnabled { get; set; }
    }
}
