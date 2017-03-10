using Testura.Android.PageObjectCreator.Services;
using NUnit.Framework;
using Moq;

namespace Testura.Android.PageObjectCreator.Tests.Services
{
    [TestFixture]
    public class ScreenServiceTests
    {
        private ScreenService _screenService;

        [SetUp]
        public void SetUp()
        {
            _screenService = new ScreenService();
        }
    }
}