using System;
using Moq;
using NUnit.Framework;
using SwissKnife.Time;

namespace SwissKnife.Tests.Unit.Time
{
    /// <remarks>
    /// We will test <see cref="TimeGenerator"/> through the <see cref="ConstantTimeGenerator"/> as it is a
    /// very thin wrapper convenient for testing the base class.
    /// </remarks>
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class TimeGeneratorTests
    {
        [Test]
        public void UtcNow_CallsLocalNow()
        {
            var timeGeneratorMock = new Mock<TimeGenerator>();
            
            timeGeneratorMock.Object.UtcNow();
            
            timeGeneratorMock.Verify(x => x.LocalNow());
        }

        [Test]
        public void UtcNow_ReturnsProperUtcNow()
        {
            var generator = new ConstantTimeGenerator(new DateTimeOffset(1000, 1, 1, 1, 1, 1, TimeSpan.FromHours(1)));

            Assert.That(generator.UtcNow(), Is.EqualTo(new DateTimeOffset(1000, 1, 1, 0, 1, 1, TimeSpan.Zero)));
        }

        [Test]
        public void Today_CallsLocalNow()
        {
            var timeGeneratorMock = new Mock<TimeGenerator>();

            timeGeneratorMock.Object.Today();

            timeGeneratorMock.Verify(x => x.LocalNow());
        }

        [Test]
        public void Today_ReturnsProperToday()
        {
            var generator = new ConstantTimeGenerator(new DateTimeOffset(1000, 1, 1, 1, 1, 1, TimeSpan.FromHours(1)));

            Assert.That(generator.Today(), Is.EqualTo(new DateTime(1000, 1, 1)));
        }
    }
    // ReSharper restore InconsistentNaming
}
