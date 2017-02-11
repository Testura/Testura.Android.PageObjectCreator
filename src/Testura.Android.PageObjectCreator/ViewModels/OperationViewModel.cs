using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using Testura.Android.PageObjectCreator.Models.Messages;
using Testura.Android.PageObjectCreator.Services;

namespace Testura.Android.PageObjectCreator.ViewModels
{
    [ImplementPropertyChanged]
    public class OperationViewModel : ViewModelBase
    {
        private readonly IDumpService _dumpService;
        private readonly IDialogService _dialogService;
        private string _serial;
        private bool _isDeviceAvailable;

        public OperationViewModel(IDumpService dumpService, IDialogService dialogService)
        {
            _dumpService = dumpService;
            _dialogService = dialogService;
            DumpCommand = new RelayCommand(DumpScreen);
            MessengerInstance.Register<DeviceAvailableMessage>(this, NewDeviceAvailable);
            MessengerInstance.Register<RequestDumpMessage>(this, (message) => DumpScreen());
            MessengerInstance.Register<SelectedDeviceChangedMessage>(this, SelectedDeviceChanged);
        }

        public bool IsDumpingScreen { get; set; }

        public bool IsDeviceAvailable { get; set; }

        public bool IsGoToActivityEnabled { get; set; }

        public RelayCommand DumpCommand { get; set; }

        private async void DumpScreen()
        {
            MessengerInstance.Send(new StartedDumpScreenMessage());
            IsDumpingScreen = true;
            try
            {
                var dumpInformation = await _dumpService.DumpScreenAsync(_serial);
                MessengerInstance.Send(new DumpMessage { DumpInfo = dumpInformation });
                IsGoToActivityEnabled = true;
            }
            catch (Exception)
            {
                MessengerInstance.Send(new StoppedDumpScreenMessage());
                _dialogService.ShowErrorDialog("Failed to dump screen, make sure you're device are connected and powered on.");
            }

            IsDumpingScreen = false;
        }

        private void NewDeviceAvailable(DeviceAvailableMessage message)
        {
            _isDeviceAvailable = message.IsDeviceAvailable;
            CheckDumpConditions();
        }

        private void SelectedDeviceChanged(SelectedDeviceChangedMessage message)
        {
            _serial = message.Serial;
            CheckDumpConditions();
        }

        private void CheckDumpConditions()
        {
            if (!string.IsNullOrEmpty(_serial) && _isDeviceAvailable)
            {
                IsDeviceAvailable = true;
            }
        }
    }
}
