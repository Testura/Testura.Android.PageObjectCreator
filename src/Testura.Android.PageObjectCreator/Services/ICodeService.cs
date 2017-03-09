using System.Collections.Generic;
using Testura.Android.PageObjectCreator.Models;

namespace Testura.Android.PageObjectCreator.Services
{
    public interface ICodeService
    {
        /// <summary>
        /// Generate the class, fields and constructor for a page object
        /// </summary>
        /// <param name="pageObejctName">Name of the new page object</param>
        /// <param name="namespace">Name of the namespace to generate</param>
        /// <param name="uiObjects">UiObject inside the class</param>
        /// <returns>The generated code as a string</returns>
        string GeneratePageObject(string pageObejctName, string @namespace, IEnumerable<UiObjectInfo> uiObjects);
    }
}
