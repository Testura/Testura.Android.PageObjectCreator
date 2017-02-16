using System.Collections.Generic;
using System.Windows;
using Testura.Android.Device.Ui.Nodes.Data;
using Testura.Android.PageObjectCreator.Models;

namespace Testura.Android.PageObjectCreator.Services
{
    public interface IScreenService
    {
        /// <summary>
        /// Get all elements that intersect with a point on the screen
        /// </summary>
        /// <param name="point">Points to check</param>
        /// <param name="dump">the xml dump</param>
        /// <returns>All intersected android elements</returns>
        IList<Node> GetNodes(Point point, string dump);
    }
}
