using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Testura.Android.PageObjectCreator.Models;

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
        public IList<AndroidElement> GetElements(Point point, string dump)
        {
            var androidElements = _dumpService.ParseDump(dump);
            var matchingElements = androidElements.Where(n => n.PointInsideBounds(point)).ToList();

            matchingElements.Sort((ae1, ae2) => ae1.Area().CompareTo(ae2.Area()));

            return matchingElements;
        }
    }
}
