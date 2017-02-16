using Testura.Android.Device.Ui.Nodes.Data;

namespace Testura.Android.PageObjectCreator.Models.Messages
{
    /// <summary>
    /// Message sent when we want to create a new UiObject with this node
    /// </summary>
    public class AddNodeMessage
    {
        public Node Node { get; set; }
    }
}
