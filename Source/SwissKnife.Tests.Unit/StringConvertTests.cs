using System.Globalization;
using NUnit.Framework;

namespace SwissKnife.Tests.Unit
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class StringConvertTests
    {
        #region ToInt32
        [Test]
        public void ToInt32_ValueIsNone_ReturnsNull()
        {
            Assert.That(StringConvert.ToInt32(null).HasValue, Is.False);
        }

        [Test]
        public void ToInt32_ValueIsEmpty_ReturnsNull()
        {
            Assert.That(StringConvert.ToInt32(string.Empty).HasValue, Is.False);
        }

        [Test]
        public void ToInt32_ValueIsWhiteSpace_ReturnsNull()
        {
            Assert.That(StringConvert.ToInt32(" ").HasValue, Is.False);
        }

        [Test]
        public void ToInt32_ValueIsNotAnInt32_ReturnsNull()
        {
            Assert.That(StringConvert.ToInt32("QWERT").HasValue, Is.False);
        }

        [Test]
        public void ToInt32_ValueIsLessThanMinInt32_ReturnsNull()
        {
            Assert.That(StringConvert.ToInt32(int.MinValue + "0").HasValue, Is.False);
        }

        [Test]
        public void ToInt32_ValueIsGreaterThanMaxInt32_ReturnsNull()
        {
            Assert.That(StringConvert.ToInt32(int.MaxValue + "0").HasValue, Is.False);
        }

        [Test]
        public void ToInt32_ValueIsMinInt32_ReturnsMinInteger()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToInt32(int.MinValue.ToString(CultureInfo.InvariantCulture)).Value, Is.EqualTo(int.MinValue));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToInt32_ValueIsMaxInt32_ReturnsMaxInteger()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToInt32(int.MaxValue.ToString(CultureInfo.InvariantCulture)).Value, Is.EqualTo(int.MaxValue));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToInt32_ValueIsZero_ReturnsZero()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToInt32(0.ToString(CultureInfo.InvariantCulture)).Value, Is.EqualTo(0));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToInt32_ValueIsArbitraryInt32_ReturnsThatInt32()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToInt32(123.ToString(CultureInfo.InvariantCulture)).Value, Is.EqualTo(123));
            // ReSharper restore PossibleInvalidOperationException
        }
        #endregion

        #region ToInt32OrZero
        [Test]
        public void ToInt32OrZero_ValueIsNone_ReturnsZero()
        {
            Assert.That(StringConvert.ToInt32OrZero(null), Is.EqualTo(0));
        }

        [Test]
        public void ToInt32OrZero_ValueIsEmpty_ReturnsZero()
        {
            Assert.That(StringConvert.ToInt32OrZero(string.Empty), Is.EqualTo(0));
        }

        [Test]
        public void ToInt32OrZero_ValueIsWhiteSpace_ReturnsZero()
        {
            Assert.That(StringConvert.ToInt32OrZero(" "), Is.EqualTo(0));
        }

        [Test]
        public void ToInt32OrZero_ValueIsNotAnInt32_ReturnsZero()
        {
            Assert.That(StringConvert.ToInt32OrZero("QWERT"), Is.EqualTo(0));
        }

        [Test]
        public void ToInt32OrZero_ValueIsLessThanMinInt32_ReturnsZero()
        {
            Assert.That(StringConvert.ToInt32OrZero(int.MinValue + "0"), Is.EqualTo(0));
        }

        [Test]
        public void ToInt32OrZero_ValueIsGreaterThanMaxInt32_ReturnsZero()
        {
            Assert.That(StringConvert.ToInt32OrZero(int.MaxValue + "0"), Is.EqualTo(0));
        }

        [Test]
        public void ToInt32OrZero_ValueIsMinInt32_ReturnsMinInteger()
        {
            Assert.That(StringConvert.ToInt32OrZero(int.MinValue.ToString(CultureInfo.InvariantCulture)), Is.EqualTo(int.MinValue));
        }

        [Test]
        public void ToInt32OrZero_ValueIsMaxInt32_ReturnsMaxInteger()
        {
            Assert.That(StringConvert.ToInt32OrZero(int.MaxValue.ToString(CultureInfo.InvariantCulture)), Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void ToInt32OrZero_ValueIsZero_ReturnsZero()
        {
            Assert.That(StringConvert.ToInt32OrZero(0.ToString(CultureInfo.InvariantCulture)), Is.EqualTo(0));
        }

        [Test]
        public void ToInt32OrZero_ValueIsArbitraryInt32_ReturnsThatInt32()
        {
            Assert.That(StringConvert.ToInt32OrZero(123.ToString(CultureInfo.InvariantCulture)), Is.EqualTo(123));
        }
        #endregion

        #region ToInt32Or
        [Test]
        public void ToInt32Or_ValueIsNone_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToInt32Or(null, 123), Is.EqualTo(123));
        }

        [Test]
        public void ToInt32Or_ValueIsEmpty_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToInt32Or(string.Empty, 123), Is.EqualTo(123));
        }

        [Test]
        public void ToInt32Or_ValueIsWhiteSpace_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToInt32Or(" ", 123), Is.EqualTo(123));
        }

        [Test]
        public void ToInt32Or_ValueIsNotAnInt32_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToInt32Or("QWERT", 123), Is.EqualTo(123));
        }

        [Test]
        public void ToInt32Or_ValueIsLessThanMinInt32_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToInt32Or(int.MinValue + "0", 123), Is.EqualTo(123));
        }

        [Test]
        public void ToInt32Or_ValueIsGreaterThanMaxInt32_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToInt32Or(int.MaxValue + "0", 123), Is.EqualTo(123));
        }

        [Test]
        public void ToInt32Or_ValueIsMinInt32_ReturnsMinInteger()
        {
            Assert.That(StringConvert.ToInt32Or(int.MinValue.ToString(CultureInfo.InvariantCulture), 123), Is.EqualTo(int.MinValue));
        }

        [Test]
        public void ToInt32DefaultValue_ValueIsMaxInt32_ReturnsMaxInteger()
        {
            Assert.That(StringConvert.ToInt32Or(int.MaxValue.ToString(CultureInfo.InvariantCulture), 123), Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void ToInt32Or_ValueIsZero_ReturnsZero()
        {
            Assert.That(StringConvert.ToInt32Or(0.ToString(CultureInfo.InvariantCulture), 123), Is.EqualTo(0));
        }

        [Test]
        public void ToInt32Or_ValueIsArbitraryInt32_ReturnsThatInt32()
        {
            Assert.That(StringConvert.ToInt32Or(12345.ToString(CultureInfo.InvariantCulture), 123), Is.EqualTo(12345));
        }
        #endregion
    }
    // ReSharper restore InconsistentNaming
}
