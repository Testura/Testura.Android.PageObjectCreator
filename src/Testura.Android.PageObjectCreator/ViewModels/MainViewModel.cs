using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using Testura.Android.PageObjectCreator.Models.Messages;

namespace Testura.Android.PageObjectCreator.ViewModels
{
    [ImplementPropertyChanged]
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            DumpScreenCommand = new RelayCommand(() => MessengerInstance.Send(new RequestDumpMessage()));
            RefreshDeviceListCommand = new RelayCommand(() => MessengerInstance.Send(new RequestRefreshDeviceListCommand()));
            MessengerInstance.Register<DumpMessage>(this, (message) => IsEditEnabled = true);
        }

        public RelayCommand DumpScreenCommand { get; set; }

        public RelayCommand RefreshDeviceListCommand { get; set; }

        public bool IsEditEnabled { get; set; }
    }
}
