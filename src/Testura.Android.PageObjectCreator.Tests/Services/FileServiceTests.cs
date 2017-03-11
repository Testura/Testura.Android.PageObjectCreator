using Testura.Android.PageObjectCreator.Services;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Services
{
    [TestFixture]
    public class FileServiceTests
    {
        private FileService _fileService;

        [SetUp]
        public void SetUp()
        {
            _fileService = new FileService();
        }
    }
}