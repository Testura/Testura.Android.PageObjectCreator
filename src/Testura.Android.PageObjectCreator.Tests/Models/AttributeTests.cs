using NUnit.Framework;
using Attribute = Testura.Android.PageObjectCreator.Models.Attribute;

namespace Testura.Android.PageObjectCreator.Tests.Models
{
    [TestFixture]
    public class AttributeTests
    {
        private Attribute _attribute;

        [SetUp]
        public void SetUp()
        {
            _attribute = new Attribute(string.Empty, string.Empty);
        }
    }
}