using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using Testura.Android.Device.Ui.Nodes.Data;
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
            Nodes = new ObservableCollection<Node>();
            Attributes = new ObservableCollection<Attribute>();
            SelectedItemChangedCommand = new RelayCommand<Node>(SelectedItemChanged);
            AddCommand = new RelayCommand(AddNode, CanAddNode);
            ExpandAllCommand = new RelayCommand(() => IsTreeExpanded = true);
            ContractAllCommand = new RelayCommand(() => IsTreeExpanded = false);
            MessengerInstance.Register<DumpMessage>(this, OnDump);
            MessengerInstance.Register<ShowNodeDetailsMessage>(this, (message) => SelectedItemChanged(message.Node));
        }

        public Node SelectedNode { get; set; }

        public bool IsTreeExpanded { get; set; }

        public ObservableCollection<Node> Nodes { get; set; }

        public ObservableCollection<Attribute> Attributes { get; set; }

        public RelayCommand<Node> SelectedItemChangedCommand { get; set; }

        public RelayCommand AddCommand { get; set; }

        public RelayCommand ExpandAllCommand { get; set; }

        public RelayCommand ContractAllCommand { get; set; }

        private void OnDump(DumpMessage message)
        {
            var lines = _fileService.ReadAllLinesFromFile(message.DumpInfo.DumpPath);
            var node = _dumpService.ParseDumpAsTree(string.Join(string.Empty, lines));
            Nodes = new ObservableCollection<Node> { node };
        }

        private void SelectedItemChanged(Node selectNode)
        {
            SelectedNode = selectNode;
            MessengerInstance.Send(new SelectedHierarchyNodeMesssage { SelectedNode = selectNode });
            Attributes.Clear();
            foreach (var xAttribute in SelectedNode.Element.Attributes())
            {
                Attributes.Add(new Attribute(xAttribute.Name.LocalName, xAttribute.Value));
            }
        }

        private void AddNode()
        {
            MessengerInstance.Send(new AddNodeMessage { Node = SelectedNode });
        }

        private bool CanAddNode()
        {
            return SelectedNode != null;
        }
    }
}
