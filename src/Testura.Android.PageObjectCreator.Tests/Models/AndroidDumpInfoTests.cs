using Testura.Android.PageObjectCreator.Models;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Models
{
    [TestFixture]
    public class AndroidDumpInfoTests
    {
        private AndroidDumpInfo _androidDumpInfo;

        [SetUp]
        public void SetUp()
        {
            _androidDumpInfo = new AndroidDumpInfo();
        }
    }
}