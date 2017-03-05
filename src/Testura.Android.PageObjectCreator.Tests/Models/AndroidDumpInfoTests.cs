using Testura.Android.PageObjectCreator.Models;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Models
{
    [TestFixture]
    public class AndroidDumpInfoTests
    {
        private AndroidDumpInfo androidDumpInfo;

        [SetUp]
        public void SetUp()
        {
            androidDumpInfo = new AndroidDumpInfo();
        }
    }
}