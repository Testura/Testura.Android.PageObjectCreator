using Testura.Android.PageObjectCreator.Util.Behaviors;
using NUnit.Framework;

namespace Testura.Android.PageObjectCreator.Tests.Util.Behaviors
{
    [TestFixture]
    public class AvalonEditBehaviorTests
    {
        private AvalonEditBehavior avalonEditBehavior;

        [SetUp]
        public void SetUp()
        {
            avalonEditBehavior = new AvalonEditBehavior();
        }
    }
}