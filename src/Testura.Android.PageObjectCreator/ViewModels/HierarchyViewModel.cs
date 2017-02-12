using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using Testura.Android.PageObjectCreator.Models;
using Testura.Android.PageObjectCreator.Models.Messages;
using Testura.Android.PageObjectCreator.Services;

namespace Testura.Android.PageObjectCreator.ViewModels
{
    [ImplementPropertyChanged]
    public class HierarchyViewModel : ViewModelBase
    {
        private readonly IDumpService _dumpService;
        private readonly IFileService _fileService;

        public HierarchyViewModel(IDumpService dumpService, IFileService fileService)
        {
            _dumpService = dumpService;
            _fileService = fileService;
            AndroidElements = new ObservableCollection<AndroidElement>();
            Attributes = new ObservableCollection<Attribute>();
            SelectedItemChangedCommand = new RelayCommand<AndroidElement>(SelectedItemChanged);
            AddCommand = new RelayCommand(AddAndroidElement, CanAddAndroidElement);
            ExpandAllCommand = new RelayCommand(() => IsTreeExpanded = true);
            ContractAllCommand = new RelayCommand(() => IsTreeExpanded = false);
            MessengerInstance.Register<DumpMessage>(this, OnDump);
        }

        public AndroidElement SelectedAndroidElement { get; set; }

        public bool IsTreeExpanded { get; set; }

        public ObservableCollection<AndroidElement> AndroidElements { get; set; }

        public ObservableCollection<Attribute> Attributes { get; set; }

        public RelayCommand<AndroidElement> SelectedItemChangedCommand { get; set; }

        public RelayCommand AddCommand { get; set; }

        public RelayCommand ExpandAllCommand { get; set; }

        public RelayCommand ContractAllCommand { get; set; }

        private void OnDump(DumpMessage message)
        {
            var lines = _fileService.ReadAllLinesFromFile(message.DumpInfo.DumpPath);
            var elements = _dumpService.ParseDumpSimple(string.Join(string.Empty, lines));
            AndroidElements = new ObservableCollection<AndroidElement>(elements);
        }

        private void SelectedItemChanged(AndroidElement selectAndroidElement)
        {
            SelectedAndroidElement = selectAndroidElement;
            MessengerInstance.Send(new SelectedHierarchyNodeMesssage { SelectedAndroidElement = selectAndroidElement });
            Attributes.Clear();
            foreach (var xAttribute in SelectedAndroidElement.Element.Attributes())
            {
                Attributes.Add(new Attribute(xAttribute.Name.LocalName, xAttribute.Value));
            }
        }

        private void AddAndroidElement()
        {
            MessengerInstance.Send(new AddAndroidElementMessage { AndroidElement = SelectedAndroidElement });
        }

        private bool CanAddAndroidElement()
        {
            return SelectedAndroidElement != null;
        }
    }
}
