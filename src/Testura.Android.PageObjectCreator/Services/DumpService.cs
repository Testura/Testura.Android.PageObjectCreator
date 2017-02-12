using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using Testura.Android.PageObjectCreator.Models;
using Testura.Android.PageObjectCreator.Util;

namespace Testura.Android.PageObjectCreator.Services
{
    public class DumpService : IDumpService
    {
        private readonly ITerminal _terminal;

        public DumpService(ITerminal terminal)
        {
            _terminal = terminal;
        }

        private string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        /// <summary>
        /// Dump the screen and take a screenshot and save on the computer
        /// </summary>
        /// <param name="serial">Serial number of the device to dump for</param>
        /// <returns>Object with dump and screenshot path</returns>
        public async Task<AndroidDumpInfo> DumpScreenAsync(string serial)
        {
            return await Task.Run(() =>
            {
                var dump = DumpScreen(serial);
                var screenshot = TakeScreenshot(serial);
                var packageAndActivity = GetCurrentFocus().Split('/');
                return new AndroidDumpInfo
                {
                    DumpPath = dump,
                    ScreenshotPath = screenshot,
                    Package = packageAndActivity[0],
                    Activity = packageAndActivity.Length > 1 ? packageAndActivity[1] : string.Empty
                };
            });
        }

        /// <summary>
        /// Parse an xml dump
        /// </summary>
        /// <param name="dump">The xml dump</param>
        /// <returns>All parsed android elements</returns>
        public IList<AndroidElement> ParseDump(string dump)
        {
            var document = XDocument.Parse(dump);

            var nodes = new List<AndroidElement>();
            foreach (var element in document.Descendants("node"))
            {
                AndroidElement node;
                var parent = nodes.FirstOrDefault(p => p.Element == element.Parent);
                if (parent != null)
                {
                    node = new AndroidElement(element, parent);
                    parent.Children.Add(node);
                }
                else
                {
                    node = new AndroidElement(element, null);
                }

                nodes.Add(node);
            }

            return nodes;
        }

        /// <summary>
        /// Parse an xml dump
        /// </summary>
        /// <param name="dump">The xml dump</param>
        /// <returns>All parsed android elements</returns>
        public IList<AndroidElement> ParseDumpSimple(string dump)
        {
            var document = XDocument.Parse(dump);

            var nodes = new List<AndroidElement>();

            var currentNode = document.Root;
            var androidElement = new AndroidElement(currentNode.Element("node"), null);
            DoStuff(currentNode.Element("node"), androidElement);

            return new List<AndroidElement> { androidElement };
        }

        private void DoStuff(XElement element, AndroidElement androidElement)
        {
            var childEelements = element.Elements();
            foreach (var child in childEelements)
            {
                var childAndroidElement = new AndroidElement(child, androidElement);
                DoStuff(child, childAndroidElement);
                androidElement.Children.Add(childAndroidElement);
            }
        }

        private string DumpScreen(string serial)
        {
            var savePath = Path.Combine(AssemblyDirectory, "dump.xml");
            _terminal.ExecuteCmdCommand("adb.exe", "shell", "uiautomator", "dump", "/sdcard/dump.xml");
            _terminal.ExecuteCmdCommand("adb.exe", "pull", "/sdcard/dump.xml", AssemblyDirectory);
            _terminal.ExecuteCmdCommand("adb.exe", "shell", "rm", savePath);
            return savePath;
        }

        private string TakeScreenshot(string serial)
        {
            var savePath = Path.Combine(AssemblyDirectory, "screenshot.png");
            _terminal.ExecuteCmdCommand("adb.exe", "shell", "screencap ", "-p", "/sdcard/screenshot.png");
            _terminal.ExecuteCmdCommand("adb.exe", "pull", "/sdcard/screenshot.png", AssemblyDirectory);
            _terminal.ExecuteCmdCommand("adb.exe", "shell", "rm", savePath);
            return savePath;
        }

        private string GetCurrentFocus()
        {
            var result = _terminal.ExecuteCmdCommand("adb.exe", "shell", "dumpsys window windows | grep -E 'mCurrentFocus|mFocusedApp'");
            var focus = result.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).First();
            return focus.Split(' ', '}')[4];
        }
    }
}
