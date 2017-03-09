using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Testura.Android.Device.Ui.Nodes.Data;
using Testura.Android.PageObjectCreator.Models;
using Testura.Android.PageObjectCreator.Util.Extensions;

namespace Testura.Android.PageObjectCreator.Services
{
    public class ScreenService : IScreenService
    {
        private readonly IDumpService _dumpService;

        public ScreenService(IDumpService dumpService)
        {
            _dumpService = dumpService;
        }

        /// <summary>
        /// Get all elements that intersect with a point on the screen
        /// </summary>
        /// <param name="point">Points to check</param>
        /// <param name="dump">the xml dump</param>
        /// <returns>All intersected android elements</returns>
        public IList<Node> GetNodes(Point point, Node node)
        {
            var nodes = new List<Node>();
             FindNode(point, node, nodes);

            nodes.Sort((ae1, ae2) => ae1.Area().CompareTo(ae2.Area()));

            return nodes;
        }

        private void FindNode(Point point, Node node, IList<Node> foundNodes)
        {
            if (node.PointInsideBounds(point))
            {
                foundNodes.Add(node);
            }

            foreach (var nodeChild in node.Children)
            {
                FindNode(point, nodeChild, foundNodes);
            }

        }
    }
}
