using System;
using System.Collections.Generic;
using System.Linq;
using Testura.Android.PageObjectCreator.Util;

namespace Testura.Android.PageObjectCreator.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly ITerminal _terminal;

        public DeviceService(ITerminal terminal)
        {
            _terminal = terminal;
        }

        /// <summary>
        /// Get all the connected devices
        /// </summary>
        /// <returns>A list with serials of all the connected devices</returns>
        public IList<string> GetAllConnectedDevices()
        {
            var serials = new List<string>();
            var output = _terminal.ExecuteCmdCommand("adb.exe", "devices");
            var devices = output.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 1; i < devices.Length; i++)
            {
                if (devices[i].Contains("daemon"))
                {
                    continue;
                }

                serials.Add(devices[i].Split('\t').First());
            }

            return serials;
        }
    }
}
