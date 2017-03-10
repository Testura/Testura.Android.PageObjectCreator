using System.Collections.Generic;
using System.Windows;
using Testura.Android.Device.Ui.Nodes.Data;

namespace Testura.Android.PageObjectCreator.Services
{
    public interface IScreenService
    {
        /// <summary>
        /// Get all elements that intersect with a point on the screen
        /// </summary>
        /// <param name="point">Points to check</param>
        /// <param name="nodes">All nodes on the dumped screen</param>
        /// <returns>All intersected android elements</returns>
        IList<Node> GetNodes(Point point, IList<Node> nodes);
    }
}
