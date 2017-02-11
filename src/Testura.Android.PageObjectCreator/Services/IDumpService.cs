using System.Collections.Generic;
using System.Threading.Tasks;
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
        /// <returns>All parsed android elements</returns>
        IList<AndroidElement> ParseDump(string dump);
    }
}
