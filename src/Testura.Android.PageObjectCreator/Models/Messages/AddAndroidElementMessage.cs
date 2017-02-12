namespace Testura.Android.PageObjectCreator.Models.Messages
{
    /// <summary>
    /// Message sent when we want to create a new UiObject with this android element
    /// </summary>
    public class AddAndroidElementMessage
    {
        public AndroidElement AndroidElement { get; set; }
    }
}
