using System;
using NUnit.Framework;
using SwissKnife.Time;

namespace SwissKnife.Tests.Unit.Time
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class ConstantTimeGeneratorTests
    {
        [Test]
        public void LocalNow_ReturnsConstantTime()
        {
            var constantTime = DateTimeOffset.Now;
            Assert.That(new ConstantTimeGenerator(constantTime).LocalNow(), Is.EqualTo(constantTime));
        }

        [Test]
        public void ConstantTime_ReturnsConstantTime()
        {
            var constantTime = DateTimeOffset.Now;
            Assert.That(new ConstantTimeGenerator(constantTime).ConstantTime, Is.EqualTo(constantTime));
        }
    }
    // ReSharper restore InconsistentNaming
}
