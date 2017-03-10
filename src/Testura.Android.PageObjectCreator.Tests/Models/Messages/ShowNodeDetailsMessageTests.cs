using Testura.Android.PageObjectCreator.Models.Messages;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Models.Messages
{
    [TestFixture]
    public class ShowNodeDetailsMessageTests
    {
        private ShowNodeDetailsMessage _showNodeDetailsMessage;

        [SetUp]
        public void SetUp()
        {
            _showNodeDetailsMessage = new ShowNodeDetailsMessage();
        }
    }
}