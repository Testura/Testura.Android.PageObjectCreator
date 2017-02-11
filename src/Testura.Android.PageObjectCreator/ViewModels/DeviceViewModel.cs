using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Testura.Android.PageObjectCreator.Models.Messages;
using Testura.Android.PageObjectCreator.Services;

namespace Testura.Android.PageObjectCreator.ViewModels
{
    public class DeviceViewModel : ViewModelBase
    {
        private readonly IDeviceService _deviceService;

        public DeviceViewModel(IDeviceService deviceService)
        {
            _deviceService = deviceService;
            Devices = new ObservableCollection<string>();
            RefreshDevicesCommand = new RelayCommand(RefreshDeviceList);
            SelectedDeviceChangedCommand = new RelayCommand<string>(SelectedDeviceChanged);
            RefreshDeviceList();
            MessengerInstance.Register<RequestRefreshDeviceListCommand>(this, (message) => RefreshDeviceList());
        }

        public ObservableCollection<string> Devices { get; set; }

        public RelayCommand RefreshDevicesCommand { get; set; }

        public RelayCommand<string> SelectedDeviceChangedCommand { get; set; }

        private void RefreshDeviceList()
        {
            Devices.Clear();
            var connectedDevices = _deviceService.GetAllConnectedDevices();
            foreach (var connectedDevice in connectedDevices)
            {
                Devices.Add(connectedDevice);
            }

            if (Devices.Any())
            {
                MessengerInstance.Send(new DeviceAvailableMessage { IsDeviceAvailable = true });
                MessengerInstance.Send(new SelectedDeviceChangedMessage { Serial = Devices.First() });
            }
        }

        private void SelectedDeviceChanged(string serial)
        {
            MessengerInstance.Send(new SelectedDeviceChangedMessage { Serial = serial });
        }
    }
}
