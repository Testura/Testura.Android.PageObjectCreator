using System.Collections.Generic;
using PropertyChanged;
using Testura.Android.Device.Ui.Nodes.Data;
using Testura.Android.Util;

namespace Testura.Android.PageObjectCreator.Models
{
    [ImplementPropertyChanged]
    public class UiObjectInfo
    {
        /// <summary>
        /// Gets or sets the name of the ui object
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the node
        /// </summary>
        public Node Node { get; set; }

        /// <summary>
        /// Gets or sets the find with for a node
        /// </summary>
        public IList<AttributeTags> FindWith { get; set; }

        /// <summary>
        /// Gets or sets the auto selected with
        /// </summary>
        public AutoSelectedWith AutoSelectedWith { get; set; }

        /// <summary>
        /// Gets or sets the find string that contains all our selected withs
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Update the display name
        /// </summary>
        public void UpdateDisplayName()
        {
            DisplayName = AutoSelectedWith != null ? "Automatic" : string.Join(", ", FindWith);
        }
    }
}
