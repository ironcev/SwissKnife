using System;
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
        public void ToInt32_ValueIsNotInt32_ReturnsNull()
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
        public void ToInt32OrZero_ValueIsNotInt32_ReturnsZero()
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
        public void ToInt32Or_ValueIsNotInt32_ReturnsDefaultValue()
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
        public void ToInt64_ValueIsNotInt64_ReturnsNull()
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
        public void ToInt64OrZero_ValueIsNotInt64_ReturnsZero()
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
        public void ToInt64Or_ValueIsNotInt64_ReturnsDefaultValue()
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
        public void ToSingle_ValueIsNotSingle_ReturnsNull()
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

        [Test]
        public void ToSingle_ValueIsArbitraryNonNegativeSingleInScientificNotation_ReturnsThatSingle()
        {
            const float value = 1.234e20f;
            string valueAsString = value.ToString("E10");
            Assert.That(valueAsString.ToLowerInvariant().Contains("e"));

            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToSingle(valueAsString).Value, Is.EqualTo(value));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToSingle_ValueIsArbitraryNegativeSingleInScientificNotation_ReturnsThatSingle()
        {
            const float value = -1.234e20f;
            string valueAsString = value.ToString("E10");
            Assert.That(valueAsString.StartsWith("-"));
            Assert.That(valueAsString.ToLowerInvariant().Contains("e"));

            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToSingle(valueAsString).Value, Is.EqualTo(value));
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
        public void ToSingleOrZero_ValueIsNotSingle_ReturnsZero()
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

        [Test]
        public void ToSingleOrZero_ValueIsArbitraryNonNegativeSingleInScientificNotation_ReturnsThatSingle()
        {
            const float value = 1.234e20f;
            string valueAsString = value.ToString("E10");
            Assert.That(valueAsString.ToLowerInvariant().Contains("e"));

            Assert.That(StringConvert.ToSingleOrZero(valueAsString), Is.EqualTo(value));
        }

        [Test]
        public void ToSingleOrZero_ValueIsArbitraryNegativeSingleInScientificNotation_ReturnsThatSingle()
        {
            const float value = -1.234e20f;
            string valueAsString = value.ToString("E10");
            Assert.That(valueAsString.StartsWith("-"));
            Assert.That(valueAsString.ToLowerInvariant().Contains("e"));

            Assert.That(StringConvert.ToSingleOrZero(valueAsString), Is.EqualTo(value));
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
        public void ToSingleOr_ValueIsNotSingle_ReturnsDefaultValue()
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

        [Test]
        public void ToSingleOr_ValueIsArbitraryNonNegativeSingleInScientificNotation_ReturnsThatSingle()
        {
            const float value = 1.234e20f;
            string valueAsString = value.ToString("E10");
            Assert.That(valueAsString.ToLowerInvariant().Contains("e"));

            Assert.That(StringConvert.ToSingleOr(valueAsString, 123.4556f), Is.EqualTo(value));
        }

        [Test]
        public void ToSingleOr_ValueIsArbitraryNegativeSingleInScientificNotation_ReturnsThatSingle()
        {
            const float value = -1.234e20f;
            string valueAsString = value.ToString("E10");
            Assert.That(valueAsString.StartsWith("-"));
            Assert.That(valueAsString.ToLowerInvariant().Contains("e"));

            Assert.That(StringConvert.ToSingleOr(valueAsString, 123.4556f), Is.EqualTo(value));
        }
        #endregion

        #region ToDouble
        [Test]
        public void ToDouble_ValueIsNone_ReturnsNull()
        {
            Assert.That(StringConvert.ToDouble(null).HasValue, Is.False);
        }

        [Test]
        public void ToDouble_ValueIsEmpty_ReturnsNull()
        {
            Assert.That(StringConvert.ToDouble(string.Empty).HasValue, Is.False);
        }

        [Test]
        public void ToDouble_ValueIsWhiteSpace_ReturnsNull()
        {
            Assert.That(StringConvert.ToDouble(" ").HasValue, Is.False);
        }

        [Test]
        public void ToDouble_ValueIsNotDouble_ReturnsNull()
        {
            Assert.That(StringConvert.ToDouble("QWERT").HasValue, Is.False);
        }

        [Test]
        public void ToDouble_ValueIsLessThanMinDouble_ReturnsNull()
        {
            Assert.That(StringConvert.ToDouble("-1" + double.MinValue.ToString("F").Remove(0, 1)).HasValue, Is.False);
        }

        [Test]
        public void ToDouble_ValueIsGreaterThanMaxDouble_ReturnsNull()
        {
            Assert.That(StringConvert.ToDouble("1" + double.MaxValue.ToString("F")).HasValue, Is.False);
        }

        [Test]
        public void ToDouble_ValueIsMinDouble_ReturnsMinDouble()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToDouble(double.MinValue.ToString("E20")).Value, Is.EqualTo(double.MinValue));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToDouble_ValueIsMaxDouble_ReturnsMaxDouble()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToDouble(double.MaxValue.ToString("E20")).Value, Is.EqualTo(double.MaxValue));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToDouble_ValueIsZero_ReturnsZero()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToDouble(0d.ToString(CultureInfo.CurrentCulture)).Value, Is.EqualTo(0d));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToDouble_ValueIsArbitraryDouble_ReturnsThatDouble()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToDouble(123.456d.ToString(CultureInfo.CurrentCulture)).Value, Is.EqualTo(123.456d));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToDouble_ValueIsArbitraryNonNegativeSingleInScientificNotation_ReturnsThatDouble()
        {
            const double value = 1.234e20d;
            string valueAsString = value.ToString("E10");
            Assert.That(valueAsString.ToLowerInvariant().Contains("e"));

            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToDouble(valueAsString).Value, Is.EqualTo(value));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToDouble_ValueIsArbitraryNegativeDoubleInScientificNotation_ReturnsThatDouble()
        {
            const double value = -1.234e20d;
            string valueAsString = value.ToString("E10");
            Assert.That(valueAsString.StartsWith("-"));
            Assert.That(valueAsString.ToLowerInvariant().Contains("e"));

            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToDouble(valueAsString).Value, Is.EqualTo(value));
            // ReSharper restore PossibleInvalidOperationException
        }
        #endregion

        #region ToDoubleOrZero
        [Test]
        public void ToDoubleOrZero_ValueIsNone_ReturnsZero()
        {
            Assert.That(StringConvert.ToDoubleOrZero(null), Is.EqualTo(0d));
        }

        [Test]
        public void ToDoubleOrZero_ValueIsEmpty_ReturnsZero()
        {
            Assert.That(StringConvert.ToDoubleOrZero(string.Empty), Is.EqualTo(0d));
        }

        [Test]
        public void ToDoubleOrZero_ValueIsWhiteSpace_ReturnsZero()
        {
            Assert.That(StringConvert.ToDoubleOrZero(" "), Is.EqualTo(0d));
        }

        [Test]
        public void ToDoubleOrZero_ValueIsNotDouble_ReturnsZero()
        {
            Assert.That(StringConvert.ToDoubleOrZero("QWERT"), Is.EqualTo(0d));
        }

        [Test]
        public void ToDoubleOrZero_ValueIsLessThanMinDouble_ReturnsZero()
        {
            Assert.That(StringConvert.ToDoubleOrZero("-1" + double.MinValue.ToString("F").Remove(0, 1)), Is.EqualTo(0d));
        }

        [Test]
        public void ToDoubleOrZero_ValueIsGreaterThanMaxDouble_ReturnsZero()
        {
            Assert.That(StringConvert.ToDoubleOrZero("1" + double.MaxValue.ToString("F")), Is.EqualTo(0d));
        }

        [Test]
        public void ToDoubleOrZero_ValueIsMinDouble_ReturnsMinDouble()
        {
            Assert.That(StringConvert.ToDoubleOrZero(double.MinValue.ToString("E20")), Is.EqualTo(double.MinValue));
        }

        [Test]
        public void ToDoubleOrZero_ValueIsMaxDouble_ReturnsMaxDouble()
        {
            Assert.That(StringConvert.ToDoubleOrZero(double.MaxValue.ToString("E20")), Is.EqualTo(double.MaxValue));
        }

        [Test]
        public void ToDoubleOrZero_ValueIsZero_ReturnsZero()
        {
            Assert.That(StringConvert.ToDoubleOrZero(0.ToString(CultureInfo.CurrentCulture)), Is.EqualTo(0d));
        }

        [Test]
        public void ToDoubleOrZero_ValueIsArbitraryDouble_ReturnsThatDouble()
        {
            Assert.That(StringConvert.ToDoubleOrZero(123.456d.ToString(CultureInfo.CurrentCulture)), Is.EqualTo(123.456d));
        }

        [Test]
        public void ToDoubleOrZero_ValueIsArbitraryNonNegativeDoubleInScientificNotation_ReturnsThatDouble()
        {
            const double value = 1.234e20d;
            string valueAsString = value.ToString("E10");
            Assert.That(valueAsString.ToLowerInvariant().Contains("e"));

            Assert.That(StringConvert.ToDoubleOrZero(valueAsString), Is.EqualTo(value));
        }

        [Test]
        public void ToDoubleOrZero_ValueIsArbitraryNegativeDoubleInScientificNotation_ReturnsThatDouble()
        {
            const double value = -1.234e20d;
            string valueAsString = value.ToString("E10");
            Assert.That(valueAsString.StartsWith("-"));
            Assert.That(valueAsString.ToLowerInvariant().Contains("e"));

            Assert.That(StringConvert.ToDoubleOrZero(valueAsString), Is.EqualTo(value));
        }
        #endregion

        #region ToDoubleOr
        [Test]
        public void ToDoubleOr_ValueIsNone_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToDoubleOr(null, 123.456d), Is.EqualTo(123.456d));
        }

        [Test]
        public void ToDoubleOr_ValueIsEmpty_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToDoubleOr(string.Empty, 123.456d), Is.EqualTo(123.456d));
        }

        [Test]
        public void ToDoubleOr_ValueIsWhiteSpace_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToDoubleOr(" ", 123.456d), Is.EqualTo(123.456d));
        }

        [Test]
        public void ToDoubleOr_ValueIsNotDouble_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToDoubleOr("QWERT", 123.456d), Is.EqualTo(123.456d));
        }

        [Test]
        public void ToDoubleOr_ValueIsLessThanMinDouble_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToDoubleOr("-1" + double.MinValue.ToString("F").Remove(0, 1), 123.456d), Is.EqualTo(123.456d));
        }

        [Test]
        public void ToDoubleOr_ValueIsGreaterThanMaxDouble_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToDoubleOr("1" + double.MaxValue.ToString("F"), 123.456d), Is.EqualTo(123.456d));
        }

        [Test]
        public void ToDoubleOr_ValueIsMinDouble_ReturnsMinDouble()
        {
            Assert.That(StringConvert.ToDoubleOr(double.MinValue.ToString("E20"), 123.456d), Is.EqualTo(double.MinValue));
        }

        [Test]
        public void ToDoubleOr_ValueIsMaxDouble_ReturnsMaxDouble()
        {
            Assert.That(StringConvert.ToDoubleOr(double.MaxValue.ToString("E20"), 123.456d), Is.EqualTo(double.MaxValue));
        }

        [Test]
        public void ToDoubleOr_ValueIsZero_ReturnsZero()
        {
            Assert.That(StringConvert.ToDoubleOr(0d.ToString(CultureInfo.CurrentCulture), 123.456d), Is.EqualTo(0d));
        }

        [Test]
        public void ToDoubleOr_ValueIsArbitraryDouble_ReturnsThatDouble()
        {
            Assert.That(StringConvert.ToDoubleOr(654.321d.ToString(CultureInfo.CurrentCulture), 123.456d), Is.EqualTo(654.321d));
        }

        [Test]
        public void ToDoubleOr_ValueIsArbitraryNonNegativeDoubleInScientificNotation_ReturnsThatDouble()
        {
            const double value = 1.234e20d;
            string valueAsString = value.ToString("E10");
            Assert.That(valueAsString.ToLowerInvariant().Contains("e"));

            Assert.That(StringConvert.ToDoubleOr(valueAsString, 123.4556f), Is.EqualTo(value));
        }

        [Test]
        public void ToDoubleOr_ValueIsArbitraryNegativeDoubleInScientificNotation_ReturnsThatDouble()
        {
            const double value = -1.234e20d;
            string valueAsString = value.ToString("E10");
            Assert.That(valueAsString.StartsWith("-"));
            Assert.That(valueAsString.ToLowerInvariant().Contains("e"));

            Assert.That(StringConvert.ToDoubleOr(valueAsString, 123.4556f), Is.EqualTo(value));
        }
        #endregion

        #region ToBoolean
        [Test]
        public void ToBoolean_ValueIsNone_ReturnsNull()
        {
            Assert.That(StringConvert.ToBoolean(null).HasValue, Is.False);
        }

        [Test]
        public void ToBoolean_ValueIsEmpty_ReturnsNull()
        {
            Assert.That(StringConvert.ToBoolean(string.Empty).HasValue, Is.False);
        }

        [Test]
        public void ToBoolean_ValueIsWhiteSpace_ReturnsNull()
        {
            Assert.That(StringConvert.ToBoolean(" ").HasValue, Is.False);
        }

        [Test]
        public void ToBoolean_ValueIsNotBoolean_ReturnsNull()
        {
            Assert.That(StringConvert.ToBoolean("QWERT").HasValue, Is.False);
        }

        [Test]
        public void ToBoolean_ValueIsZero_ReturnsFalse()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToBoolean("0").Value, Is.False);
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToBoolean_ValueIsOne_ReturnsTrue()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToBoolean("1").Value, Is.True);
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToBoolean_ValueIsZeroWithTrailing_ReturnsFalse()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToBoolean(" 0 ").Value, Is.False);
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToBoolean_ValueIsOneWithTrailing_ReturnsTrue()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToBoolean(" 1 ").Value, Is.True);
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToBoolean_ValueIsFalseString_ReturnsFalse()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToBoolean(bool.FalseString).Value, Is.False);
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToBoolean_ValueIsTrueString_ReturnsTrue()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToBoolean(bool.TrueString).Value, Is.True);
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToBoolean_ValueIsFalseStringWithTrailing_ReturnsFalse()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToBoolean(" " + bool.FalseString + " ").Value, Is.False);
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToBoolean_ValueIsTrueStringWithTrailing_ReturnsTrue()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToBoolean(" " + bool.TrueString + " ").Value, Is.True);
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToBoolean_ValueIsNonNegativeIntegerDifferentThanOne_ReturnsNull()
        {
            Assert.That(StringConvert.ToBoolean("10").HasValue, Is.False);
        }

        [Test]
        public void ToBoolean_ValueIsNegativeInteger_ReturnsNull()
        {
            Assert.That(StringConvert.ToBoolean("-1").HasValue, Is.False);
        }

        [Test]
        public void ToBoolean_IsCaseInsensitive()
        {
            // ReSharper disable PossibleInvalidOperationException
            foreach (var value in new[] { "false", "False", "FALSE", "fAlSe" })
                Assert.That(StringConvert.ToBoolean(value).Value, Is.False);

            foreach (var value in new[] { "true", "True", "TRUE", "tRuE" })
                Assert.That(StringConvert.ToBoolean(value).Value, Is.True);
            // ReSharper restore PossibleInvalidOperationException
        }
        #endregion

        #region ToBooleanOr
        [Test]
        public void ToBooleanOr_ValueIsNone_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToBooleanOr(null, true), Is.True);
            Assert.That(StringConvert.ToBooleanOr(null, false), Is.False);
        }

        [Test]
        public void ToBooleanOr_ValueIsEmpty_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToBooleanOr(string.Empty, true), Is.True);
            Assert.That(StringConvert.ToBooleanOr(string.Empty, false), Is.False);
        }

        [Test]
        public void ToBooleanOr_ValueIsWhiteSpace_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToBooleanOr(" ", true), Is.True);
            Assert.That(StringConvert.ToBooleanOr(" ", false), Is.False);
        }

        [Test]
        public void ToBooleanOr_ValueIsNotBoolean_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToBooleanOr("QWERT", true), Is.True);
            Assert.That(StringConvert.ToBooleanOr("QWERT", false), Is.False);
        }

        [Test]
        public void ToBooleanOr_ValueIsZero_ReturnsFalse()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToBooleanOr("0", true), Is.False);
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToBooleanOr_ValueIsOne_ReturnsTrue()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToBooleanOr("1", false), Is.True);
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToBooleanOr_ValueIsZeroWithTrailing_ReturnsFalse()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToBooleanOr(" 0 ", true), Is.False);
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToBooleanOr_ValueIsOneWithTrailing_ReturnsTrue()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToBooleanOr(" 1 ", false), Is.True);
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToBooleanOr_ValueIsFalseString_ReturnsFalse()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToBooleanOr(bool.FalseString, true), Is.False);
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToBooleanOr_ValueIsTrueString_ReturnsTrue()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToBooleanOr(bool.TrueString, false), Is.True);
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToBooleanOr_ValueIsFalseStringWithTrailing_ReturnsFalse()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToBooleanOr(" " + bool.FalseString + " ", true), Is.False);
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToBooleanOr_ValueIsTrueStringWithTrailing_ReturnsTrue()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToBooleanOr(" " + bool.TrueString + " ", false), Is.True);
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToBooleanOr_ValueIsNonNegativeIntegerDifferentThanOne_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToBooleanOr("10", true), Is.True);
            Assert.That(StringConvert.ToBooleanOr("10", false), Is.False);
        }

        [Test]
        public void ToBooleanOr_ValueIsNegativeInteger_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToBooleanOr("-1", true), Is.True);
            Assert.That(StringConvert.ToBooleanOr("-1", false), Is.False);
        }

        [Test]
        public void ToBooleanOr_IsCaseInsensitive()
        {
            // ReSharper disable PossibleInvalidOperationException
            foreach (var value in new[] { "false", "False", "FALSE", "fAlSe" })
                Assert.That(StringConvert.ToBooleanOr(value, true), Is.False);

            foreach (var value in new[] { "true", "True", "TRUE", "tRuE" })
                Assert.That(StringConvert.ToBooleanOr(value, false), Is.True);
            // ReSharper restore PossibleInvalidOperationException
        }
        #endregion

        #region ToGuid
        [Test]
        public void ToGuid_ValueIsNone_ReturnsNull()
        {
            Assert.That(StringConvert.ToGuid(null).HasValue, Is.False);
        }

        [Test]
        public void ToGuid_ValueIsEmpty_ReturnsNull()
        {
            Assert.That(StringConvert.ToGuid(string.Empty).HasValue, Is.False);
        }

        [Test]
        public void ToGuid_ValueIsWhiteSpace_ReturnsNull()
        {
            Assert.That(StringConvert.ToGuid(" ").HasValue, Is.False);
        }

        [Test]
        public void ToGuid_ValueIsNotGuid_ReturnsNull()
        {
            Assert.That(StringConvert.ToGuid("QWERT").HasValue, Is.False);
        }

        [Test]
        public void ToGuid_ValueIsEmptyGuid_ReturnsEmptyGuid()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToGuid(Guid.Empty.ToString()).Value, Is.EqualTo(Guid.Empty));
            // ReSharper restore PossibleInvalidOperationException
        }

        private static readonly Guid arbitraryGuid = Guid.NewGuid();

        [Test]
        public void ToGuid_ValueIsArbitraryGuidFormatedWithN_ReturnsThatGuid()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToGuid(arbitraryGuid.ToString("N")).Value, Is.EqualTo(arbitraryGuid));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToGuid_ValueIsArbitraryGuidFormatedWithD_ReturnsThatGuid()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToGuid(arbitraryGuid.ToString("D")).Value, Is.EqualTo(arbitraryGuid));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToGuid_ValueIsArbitraryGuidFormatedWithB_ReturnsThatGuid()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToGuid(arbitraryGuid.ToString("B")).Value, Is.EqualTo(arbitraryGuid));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToGuid_ValueIsArbitraryGuidFormatedWithP_ReturnsThatGuid()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToGuid(arbitraryGuid.ToString("P")).Value, Is.EqualTo(arbitraryGuid));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToGuid_ValueIsArbitraryGuidFormatedWithX_ReturnsThatGuid()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToGuid(arbitraryGuid.ToString("X")).Value, Is.EqualTo(arbitraryGuid));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToGuid_ValueIsArbitraryGuidRepresentedAsShortString_ReturnsThatGuid()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToGuid(GuidUtility.ToShortString(arbitraryGuid)).Value, Is.EqualTo(arbitraryGuid));
            // ReSharper restore PossibleInvalidOperationException
        }
        #endregion

        #region ToGuidOr
        [Test]
        public void ToGuidOr_ValueIsNone_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToGuidOr(null, arbitraryGuid), Is.EqualTo(arbitraryGuid));
        }

        [Test]
        public void ToGuidOr_ValueIsEmpty_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToGuidOr(string.Empty, arbitraryGuid), Is.EqualTo(arbitraryGuid));
        }

        [Test]
        public void ToGuidOr_ValueIsWhiteSpace_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToGuidOr(" ", arbitraryGuid), Is.EqualTo(arbitraryGuid));
        }

        [Test]
        public void ToGuidOr_ValueIsNotGuid_ReturnsDefaultValue()
        {
            Assert.That(StringConvert.ToGuidOr("QWERT", arbitraryGuid), Is.EqualTo(arbitraryGuid));
        }

        [Test]
        public void ToGuidOr_ValueIsEmptyGuid_ReturnsEmptyGuid()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToGuidOr(Guid.Empty.ToString(), arbitraryGuid), Is.EqualTo(Guid.Empty));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToGuidOr_ValueIsArbitraryGuidFormatedWithN_ReturnsThatGuid()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToGuidOr(arbitraryGuid.ToString("N"), Guid.Empty), Is.EqualTo(arbitraryGuid));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToGuidOr_ValueIsArbitraryGuidFormatedWithD_ReturnsThatGuid()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToGuidOr(arbitraryGuid.ToString("D"), Guid.Empty), Is.EqualTo(arbitraryGuid));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToGuidOr_ValueIsArbitraryGuidFormatedWithB_ReturnsThatGuid()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToGuidOr(arbitraryGuid.ToString("B"), Guid.Empty), Is.EqualTo(arbitraryGuid));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToGuidOr_ValueIsArbitraryGuidFormatedWithP_ReturnsThatGuid()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToGuidOr(arbitraryGuid.ToString("P"), Guid.Empty), Is.EqualTo(arbitraryGuid));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void ToGuidOr_ValueIsArbitraryGuidFormatedWithX_ReturnsThatGuid()
        {
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(StringConvert.ToGuidOr(arbitraryGuid.ToString("X"), Guid.Empty), Is.EqualTo(arbitraryGuid));
            // ReSharper restore PossibleInvalidOperationException
        }
        #endregion
    }
    // ReSharper restore InconsistentNaming
}
