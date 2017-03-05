using Testura.Android.PageObjectCreator.Models.Messages;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Models.Messages
{
    [TestFixture]
    public class ShowNodeDetailsMessageTests
    {
        private ShowNodeDetailsMessage showNodeDetailsMessage;

        [SetUp]
        public void SetUp()
        {
            showNodeDetailsMessage = new ShowNodeDetailsMessage();
        }
    }
}