using System.Collections.Generic;
using System.Windows;
using Testura.Android.Device.Ui.Nodes.Data;

namespace Testura.Android.PageObjectCreator.Util.Extensions
{
    public static class NodeExtensions
    {
        /// <summary>
        /// Check if a coordinate are inside node bounds
        /// </summary>
        /// <param name="coordinate">Coordinate to check</param>
        /// <returns>True if it are inside, otherwise false</returns>
        public static bool PointInsideBounds(this Node node, Point coordinate)
        {
            var bounds = node.GetNodeBounds();
            if (coordinate.X >= bounds[0].X && coordinate.X <= bounds[1].X)
            {
                if (coordinate.Y >= bounds[0].Y && coordinate.Y <= bounds[1].Y)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Get the area of the element
        /// </summary>
        /// <returns>The area of the element</returns>
        public static double Area(this Node node)
        {
            var elementBouds = node.GetNodeBounds();
            return (elementBouds[1].X - elementBouds[0].X) * (elementBouds[1].Y - elementBouds[0].Y);
        }
    }
}
