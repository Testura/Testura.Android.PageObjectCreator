namespace Testura.Android.PageObjectCreator.Models.Messages
{
    /// <summary>
    /// Message sent when the selected device in the device list has changed
    /// </summary>
    public class SelectedDeviceChangedMessage
    {
        public string Serial { get; set; }
    }
}
