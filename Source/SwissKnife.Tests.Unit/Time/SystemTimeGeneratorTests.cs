using System;
using NUnit.Framework;
using SwissKnife.Time;

namespace SwissKnife.Tests.Unit.Time
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class SystemTimeGeneratorTests
    {
        [Test]
        public void LocalNow_ReturnsSystemTimeNow()
        {
            // We will check it up to a precision of a two milliseconds.
            Assert.That<TimeSpan>((new SystemTimeGenerator().LocalNow() - DateTimeOffset.Now).Duration, Is.LessThanOrEqualTo(TimeSpan.FromMilliseconds(2)));
        }

        [Test]
        public void LocalNow_ReturnsTimeWithProperOffset()
        {
            Assert.That(new SystemTimeGenerator().LocalNow().Offset, Is.EqualTo(DateTimeOffset.Now.Offset));
        }
    }
    // ReSharper restore InconsistentNaming
}
