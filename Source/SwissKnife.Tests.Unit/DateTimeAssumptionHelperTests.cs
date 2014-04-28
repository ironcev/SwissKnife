using System.Globalization;
using NUnit.Framework;

namespace SwissKnife.Tests.Unit
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class DateTimeAssumptionHelperTests
    {
        #region ToDateTimeStyles
        [Test]
        public void ToDateTimeStyles_AssumeLocal_ReturnsAssumeLocal()
        {
            Assert.That(DateTimeAssumptionHelper.ToDateTimeStyles(DateTimeAssumption.AssumeLocal), Is.EqualTo(DateTimeStyles.AssumeLocal));
        }

        [Test]
        public void ToDateTimeStyles_AssumeUniversal_ReturnsAssumeUniversal()
        {
            Assert.That(DateTimeAssumptionHelper.ToDateTimeStyles(DateTimeAssumption.AssumeUniversal), Is.EqualTo(DateTimeStyles.AssumeUniversal));
        }
        #endregion
    }
    // ReSharper restore InconsistentNaming
}
