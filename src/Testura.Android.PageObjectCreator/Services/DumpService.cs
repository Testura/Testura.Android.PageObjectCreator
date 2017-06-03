using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using Testura.Android.Device.Ui.Nodes.Data;
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
                var packageAndActivity = GetCurrentFocus(serial).Split('/');
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
        /// <returns>All parsed nodes</returns>
        public IList<Node> ParseDump(string dump)
        {
            var document = XDocument.Parse(dump);

            var nodes = new List<Node>();
            foreach (var element in document.Descendants("node"))
            {
                Node node;
                var parent = nodes.FirstOrDefault(p => p.Element == element.Parent);
                if (parent != null)
                {
                    node = new Node(element, parent);
                    parent.Children.Add(node);
                }
                else
                {
                    node = new Node(element, null);
                }

                nodes.Add(node);
            }

            return nodes;
        }

        /// <summary>
        /// Parse an xml dump and return it as a tree structure
        /// </summary>
        /// <param name="dump">The xml dump</param>
        /// <returns>The root node</returns>
        public Node ParseDumpAsTree(string dump)
        {
            var document = XDocument.Parse(dump);
            var currentNode = document.Root;
            var node = new Node(currentNode.Element("node"), null);
            TraverseElement(currentNode.Element("node"), node);
            return node;
        }

        private void TraverseElement(XElement element, Node node)
        {
            var childElements = element.Elements();
            foreach (var child in childElements)
            {
                var childNode = new Node(child, node);
                TraverseElement(child, childNode);
                node.Children.Add(childNode);
            }
        }

        private string DumpScreen(string serial)
        {
            var savePath = Path.Combine(AssemblyDirectory, "dump.xml");
            var files = _terminal.ExecuteCmdCommand("adb.exe", "-s", serial, "shell", "ls", "/sdcard/");
            if (files.Contains("dump.xml"))
            {
                _terminal.ExecuteCmdCommand("adb.exe", "-s", serial, "shell", "rm", "/sdcard/dump.xml");
            }

            _terminal.ExecuteCmdCommand("adb.exe", "-s", serial, "shell", "uiautomator", "dump", "/sdcard/dump.xml");
            _terminal.ExecuteCmdCommand("adb.exe", "-s", serial, "pull", "/sdcard/dump.xml", AssemblyDirectory);
            return savePath;
        }

        private string TakeScreenshot(string serial)
        {
            var savePath = Path.Combine(AssemblyDirectory, "screenshot.png");
            _terminal.ExecuteCmdCommand("adb.exe", "-s", serial, "shell", "screencap ", "-p", "/sdcard/screenshot.png");
            _terminal.ExecuteCmdCommand("adb.exe", "-s", serial, "pull", "/sdcard/screenshot.png", AssemblyDirectory);
            return savePath;
        }

        private string GetCurrentFocus(string serial)
        {
            var result = _terminal.ExecuteCmdCommand("adb.exe", "-s", serial, "shell", "dumpsys window windows | grep -E 'mCurrentFocus|mFocusedApp'");
            var focus = result.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).First();
            return focus.Split(' ', '}')[4];
        }
    }
}
