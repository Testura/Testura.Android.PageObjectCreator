using Testura.Android.PageObjectCreator.Util.Converters;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Util.Converters
{
    [TestFixture]
    public class BooleanToVisibilityInvertedConverterTests
    {
        private BooleanToVisibilityInvertedConverter booleanToVisibilityInvertedConverter;

        [SetUp]
        public void SetUp()
        {
            booleanToVisibilityInvertedConverter = new BooleanToVisibilityInvertedConverter();
        }
    }
}