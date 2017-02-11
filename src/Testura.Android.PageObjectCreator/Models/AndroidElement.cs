using System;
using System.Collections.Generic;
using System.Windows;
using System.Xml.Linq;

namespace Testura.Android.PageObjectCreator.Models
{
    public class AndroidElement
    {
        public AndroidElement(XElement element, AndroidElement parent)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            Element = element;
            Parent = parent;
            Children = new List<AndroidElement>();
            Text = element.Attribute("text")?.Value;
            ResourceId = element.Attribute("resource-id")?.Value;
            ContentDesc = element.Attribute("content-desc")?.Value;
            Class = element.Attribute("class")?.Value;
            Index = element.Attribute("index")?.Value;
            Package = element.Attribute("package")?.Value;
        }

        /// <summary>
        /// Gets the raw element of a node
        /// </summary>
        public XElement Element { get; }

        /// <summary>
        /// Gets the parent of a node
        /// </summary>
        public AndroidElement Parent { get; }

        /// <summary>
        /// Gets all children of a node
        /// </summary>
        public IList<AndroidElement> Children { get; }

        /// <summary>
        /// Gets the text of a node
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets the resource if of a node
        /// </summary>
        public string ResourceId { get; }

        /// <summary>
        /// Gets the content desc of a node
        /// </summary>
        public string ContentDesc { get; }

        /// <summary>
        /// Gets the class of a node
        /// </summary>
        public string Class { get; }

        /// <summary>
        /// Gets the index of a node
        /// </summary>
        public string Index { get; }

        /// <summary>
        /// Gets the package of a node
        /// </summary>
        public string Package { get; }

        /// <summary>
        /// Get the top left and lower right corner of a element.
        /// </summary>
        /// <returns>A list with the top left and lower right coordinate.</returns>
        public List<Point> GetElementBounds()
        {
            var bounds = Element.Attribute("bounds");

            // Could we use regexp? Yes, but this is more hardcore.
            var values = bounds.Value
                .Replace("][", ",")
                .Replace("[", string.Empty)
                .Replace("]", string.Empty)
                .Split(',');
            return new List<Point>
            {
                new Point(int.Parse(values[0]), int.Parse(values[1])),
                new Point(int.Parse(values[2]), int.Parse(values[3]))
            };
        }

        /// <summary>
        /// Check if a coordinate are inside element bounds
        /// </summary>
        /// <param name="coordinate">Coordinate to check</param>
        /// <returns>True if it are inside, otherwise false</returns>
        public bool PointInsideBounds(Point coordinate)
        {
            var bounds = GetElementBounds();
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
        public double Area()
        {
            var elementBouds = GetElementBounds();
            return (elementBouds[1].X - elementBouds[0].X) * (elementBouds[1].Y - elementBouds[0].Y);
        }
    }
}
