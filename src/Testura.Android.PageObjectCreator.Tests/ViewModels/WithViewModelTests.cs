using Testura.Android.PageObjectCreator.ViewModels;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.ViewModels
{
    [TestFixture]
    public class WithViewModelTests
    {
        private WithViewModel withViewModel;

        [SetUp]
        public void SetUp()
        {
            withViewModel = new WithViewModel();
        }
    }
}