using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Xml;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
        private AndroidElement _selectedAndroidElement;

        public HierarchyViewModel(IDumpService dumpService, IFileService fileService)
        {
            _dumpService = dumpService;
            _fileService = fileService;
            AndroidElements = new ObservableCollection<AndroidElement>();
            Attributes = new ObservableCollection<Attribute>();
            SelectedItemChangedCommand = new RelayCommand<AndroidElement>(SelectedItemChanged);
            MessengerInstance.Register<DumpMessage>(this, OnDump);
        }

        public ObservableCollection<AndroidElement> AndroidElements { get; set; }

        public ObservableCollection<Attribute> Attributes { get; set; }

        public RelayCommand<AndroidElement> SelectedItemChangedCommand { get; set; }

        private void OnDump(DumpMessage message)
        {
            var lines = _fileService.ReadAllLinesFromFile(message.DumpInfo.DumpPath);
            var elements = _dumpService.ParseDumpSimple(string.Join(string.Empty, lines));
            AndroidElements = new ObservableCollection<AndroidElement>(elements);
        }

        private void SelectedItemChanged(AndroidElement selectAndroidElement)
        {
            _selectedAndroidElement = selectAndroidElement;
            Attributes.Clear();
            foreach (var xAttribute in _selectedAndroidElement.Element.Attributes())
            {
                Attributes.Add(new Attribute(xAttribute.Name.LocalName, xAttribute.Value));
            }
            //Attributes.Add(new Attribute("Index", _selectedAndroidElement.Index));
            //Attributes.Add(new Attribute("Text", _selectedAndroidElement.Text));
            //Attributes.Add(new Attribute("Resource-id", _selectedAndroidElement.ResourceId));
            //Attributes.Add(new Attribute("Class", _selectedAndroidElement.Class));
            //Attributes.Add(new Attribute("Package", _selectedAndroidElement.Package));
            //Attributes.Add(new Attribute("Content-desc", _selectedAndroidElement.ContentDesc));
            //Attributes.Add(new Attribute("Checkable", _selectedAndroidElement.Checkable));
            //Attributes.Add(new Attribute("Checked", _selectedAndroidElement.Checked));
            //Attributes.Add(new Attribute("Clickable", _selectedAndroidElement.Clickable));
            //Attributes.Add(new Attribute("Enabled", _selectedAndroidElement.Enabled));
            //Attributes.Add(new Attribute("Focusable", _selectedAndroidElement.Focusable));
            //Attributes.Add(new Attribute("Focused", _selectedAndroidElement.Focused));
        }

    }
}
