using NUnit.Framework;
using SwissKnife.Idioms;

namespace SwissKnife.Tests.Unit.Idioms
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public partial class OptionTests
    {
        [Test]
        public void None_CreatesNone()
        {
            var none = Option<object>.None;
            Assert.That(none.IsNone);
        }

        [Test]
        public void Some_CreatesSome()
        {
            var some = Option<object>.Some(new object());
            Assert.That(some.IsSome);
        }

        [Test]
        public void Some_CreatesSomeThatRepresentsValue()
        {
            var value = new object();
            var some = Option<object>.Some(value);
            Assert.That(some.Value, Is.SameAs(value));
        }

        [Test]
        public void From_ValueIsNull_CreatesNone()
        {
            var option = Option<object>.From(null);
            Assert.That(option.IsNone);
        }

        [Test]
        public void From_ValueIsNotNull_CreatesSome()
        {
            var option = Option<object>.From(new object());
            Assert.That(option.IsSome);
        }

        [Test]
        public void From_ValueIsNotNull_CreatesSomeThatRepresentsValue()
        {
            var value = new object();
            var option = Option<object>.From(value);
            Assert.That(option.Value, Is.SameAs(value));
        }
    }
    // ReSharper restore InconsistentNaming
}
