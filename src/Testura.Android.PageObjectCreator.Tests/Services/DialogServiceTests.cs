using Testura.Android.PageObjectCreator.Services;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Services
{
    [TestFixture]
    public class DialogServiceTests
    {
        private DialogService _dialogService;

        [SetUp]
        public void SetUp()
        {
            _dialogService = new DialogService();
        }
    }
}