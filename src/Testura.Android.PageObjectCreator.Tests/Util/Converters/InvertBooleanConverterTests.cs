using Testura.Android.PageObjectCreator.Util.Converters;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Util.Converters
{
    [TestFixture]
    public class InvertBooleanConverterTests
    {
        private InvertBooleanConverter _invertBooleanConverter;

        [SetUp]
        public void SetUp()
        {
            _invertBooleanConverter = new InvertBooleanConverter();
        }
    }
}