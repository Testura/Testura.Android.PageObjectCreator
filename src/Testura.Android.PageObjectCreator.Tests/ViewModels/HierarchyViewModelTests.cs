using Testura.Android.PageObjectCreator.ViewModels;
using NUnit.Framework;
using Testura.Android.PageObjectCreator.Services;
using Moq;

namespace Testura.Android.PageObjectCreator.Tests.ViewModels
{
    [TestFixture]
    public class HierarchyViewModelTests
    {
        private Mock<IDumpService> dumpServiceMock;
        private Mock<IFileService> fileServiceMock;
        private HierarchyViewModel hierarchyViewModel;

        [SetUp]
        public void SetUp()
        {
            dumpServiceMock = new Mock<IDumpService>();
            fileServiceMock = new Mock<IFileService>();
            hierarchyViewModel = new HierarchyViewModel(dumpServiceMock.Object, fileServiceMock.Object);
        }
    }
}