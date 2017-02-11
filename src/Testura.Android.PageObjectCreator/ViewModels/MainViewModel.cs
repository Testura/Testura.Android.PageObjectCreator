using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Testura.Android.PageObjectCreator.Models.Messages;

namespace Testura.Android.PageObjectCreator.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            DumpScreenCommand = new RelayCommand(() => MessengerInstance.Send(new RequestDumpMessage()));
            RefreshDeviceListCommand = new RelayCommand(() => MessengerInstance.Send(new RequestRefreshDeviceListCommand()));
        }

        public RelayCommand DumpScreenCommand { get; set; }

        public RelayCommand RefreshDeviceListCommand { get; set; }
    }
}
