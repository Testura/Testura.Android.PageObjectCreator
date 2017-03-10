using Testura.Android.PageObjectCreator.Views;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Views
{
    [TestFixture]
    public class OperationViewTests
    {
        private OperationView _operationView;

        [SetUp]
        public void SetUp()
        {
            _operationView = new OperationView();
        }
    }
}