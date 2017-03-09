using System.Collections.Generic;
using Testura.Android.Device.Ui.Nodes.Data;
using Testura.Android.Util;

namespace Testura.Android.PageObjectCreator.Models
{
    public class OptimalWith
    {
        public Node Node { get; set; }

        public IList<AttributeTags> Withs { get; set; }

        public OptimalWith Parent { get; set; }
    }
}
