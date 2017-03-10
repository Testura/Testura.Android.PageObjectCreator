using Testura.Android.PageObjectCreator.Util.Converters;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Util.Converters
{
    [TestFixture]
    public class BooleanToVisibilityInvertedConverterTests
    {
        private BooleanToVisibilityInvertedConverter _booleanToVisibilityInvertedConverter;

        [SetUp]
        public void SetUp()
        {
            _booleanToVisibilityInvertedConverter = new BooleanToVisibilityInvertedConverter();
        }
    }
}