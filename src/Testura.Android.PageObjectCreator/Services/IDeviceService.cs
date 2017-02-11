using System.Collections.Generic;

namespace Testura.Android.PageObjectCreator.Services
{
    public interface IDeviceService
    {
        /// <summary>
        /// Get all the connected devices
        /// </summary>
        /// <returns>A list with serials of all the connected devices</returns>
        IList<string> GetAllConnectedDevices();
    }
}
