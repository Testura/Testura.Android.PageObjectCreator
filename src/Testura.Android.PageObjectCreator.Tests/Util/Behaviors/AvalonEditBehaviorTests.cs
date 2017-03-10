using Testura.Android.PageObjectCreator.Util.Behaviors;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Util.Behaviors
{
    [TestFixture]
    public class AvalonEditBehaviorTests
    {
        private AvalonEditBehavior _avalonEditBehavior;

        [SetUp]
        public void SetUp()
        {
            _avalonEditBehavior = new AvalonEditBehavior();
        }
    }
}