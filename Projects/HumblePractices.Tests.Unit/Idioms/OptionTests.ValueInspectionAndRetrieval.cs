using System;
using HumblePractices.Idioms;
using NUnit.Framework;

namespace HumblePractices.Tests.Unit.Idioms
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public partial class OptionTests
    {
        [Test]
        public void IsNone_OptionIsNone_ReturnsTrue()
        {
            var none = Option<object>.None;
            Assert.That(none.IsNone, Is.True);
        }

        [Test]
        public void IsNone_OptionIsSome_ReturnsFalse()
        {
            var some = Option<object>.Some(new object());
            Assert.That(some.IsNone, Is.False);
        }

        [Test]
        public void IsSome_OptionIsSome_ReturnsTrue()
        {
            var some = Option<object>.Some(new object());
            Assert.That(some.IsSome, Is.True);
        }

        [Test]
        public void IsSome_OptionIsNone_ReturnsFalse()
        {
            var none = Option<object>.None;
            Assert.That(none.IsSome, Is.False);
        }

        [Test]
        public void Value_OptionIsNone_ThrowsException()
        {
            var none = Option<object>.None;
            var exceptionMessage = Assert.Throws<InvalidOperationException>(() => System.Diagnostics.Debug.Write(none.Value)).Message;
            Assert.That(exceptionMessage, Is.EqualTo("Option must have a value."));
        }

        [Test]
        public void Value_OptionIsSome_ReturnsValue()
        {
            var value = new object();
            var some = Option<object>.Some(value);
            Assert.That(some.Value, Is.SameAs(value));
        }

        [Test]
        public void ValueOrNull_OptionIsNone_ReturnsNull()
        {
            var none = Option<object>.None;
            Assert.That(none.ValueOrNull, Is.Null);
        }

        [Test]
        public void ValueOrNull_OptionIsSome_ReturnsValue()
        {
            var value = new object();
            var some = Option<object>.Some(value);
            Assert.That(some.ValueOrNull, Is.SameAs(value));
        }

        [Test]
        public void ValueOr_OptionIsNone_ReturnsDefaultValue()
        {
            var defaultValue = new object();
            var none = Option<object>.None;
            Assert.That(none.ValueOr(defaultValue), Is.SameAs(defaultValue));
        }

        [Test]
        public void ValueOr_OptionIsSome_ReturnsValue()
        {
            var value = new object();
            var some = Option<object>.Some(value);
            Assert.That(some.ValueOr(new object()), Is.SameAs(value));
        }
    }
    // ReSharper restore InconsistentNaming
}
