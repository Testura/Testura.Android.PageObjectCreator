namespace Testura.Android.PageObjectCreator.Models.Messages
{
    /// <summary>
    /// Message sent when we want to add a new UI object info object
    /// </summary>
    public class AddUiObjectInfoMessage
    {
        public UiObjectInfo UiNodeInfo { get; set; }
    }
}
