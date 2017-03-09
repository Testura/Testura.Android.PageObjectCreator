using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using Testura.Android.Device.Ui.Nodes.Data;
using Testura.Android.PageObjectCreator.Models;
using Testura.Android.PageObjectCreator.Models.Messages;
using Testura.Android.PageObjectCreator.Services;
using Attribute = Testura.Android.PageObjectCreator.Models.Attribute;

namespace Testura.Android.PageObjectCreator.ViewModels
{
    [ImplementPropertyChanged]
    public class HierarchyViewModel : ViewModelBase
    {
        public HierarchyViewModel()
        {
            Nodes = new ObservableCollection<NodeTreeItem>();
            Attributes = new ObservableCollection<Attribute>();
            SelectedItemChangedCommand = new RelayCommand<NodeTreeItem>(SelectedItemChanged);
            AddCommand = new RelayCommand(AddNode, CanAddNode);
            ExpandAllCommand = new RelayCommand(() => ExpandAll(Nodes.First(), true));
            ContractAllCommand = new RelayCommand(() => ExpandAll(Nodes.First(), false));
            MessengerInstance.Register<DumpMessage>(this, OnDump);
            MessengerInstance.Register<ShowNodeDetailsMessage>(this, (message) => SelectedItemChanged(new NodeTreeItem(message.Node)));
        }

        private void ExpandAll(NodeTreeItem node, bool expand)
        {
            node.IsExpanded = expand;

            foreach (var nodeTreeItem in node.Children)
            {
                ExpandAll(nodeTreeItem, expand);
            }
        }

        public NodeTreeItem SelectedNode { get; set; }

        public bool IsTreeExpanded { get; set; }

        public ObservableCollection<NodeTreeItem> Nodes { get; set; }

        public ObservableCollection<Attribute> Attributes { get; set; }

        public RelayCommand<NodeTreeItem> SelectedItemChangedCommand { get; set; }

        public RelayCommand AddCommand { get; set; }

        public RelayCommand ExpandAllCommand { get; set; }

        public RelayCommand ContractAllCommand { get; set; }

        private void OnDump(DumpMessage message)
        {
            Nodes = new ObservableCollection<NodeTreeItem> { new NodeTreeItem(message.Node) };
        }

        private void SelectedItemChanged(NodeTreeItem selectNode)
        {
            if (selectNode == null)
            {
                return;
            }

            SelectedNode = FindNode(Nodes.First(), selectNode.Node);
            MessengerInstance.Send(new SelectedHierarchyNodeMesssage { SelectedNode = SelectedNode.Node });
            Attributes.Clear();
            foreach (var xAttribute in SelectedNode.Node.Element.Attributes())
            {
                Attributes.Add(new Attribute(xAttribute.Name.LocalName, xAttribute.Value));
            }

            SelectedNode.IsSelected = true;
            ExpandAll(Nodes.First(), true);
        }

        private NodeTreeItem FindNode(NodeTreeItem node, Node wantedNode)
        {
            if (node.Node == wantedNode)
            {
                return node;
            }

            foreach (var nodeTreeItem in node.Children)
            {
                var foundNode = FindNode(nodeTreeItem, wantedNode);
                if (foundNode != null)
                {
                    return foundNode;
                }
            }

            return null;
        }

        private void AddNode()
        {
            MessengerInstance.Send(new AddNodeMessage { Node = SelectedNode.Node });
        }

        private bool CanAddNode()
        {
            return SelectedNode != null;
        }
    }
}
