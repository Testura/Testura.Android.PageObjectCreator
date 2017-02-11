using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using PropertyChanged;
using Testura.Android.PageObjectCreator.Models;
using Testura.Android.PageObjectCreator.Models.Messages;
using Testura.Android.PageObjectCreator.Services;
using Testura.Android.Util;

namespace Testura.Android.PageObjectCreator.ViewModels
{
    [ImplementPropertyChanged]
    public class ScreenViewModel : ViewModelBase
    {
        private readonly IFileService _fileService;
        private readonly IScreenService _screenService;
        private readonly IDialogService _dialogService;

        public ScreenViewModel(IFileService fileService, IScreenService screenService, IDialogService dialogService)
        {
            _fileService = fileService;
            _screenService = screenService;
            _dialogService = dialogService;
            ShouldShowInfoMessage = true;
            MessengerInstance.Register<DumpMessage>(this, OnNewDump);
            MessengerInstance.Register<StartedDumpScreenMessage>(this, OnStartedDumpingScreen);
            MessengerInstance.Register<StoppedDumpScreenMessage>(this, OnStoppedDumpingScreen);
        }

        public event EventHandler<AndroidDumpInfo> LoadImage;

        public bool IsDumpingScreen { get; set; }

        public bool ShouldShowInfoMessage { get; set; }

        public AndroidElement GetElements(Point point, string dumpPath)
        {
            var lines = _fileService.ReadAllLinesFromFile(dumpPath);
            var elements = _screenService.GetElements(point, string.Join(string.Empty, lines));
            if (elements.Any())
            {
                var element = elements.First();

                // Sometimes we get nodes that contains nodes with the same boundary
                // So this is just to make sure we get the absolut child.
                while (element.Children.Any())
                {
                    element = element.Children.First();
                }

                return element;
            }

            return null;
        }

        public bool AddElement(AndroidElement element)
        {
            var name = _dialogService.ShowNameDialog();
            if (!string.IsNullOrEmpty(name))
            {
                var uiNodeInfo = new UiObjectInfo
                {
                    Name = name,
                    AndroidElement = element,
                    FindWith = new List<AttributeTags>()
                };
                MessengerInstance.Send(new AddUiObjectInfoMessage { UiNodeInfo = uiNodeInfo });
                return true;
            }

            return false;
        }

        private void OnNewDump(DumpMessage message)
        {
            IsDumpingScreen = false;
            LoadImage?.Invoke(this, message.DumpInfo);
        }

        private void OnStartedDumpingScreen(StartedDumpScreenMessage obj)
        {
            IsDumpingScreen = true;
            ShouldShowInfoMessage = false;
        }

        private void OnStoppedDumpingScreen(StoppedDumpScreenMessage obj)
        {
            IsDumpingScreen = false;
        }
    }
}
