namespace Testura.Android.PageObjectCreator.Models
{
    public class AndroidDumpInfo
    {
        /// <summary>
        /// Gets or sets the path to the .xml dump file
        /// </summary>
        public string DumpPath { get; set; }

        /// <summary>
        /// Gets or sets the path to the screenshot
        /// </summary>
        public string ScreenshotPath { get; set; }

        /// <summary>
        /// Gets or sets the name of the current activity after dumping screen
        /// </summary>
        public string Activity { get; set; }

        /// <summary>
        /// Gets or sets the name of the package after dumping the screen
        /// </summary>
        public string Package { get; set; }
    }
}
