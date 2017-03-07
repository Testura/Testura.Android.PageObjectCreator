using System.Collections.Generic;
using Testura.Android.Util;

namespace Testura.Android.PageObjectCreator.Models
{
    public class OptimalWith
    {
        public IList<AttributeTags> Withs { get; set; }

        public OptimalWith Parent { get; set; }
    }
}
