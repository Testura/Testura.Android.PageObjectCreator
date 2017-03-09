using System.Collections.ObjectModel;
using System.Linq;
using PropertyChanged;
using Testura.Android.Device.Ui.Nodes.Data;

namespace Testura.Android.PageObjectCreator.Models
{
    [ImplementPropertyChanged]
    public class NodeTreeItem
    {
        public NodeTreeItem(Node node)
        {
            Node = node;
            Children = new ObservableCollection<NodeTreeItem>(Node.Children.Select(n => new NodeTreeItem(n)));
        }

        public Node Node { get; set; }

        public ObservableCollection<NodeTreeItem> Children { get; set; }

        public bool IsSelected { get; set; }

        public bool IsExpanded { get; set; }
    }
}
