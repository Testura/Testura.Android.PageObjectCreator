using Testura.Android.PageObjectCreator.Views;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Views
{
    [TestFixture]
    public class OperationViewTests
    {
        private OperationView operationView;

        [SetUp]
        public void SetUp()
        {
            operationView = new OperationView();
        }
    }
}