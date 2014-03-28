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
        #region UtcNow
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
        #endregion

        #region Today
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
        #endregion

        #region GetLocalNow
        [Test]
        public void GetLocalNow_Get_InitiallyIsNotNull()
        {
            Assert.That(TimeGenerator.GetLocalNow, Is.Not.Null);
        }

        [Test]
        public void GetLocalNow_Get_InitiallySetToDelegateThatReturnsSystemTimeNow()
        {
            // We will check it up to a precision of a two milliseconds.
            Assert.That<TimeSpan>((TimeGenerator.GetLocalNow() - DateTimeOffset.Now).Duration, Is.LessThanOrEqualTo(TimeSpan.FromMilliseconds(2)));
        }

        [Test]
        public void GetLocalNow_Set_ValueIsNull_ThrowsException()
        {
            var parameterName = Assert.Throws<ArgumentNullException>(() => TimeGenerator.GetLocalNow = null).ParamName;
            Assert.That(parameterName, Is.EqualTo("value"));
        }

        [Test]
        public void GetLocalNow_Set_SetsDelegate()
        {
            Func<DateTimeOffset> @delegate = () => new DateTimeOffset();

            TimeGenerator.GetLocalNow = @delegate;

            Assert.That(TimeGenerator.GetLocalNow, Is.EqualTo(@delegate));
        }
        #endregion

        #region GetUtcNow
        [Test]
        public void GetUtcNow_CallsGetLocalNow()
        {
            int callCounter = 0;
            TimeGenerator.GetLocalNow = () =>
            {
                callCounter++;
                return new DateTimeOffset(1000, 1, 1, 1, 1, 1, TimeSpan.FromHours(1));
            };

            TimeGenerator.GetUtcNow();

            Assert.That(callCounter, Is.EqualTo(1));
        }

        [Test]
        public void GetUtcNow_ReturnsProperUtcNow()
        {
            TimeGenerator.GetLocalNow = () => new DateTimeOffset(1000, 1, 1, 1, 1, 1, TimeSpan.FromHours(1));

            Assert.That(TimeGenerator.GetUtcNow(), Is.EqualTo(new DateTimeOffset(1000, 1, 1, 0, 1, 1, TimeSpan.Zero)));
        }
        #endregion

        #region GetToday
        [Test]
        public void GetToday_CallsGetLocalNow()
        {
            int callCounter = 0;
            TimeGenerator.GetLocalNow = () =>
            {
                callCounter++;
                return new DateTimeOffset(1000, 1, 1, 1, 1, 1, TimeSpan.FromHours(1));
            };

            TimeGenerator.GetToday();

            Assert.That(callCounter, Is.EqualTo(1));
        }

        [Test]
        public void GetToday_ReturnsProperToday()
        {
            TimeGenerator.GetLocalNow = () => new DateTimeOffset(1000, 1, 1, 1, 1, 1, TimeSpan.FromHours(1));

            Assert.That(TimeGenerator.GetToday(), Is.EqualTo(new DateTime(1000, 1, 1)));
        }
        #endregion
    }
    // ReSharper restore InconsistentNaming
}
