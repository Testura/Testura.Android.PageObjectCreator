using System.Collections.Generic;
using Testura.Android.Device.Ui.Nodes.Data;
using Testura.Android.Util;

namespace Testura.Android.PageObjectCreator.Models
{
    public class AutoSelectedWith
    {
        public Node Node { get; set; }

        public IList<AttributeTags> Withs { get; set; }

        public AutoSelectedWith Parent { get; set; }
    }
}
