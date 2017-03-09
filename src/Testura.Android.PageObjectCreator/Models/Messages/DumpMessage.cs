using Testura.Android.Device.Ui.Nodes.Data;

namespace Testura.Android.PageObjectCreator.Models.Messages
{
    /// <summary>
    /// Message sent after a screen dump
    /// </summary>
    public class DumpMessage
    {
        public AndroidDumpInfo DumpInfo { get; set; }

        public Node Node { get; set; }
    }
}
