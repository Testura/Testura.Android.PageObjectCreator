namespace Testura.Android.PageObjectCreator.Models.Messages
{
    /// <summary>
    /// Message sent after a screen dump
    /// </summary>
    public class DumpMessage
    {
        public AndroidDumpInfo DumpInfo { get; set; }
    }
}
