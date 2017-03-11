using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Testura.Android.Device.Ui.Nodes.Data;
using Testura.Android.PageObjectCreator.Util.Extensions;

namespace Testura.Android.PageObjectCreator.Services
{
    public class ScreenService : IScreenService
    {
        /// <summary>
        /// Get all elements that intersect with a point on the screen
        /// </summary>
        /// <param name="point">Points to check</param>
        /// <param name="nodes">All nodes on the dumped screen</param>
        /// <returns>All intersected android elements</returns>
        public IList<Node> GetNodes(Point point, IList<Node> nodes)
        {
            var foundNodes = nodes.Where(n => n.IsPointInsideBounds(point)).ToList();
            foundNodes.Sort((ae1, ae2) => ae1.Area().CompareTo(ae2.Area()));
            return foundNodes;
        }
    }
}
