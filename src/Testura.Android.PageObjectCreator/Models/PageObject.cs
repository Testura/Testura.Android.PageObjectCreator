using System.Collections.ObjectModel;
using PropertyChanged;

namespace Testura.Android.PageObjectCreator.Models
{
    [ImplementPropertyChanged]
    public class PageObject
    {
        public PageObject()
        {
            UiObjectInfos = new ObservableCollection<UiObjectInfo>();
        }

        /// <summary>
        /// Gets or sets the name of the page object (will generate as class name)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or set the namespace of the page object
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Gets or sets the name of the current activity after dumping screen
        /// </summary>
        public string Package { get; set; }

        /// <summary>
        /// Gets or sets the name of the package after dumping the screen
        /// </summary>
        public string Activity { get; set; }

        /// <summary>
        /// Gets or sets the list of ui object on the page object
        /// </summary>
        public ObservableCollection<UiObjectInfo> UiObjectInfos { get; set; }
    }
}
