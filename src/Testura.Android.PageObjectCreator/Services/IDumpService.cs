using System.Collections.Generic;
using System.Threading.Tasks;
using Testura.Android.Device.Ui.Nodes.Data;
using Testura.Android.PageObjectCreator.Models;

namespace Testura.Android.PageObjectCreator.Services
{
    public interface IDumpService
    {
        /// <summary>
        /// Dump the screen and take a screenshot and save on the computer
        /// </summary>
        /// <param name="serial">Serial number of the device to dump for</param>
        /// <returns>Object with dump and screenshot path</returns>
        Task<AndroidDumpInfo> DumpScreenAsync(string serial);

        /// <summary>
        /// Parse an xml dump
        /// </summary>
        /// <param name="dump">The xml dump</param>
        /// <returns>All parsed nodes</returns>
        IList<Node> ParseDump(string dump);

        /// <summary>
        /// Parse an xml dump and return it as a tree structure
        /// </summary>
        /// <param name="dump">The xml dump</param>
        /// <returns>The root node</returns>
        Node ParseDumpAsTree(string dump);
    }
}
