using Testura.Android.PageObjectCreator.Services;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Services
{
    [TestFixture]
    public class FileServiceTests
    {
        private FileService fileService;

        [SetUp]
        public void SetUp()
        {
            fileService = new FileService();
        }
    }
}