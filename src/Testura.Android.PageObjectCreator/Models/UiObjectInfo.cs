using System;
using System.Collections.Generic;
using System.Text;
using PropertyChanged;
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
        /// Gets or sets the android element element
        /// </summary>
        public AndroidElement AndroidElement { get; set; }

        /// <summary>
        /// Gets or sets the find by for a android element
        /// </summary>
        public IList<AttributeTags> FindWith { get; set; }

        /// <summary>
        /// Gets or sets the find string that contains all our selected withs
        /// </summary>
        public string FindWithString { get; set; }

        /// <summary>
        /// Get the correct property data by FindBY
        /// </summary>
        /// <returns>Correct property data</returns>
        public string GetFindBy()
        {
            var stringBuilder = new StringBuilder();
            foreach (var attributeTagse in FindWith)
            {
                switch (attributeTagse)
                {
                    case AttributeTags.ResourceId:
                        stringBuilder.Append($"ResourceId={AndroidElement.ResourceId},");
                        break;

                    case AttributeTags.ContentDesc:
                        stringBuilder.Append($"ContentDesc={AndroidElement.ContentDesc},");
                        break;

                    case AttributeTags.Text:
                        stringBuilder.Append($"Text={AndroidElement.Text},");
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return stringBuilder.Remove(stringBuilder.Length - 1, 1).ToString();
        }
    }
}
