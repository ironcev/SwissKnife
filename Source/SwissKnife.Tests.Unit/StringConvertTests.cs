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
        public void ToInt32_ValueIsMinInt32_ReturnsMinInt32()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToInt32(int.MinValue.ToString(CultureInfo.CurrentCulture)).Value, Is.EqualTo(int.MinValue));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToInt32_ValueIsMaxInt32_ReturnsMaxInt32()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToInt32(int.MaxValue.ToString(CultureInfo.CurrentCulture)).Value, Is.EqualTo(int.MaxValue));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToInt32_ValueIsZero_ReturnsZero()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToInt32(0.ToString(CultureInfo.CurrentCulture)).Value, Is.EqualTo(0));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToInt32_ValueIsArbitraryInt32_ReturnsThatInt32()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToInt32(123.ToString(CultureInfo.CurrentCulture)).Value, Is.EqualTo(123));
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
        public void ToInt32OrZero_ValueIsMinInt32_ReturnsMinInt32()
        {
            Assert.That(StringConvert.ToInt32OrZero(int.MinValue.ToString(CultureInfo.CurrentCulture)), Is.EqualTo(int.MinValue));
        }

        [Test]
        public void ToInt32OrZero_ValueIsMaxInt32_ReturnsMaxInt32()
        {
            Assert.That(StringConvert.ToInt32OrZero(int.MaxValue.ToString(CultureInfo.CurrentCulture)), Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void ToInt32OrZero_ValueIsZero_ReturnsZero()
        {
            Assert.That(StringConvert.ToInt32OrZero(0.ToString(CultureInfo.CurrentCulture)), Is.EqualTo(0));
        }

        [Test]
        public void ToInt32OrZero_ValueIsArbitraryInt32_ReturnsThatInt32()
        {
            Assert.That(StringConvert.ToInt32OrZero(123.ToString(CultureInfo.CurrentCulture)), Is.EqualTo(123));
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
        public void ToInt32Or_ValueIsMinInt32_ReturnsMinInt32()
        {
            Assert.That(StringConvert.ToInt32Or(int.MinValue.ToString(CultureInfo.CurrentCulture), 123), Is.EqualTo(int.MinValue));
        }

        [Test]
        public void ToInt32Or_ValueIsMaxInt32_ReturnsMaxInt32()
        {
            Assert.That(StringConvert.ToInt32Or(int.MaxValue.ToString(CultureInfo.CurrentCulture), 123), Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void ToInt32Or_ValueIsZero_ReturnsZero()
        {
            Assert.That(StringConvert.ToInt32Or(0.ToString(CultureInfo.CurrentCulture), 123), Is.EqualTo(0));
        }

        [Test]
        public void ToInt32Or_ValueIsArbitraryInt32_ReturnsThatInt32()
        {
            Assert.That(StringConvert.ToInt32Or(12345.ToString(CultureInfo.CurrentCulture), 123), Is.EqualTo(12345));
        }
        #endregion

        #region ToInt64
        [Test]
        public void ToInt64_ValueIsNone_ReturnsNull()
        {
            Assert.That(StringConvert.ToInt64(null).HasValue, Is.False);
        }

        [Test]
        public void ToInt64_ValueIsEmpty_ReturnsNull()
        {
            Assert.That(StringConvert.ToInt64(string.Empty).HasValue, Is.False);
        }

        [Test]
        public void ToInt64_ValueIsWhiteSpace_ReturnsNull()
        {
            Assert.That(StringConvert.ToInt64(" ").HasValue, Is.False);
        }

        [Test]
        public void ToInt64_ValueIsNotAnInt64_ReturnsNull()
        {
            Assert.That(StringConvert.ToInt64("QWERT").HasValue, Is.False);
        }

        [Test]
        public void ToInt64_ValueIsLessThanMinInt64_ReturnsNull()
        {
            Assert.That(StringConvert.ToInt64(long.MinValue + "0").HasValue, Is.False);
        }

        [Test]
        public void ToInt64_ValueIsGreaterThanMaxInt64_ReturnsNull()
        {
            Assert.That(StringConvert.ToInt64(long.MaxValue + "0").HasValue, Is.False);
        }

        [Test]
        public void ToInt64_ValueIsMinInt64_ReturnsMinInt64()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToInt64(long.MinValue.ToString(CultureInfo.CurrentCulture)).Value, Is.EqualTo(long.MinValue));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToInt64_ValueIsMaxInt64_ReturnsMaxInt54()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToInt64(long.MaxValue.ToString(CultureInfo.CurrentCulture)).Value, Is.EqualTo(long.MaxValue));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToInt64_ValueIsZero_ReturnsZero()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToInt64(0.ToString(CultureInfo.CurrentCulture)).Value, Is.EqualTo(0));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToInt64_ValueIsArbitraryInt64_ReturnsThatInt64()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToInt64(123.ToString(CultureInfo.CurrentCulture)).Value, Is.EqualTo(123));
            // ReSharper restore PossibleInvalidOperationException
        }
        #endregion

        #region ToInt64OrZero
        [Test]
        public void ToInt64OrZero_ValueIsNone_ReturnsZero()
        {
            Assert.That(StringConvert.ToInt64OrZero(null), Is.EqualTo(0));
        }

        [Test]
        public void ToInt64OrZero_ValueIsEmpty_ReturnsZero()
        {
            Assert.That(StringConvert.ToInt64OrZero(string.Empty), Is.EqualTo(0));
        }

        [Test]
        public void ToInt64OrZero_ValueIsWhiteSpace_ReturnsZero()
        {
            Assert.That(StringConvert.ToInt64OrZero(" "), Is.EqualTo(0));
        }

        [Test]
        public void ToInt64OrZero_ValueIsNotAnInt64_ReturnsZero()
        {
            Assert.That(StringConvert.ToInt64OrZero("QWERT"), Is.EqualTo(0));
        }

        [Test]
        public void ToInt64OrZero_ValueIsLessThanMinInt64_ReturnsZero()
        {
            Assert.That(StringConvert.ToInt64OrZero(long.MinValue + "0"), Is.EqualTo(0));
        }

        [Test]
        public void ToInt64OrZero_ValueIsGreaterThanMaxInt64_ReturnsZero()
        {
            Assert.That(StringConvert.ToInt64OrZero(long.MaxValue + "0"), Is.EqualTo(0));
        }

        [Test]
        public void ToInt64OrZero_ValueIsMinInt64_ReturnsMinInt64()
        {
            Assert.That(StringConvert.ToInt64OrZero(long.MinValue.ToString(CultureInfo.CurrentCulture)), Is.EqualTo(long.MinValue));
        }

        [Test]
        public void ToInt64OrZero_ValueIsMaxInt64_ReturnsMaxInt64()
        {
            Assert.That(StringConvert.ToInt64OrZero(long.MaxValue.ToString(CultureInfo.CurrentCulture)), Is.EqualTo(long.MaxValue));
        }

        [Test]
        public void ToInt64OrZero_ValueIsZero_ReturnsZero()
        {
            Assert.That(StringConvert.ToInt64OrZero(0.ToString(CultureInfo.CurrentCulture)), Is.EqualTo(0));
        }

        [Test]
        public void ToInt64OrZero_ValueIsArbitraryInt64_ReturnsThatInt64()
        {
            Assert.That(StringConvert.ToInt64OrZero(123.ToString(CultureInfo.CurrentCulture)), Is.EqualTo(123));
        }
        #endregion

        #region ToInt64Or
        [Test]
        public void ToInt64Or_ValueIsNone_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToInt64Or(null, 123), Is.EqualTo(123));
        }

        [Test]
        public void ToInt64Or_ValueIsEmpty_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToInt64Or(string.Empty, 123), Is.EqualTo(123));
        }

        [Test]
        public void ToInt64Or_ValueIsWhiteSpace_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToInt64Or(" ", 123), Is.EqualTo(123));
        }

        [Test]
        public void ToInt64Or_ValueIsNotAnInt64_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToInt64Or("QWERT", 123), Is.EqualTo(123));
        }

        [Test]
        public void ToInt64Or_ValueIsLessThanMinInt64_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToInt64Or(long.MinValue + "0", 123), Is.EqualTo(123));
        }

        [Test]
        public void ToInt64Or_ValueIsGreaterThanMaxInt64_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToInt64Or(long.MaxValue + "0", 123), Is.EqualTo(123));
        }

        [Test]
        public void ToInt64Or_ValueIsMinInt64_ReturnsMinInt64()
        {
            Assert.That(StringConvert.ToInt64Or(long.MinValue.ToString(CultureInfo.CurrentCulture), 123), Is.EqualTo(long.MinValue));
        }

        [Test]
        public void ToInt64Or_ValueIsMaxInt64_ReturnsMaxInt64()
        {
            Assert.That(StringConvert.ToInt64Or(long.MaxValue.ToString(CultureInfo.CurrentCulture), 123), Is.EqualTo(long.MaxValue));
        }

        [Test]
        public void ToInt64Or_ValueIsZero_ReturnsZero()
        {
            Assert.That(StringConvert.ToInt64Or(0.ToString(CultureInfo.CurrentCulture), 123), Is.EqualTo(0));
        }

        [Test]
        public void ToInt64Or_ValueIsArbitraryInt64_ReturnsThatInt64()
        {
            Assert.That(StringConvert.ToInt64Or(12345.ToString(CultureInfo.CurrentCulture), 123), Is.EqualTo(12345));
        }
        #endregion

        #region ToSingle
        [Test]
        public void ToSingle_ValueIsNone_ReturnsNull()
        {
            Assert.That(StringConvert.ToSingle(null).HasValue, Is.False);
        }

        [Test]
        public void ToSingle_ValueIsEmpty_ReturnsNull()
        {
            Assert.That(StringConvert.ToSingle(string.Empty).HasValue, Is.False);
        }

        [Test]
        public void ToSingle_ValueIsWhiteSpace_ReturnsNull()
        {
            Assert.That(StringConvert.ToSingle(" ").HasValue, Is.False);
        }

        [Test]
        public void ToSingle_ValueIsNotAnSingle_ReturnsNull()
        {
            Assert.That(StringConvert.ToSingle("QWERT").HasValue, Is.False);
        }

        [Test]
        public void ToSingle_ValueIsLessThanMinSingle_ReturnsNull()
        {
            Assert.That(StringConvert.ToSingle("-1" + float.MinValue.ToString("F").Remove(0,1)).HasValue, Is.False);
        }

        [Test]
        public void ToSingle_ValueIsGreaterThanMaxSingle_ReturnsNull()
        {
            Assert.That(StringConvert.ToSingle("1" + float.MaxValue.ToString("F")).HasValue, Is.False);
        }

        [Test]
        public void ToSingle_ValueIsMinSingle_ReturnsMinSingle()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToSingle(float.MinValue.ToString("E10")).Value, Is.EqualTo(float.MinValue));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToSingle_ValueIsMaxSingle_ReturnsMaxSingle()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToSingle(float.MaxValue.ToString("E10")).Value, Is.EqualTo(float.MaxValue));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToSingle_ValueIsZero_ReturnsZero()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToSingle(0f.ToString(CultureInfo.CurrentCulture)).Value, Is.EqualTo(0f));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToSingle_ValueIsArbitrarySingle_ReturnsThatSingle()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToSingle(123.456f.ToString(CultureInfo.CurrentCulture)).Value, Is.EqualTo(123.456f));
            // ReSharper restore PossibleInvalidOperationException
        }
        #endregion

        #region ToSingleOrZero
        [Test]
        public void ToSingleOrZero_ValueIsNone_ReturnsZero()
        {
            Assert.That(StringConvert.ToSingleOrZero(null), Is.EqualTo(0f));
        }

        [Test]
        public void ToSingleOrZero_ValueIsEmpty_ReturnsZero()
        {
            Assert.That(StringConvert.ToSingleOrZero(string.Empty), Is.EqualTo(0f));
        }

        [Test]
        public void ToSingleOrZero_ValueIsWhiteSpace_ReturnsZero()
        {
            Assert.That(StringConvert.ToSingleOrZero(" "), Is.EqualTo(0f));
        }

        [Test]
        public void ToSingleOrZero_ValueIsNotAnSingle_ReturnsZero()
        {
            Assert.That(StringConvert.ToSingleOrZero("QWERT"), Is.EqualTo(0f));
        }

        [Test]
        public void ToSingleOrZero_ValueIsLessThanMinSingle_ReturnsZero()
        {
            Assert.That(StringConvert.ToSingleOrZero("-1" + float.MinValue.ToString("F").Remove(0, 1)), Is.EqualTo(0f));
        }

        [Test]
        public void ToSingleOrZero_ValueIsGreaterThanMaxSingle_ReturnsZero()
        {
            Assert.That(StringConvert.ToSingleOrZero("1" + float.MaxValue.ToString("F")), Is.EqualTo(0f));
        }

        [Test]
        public void ToSingleOrZero_ValueIsMinSingle_ReturnsMinSingle()
        {
            Assert.That(StringConvert.ToSingleOrZero(float.MinValue.ToString("E10")), Is.EqualTo(float.MinValue));
        }

        [Test]
        public void ToSingleOrZero_ValueIsMaxSingle_ReturnsMaxSingle()
        {
            Assert.That(StringConvert.ToSingleOrZero(float.MaxValue.ToString("E10")), Is.EqualTo(float.MaxValue));
        }

        [Test]
        public void ToSingleOrZero_ValueIsZero_ReturnsZero()
        {
            Assert.That(StringConvert.ToSingleOrZero(0.ToString(CultureInfo.CurrentCulture)), Is.EqualTo(0f));
        }

        [Test]
        public void ToSingleOrZero_ValueIsArbitrarySingle_ReturnsThatSingle()
        {
            Assert.That(StringConvert.ToSingleOrZero(123.456f.ToString(CultureInfo.CurrentCulture)), Is.EqualTo(123.456f));
        }
        #endregion

        #region ToSingleOr
        [Test]
        public void ToSingleOr_ValueIsNone_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToSingleOr(null, 123.456f), Is.EqualTo(123.456f));
        }

        [Test]
        public void ToSingleOr_ValueIsEmpty_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToSingleOr(string.Empty, 123.456f), Is.EqualTo(123.456f));
        }

        [Test]
        public void ToSingleOr_ValueIsWhiteSpace_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToSingleOr(" ", 123.456f), Is.EqualTo(123.456f));
        }

        [Test]
        public void ToSingleOr_ValueIsNotAnSingle_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToSingleOr("QWERT", 123.456f), Is.EqualTo(123.456f));
        }

        [Test]
        public void ToSingleOr_ValueIsLessThanMinSingle_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToSingleOr("-1" + float.MinValue.ToString("F").Remove(0, 1), 123.456f), Is.EqualTo(123.456f));
        }

        [Test]
        public void ToSingleOr_ValueIsGreaterThanMaxSingle_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToSingleOr("1" + float.MaxValue.ToString("F"), 123.456f), Is.EqualTo(123.456f));
        }

        [Test]
        public void ToSingleOr_ValueIsMinSingle_ReturnsMinSingle()
        {
            Assert.That(StringConvert.ToSingleOr(float.MinValue.ToString("E10"), 123.456f), Is.EqualTo(float.MinValue));
        }

        [Test]
        public void ToSingleOr_ValueIsMaxSingle_ReturnsMaxSingle()
        {
            Assert.That(StringConvert.ToSingleOr(float.MaxValue.ToString("E10"), 123.456f), Is.EqualTo(float.MaxValue));
        }

        [Test]
        public void ToSingleOr_ValueIsZero_ReturnsZero()
        {
            Assert.That(StringConvert.ToSingleOr(0f.ToString(CultureInfo.CurrentCulture), 123.456f), Is.EqualTo(0f));
        }

        [Test]
        public void ToSingleOr_ValueIsArbitrarySingle_ReturnsThatSingle()
        {
            Assert.That(StringConvert.ToSingleOr(654.321f.ToString(CultureInfo.CurrentCulture), 123.456f), Is.EqualTo(654.321f));
        }
        #endregion
    }
    // ReSharper restore InconsistentNaming
}
