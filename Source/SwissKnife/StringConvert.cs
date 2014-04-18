using System;

namespace SwissKnife
{
    /// <summary>
    /// Contains methods that convert a <see cref="string"/> to another base data type. All the methods are guaranteed not to throw exceptions.
    /// If the conversion is not possible, either a fall-back value defined by the caller is returned or an option that indicates whether the conversion succeeded or not.
    /// </summary>
    public static class StringConvert
    {
        /// <summary>
        /// Converts the string representation of a number to its 32-bit signed integer equivalent. The return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="value">A <see cref="string"/> containing a number to convert.</param>
        /// <returns>
        /// 32-bit signed integer value equivalent to the number contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/>Null if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>Null if the conversion failed.
        /// </returns>
        public static int? ToInt32(Option<string> value)
        {
            // The TryParse() fails if the string parameter is null.
            // That means we don't need additional check if the Option is None.
            int result;
            return int.TryParse(value.ValueOrNull, out result) ? result : (int?)null;
        }

        /// <summary>
        /// Converts the string representation of a number to its 32-bit signed integer equivalent or specified default value if the conversion does not succeed.
        /// </summary>
        /// <param name="value">A <see cref="string"/> containing a number to convert.</param>
        /// <param name="defaultValue">Default value to return if the conversion fails.</param>
        /// <returns>
        /// 32-bit signed integer value equivalent to the number contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/><paramref name="defaultValue"/> if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/><paramref name="defaultValue"/> if the conversion failed.
        /// </returns>
        public static int ToInt32Or(Option<string> value, int defaultValue)
        {
            return ToInt32(value).GetValueOrDefault(defaultValue);
        }

        /// <summary>
        /// Converts the string representation of a number to its 32-bit signed integer equivalent or zero if the conversion does not succeed.
        /// </summary>
        /// <param name="value">A <see cref="string"/> containing a number to convert.</param>
        /// <returns>
        /// 32-bit signed integer value equivalent to the number contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/>Zero if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>Zero if the conversion failed.
        /// </returns>
        public static int ToInt32OrZero(Option<string> value)
        {
            return ToInt32Or(value, 0);
        }

        /// <summary>
        /// Converts the string representation of a number to its 64-bit signed integer equivalent. The return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="value">A <see cref="string"/> containing a number to convert.</param>
        /// <returns>
        /// 64-bit signed integer value equivalent to the number contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/>Null if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>Null if the conversion failed.
        /// </returns>
        public static long? ToInt64(Option<string> value)
        {
            // The TryParse() fails if the string parameter is null.
            // That means we don't need additional check if the Option is None.
            long result;
            return long.TryParse(value.ValueOrNull, out result) ? result : (long?)null;
        }

        /// <summary>
        /// Converts the string representation of a number to its 64-bit signed integer equivalent or specified default value if the conversion does not succeed.
        /// </summary>
        /// <param name="value">A <see cref="string"/> containing a number to convert.</param>
        /// <param name="defaultValue">Default value to return if the conversion fails.</param>
        /// <returns>
        /// 64-bit signed integer value equivalent to the number contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/><paramref name="defaultValue"/> if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/><paramref name="defaultValue"/> if the conversion failed.
        /// </returns>
        public static long ToInt64Or(Option<string> value, long defaultValue)
        {
            return ToInt64(value).GetValueOrDefault(defaultValue);
        }

        /// <summary>
        /// Converts the string representation of a number to its 64-bit signed integer equivalent or zero if the conversion does not succeed.
        /// </summary>
        /// <param name="value">A <see cref="string"/> containing a number to convert.</param>
        /// <returns>
        /// 64-bit signed integer value equivalent to the number contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/>Zero if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>Zero if the conversion failed.
        /// </returns>
        public static long ToInt64OrZero(Option<string> value)
        {
            return ToInt64Or(value, 0);
        }

        /// <summary>
        /// Converts the string representation of a number to an equivalent single-precision floating-point number. The return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="value">A <see cref="string"/> containing a number to convert.</param>
        /// <returns>
        /// Single-precision floating-point value equivalent to the number contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/>Null if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>Null if the conversion failed.
        /// </returns>
        public static float? ToSingle(Option<string> value)
        {
            // The TryParse() fails if the string parameter is null.
            // That means we don't need additional check if the Option is None.
            float result;
            return float.TryParse(value.ValueOrNull, out result) ? result : (float?)null;
        }

        /// <summary>
        /// Converts the string representation of a number to an equivalent single-precision floating-point number or specified default value if the conversion does not succeed.
        /// </summary>
        /// <param name="value">A <see cref="string"/> containing a number to convert.</param>
        /// <param name="defaultValue">Default value to return if the conversion fails.</param>
        /// <returns>
        /// Single-precision floating-point value equivalent to the number contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/><paramref name="defaultValue"/> if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/><paramref name="defaultValue"/> if the conversion failed.
        /// </returns>
        public static float ToSingleOr(Option<string> value, float defaultValue)
        {
            return ToSingle(value).GetValueOrDefault(defaultValue);
        }

        /// <summary>
        /// Converts the string representation of a number to an equivalent double-precision floating-point number or zero if the conversion does not succeed.
        /// </summary>
        /// <param name="value">A <see cref="string"/> containing a number to convert.</param>
        /// <returns>
        /// Single-precision floating-point value equivalent to the number contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/>Zero if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>Zero if the conversion failed.
        /// </returns>
        public static float ToSingleOrZero(Option<string> value)
        {
            return ToSingleOr(value, 0f);
        }

        /// <summary>
        /// Converts the string representation of a number to an equivalent double-precision floating-point number. The return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="value">A <see cref="string"/> containing a number to convert.</param>
        /// <returns>
        /// Double-precision floating-point value equivalent to the number contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/>Null if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>Null if the conversion failed.
        /// </returns>
        public static double? ToDouble(Option<string> value)
        {
            // The TryParse() fails if the string parameter is null.
            // That means we don't need additional check if the Option is None.
            double result;
            return double.TryParse(value.ValueOrNull, out result) ? result : (double?)null;
        }

        /// <summary>
        /// Converts the string representation of a number to an equivalent double-precision floating-point number or specified default value if the conversion does not succeed.
        /// </summary>
        /// <param name="value">A <see cref="string"/> containing a number to convert.</param>
        /// <param name="defaultValue">Default value to return if the conversion fails.</param>
        /// <returns>
        /// Double-precision floating-point value equivalent to the number contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/><paramref name="defaultValue"/> if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/><paramref name="defaultValue"/> if the conversion failed.
        /// </returns>
        public static double ToDoubleOr(Option<string> value, double defaultValue)
        {
            return ToDouble(value).GetValueOrDefault(defaultValue);
        }

        /// <summary>
        /// Converts the string representation of a number to an equivalent double-precision floating-point number or zero if the conversion does not succeed.
        /// </summary>
        /// <param name="value">A <see cref="string"/> containing a number to convert.</param>
        /// <returns>
        /// Double-precision floating-point value value equivalent to the number contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/>Zero if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>Zero if the conversion failed.
        /// </returns>
        public static double ToDoubleOrZero(Option<string> value)
        {
            return ToDoubleOr(value, 0d);
        }

        /// <summary>
        /// Converts the string representation of a logical value to its <see cref="Boolean"/> equivalent. The return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="value">A <see cref="string"/> containing the value to convert.</param>
        /// <returns>
        /// <remarks>
        /// <para>
        /// The <paramref name="value"/> can be preceded or followed by white space. The comparison is case-insensitive. For example, all of this values will be converted to true:
        /// "true", "True", "TRUE", " tRuE ", "1", " 1 ".
        /// </para>
        /// <note>
        /// This method has different semantic than <see cref="Convert.ToBoolean(string)"/> or <see cref="Boolean.Parse"/>.
        /// It accepts not only <see cref="Boolean.TrueString"/> and <see cref="Boolean.FalseString"/> as a valid input for conversion but also the strings "0" and "1".
        /// </note>
        /// </remarks>
        /// true if the <paramref name="value"/> is equivalent to <see cref="Boolean.TrueString"/> or "1" or false if value is equivalent to <see cref="Boolean.FalseString"/> or "0".
        /// <br/>-or-<br/>Null if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>Null if the conversion failed.
        /// </returns>
        public static bool? ToBoolean(Option<string> value)
        {
            // The TryParse() fails if the string parameter is null.
            // That means we don't need additional check if the Option is None.
            bool result;
            if (bool.TryParse(value.ValueOrNull, out result))
                return result;

            if (value.IsNone) return null;

            int? intValue = ToInt32(value.Value.Trim());

            if (!intValue.HasValue) return null;

            if (intValue.Value == 1) return true;

            if (intValue.Value == 0) return false;

            return null;
        }

        /// <summary>
        /// Converts the string representation of a logical value to its <see cref="Boolean"/> equivalent or specified default value if the conversion does not succeed.
        /// </summary>
        /// <param name="value">A <see cref="string"/> containing the value to convert.</param>
        /// <param name="defaultValue">Default value to return if the conversion fails.</param>
        /// <returns>
        /// <remarks>
        /// <para>
        /// The <paramref name="value"/> can be preceded or followed by white space. The comparison is case-insensitive. For example, all of this values will be converted to true:
        /// "true", "True", "TRUE", " tRuE ", "1", " 1 ".
        /// </para>
        /// <note>
        /// This method has different semantic than <see cref="Convert.ToBoolean(string)"/> or <see cref="Boolean.Parse"/>.
        /// It accepts not only <see cref="Boolean.TrueString"/> and <see cref="Boolean.FalseString"/> as a valid input for conversion but also the strings "0" and "1".
        /// </note>
        /// </remarks>
        /// true if the <paramref name="value"/> is equivalent to <see cref="Boolean.TrueString"/> or "1" or false if value is equivalent to <see cref="Boolean.FalseString"/> or "0".
        /// <br/>-or-<br/><paramref name="defaultValue"/> if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/><paramref name="defaultValue"/> if the conversion failed.
        /// </returns>
        public static bool ToBooleanOr(Option<string> value, bool defaultValue)
        {
            return ToBoolean(value).GetValueOrDefault(defaultValue);
        }

        /// <summary>
        /// Converts the string representation of a logical value to its <see cref="Boolean"/> equivalent or false if the conversion does not succeed.
        /// </summary>
        /// <param name="value">A <see cref="string"/> containing the value to convert.</param>
        /// <returns>
        /// <remarks>
        /// <para>
        /// The <paramref name="value"/> can be preceded or followed by white space. The comparison is case-insensitive. For example, all of this values will be converted to true:
        /// "true", "True", "TRUE", " tRuE ", "1", " 1 ".
        /// </para>
        /// <note>
        /// This method has different semantic than <see cref="Convert.ToBoolean(string)"/> or <see cref="Boolean.Parse"/>.
        /// It accepts not only <see cref="Boolean.TrueString"/> and <see cref="Boolean.FalseString"/> as a valid input for conversion but also the strings "0" and "1".
        /// </note>
        /// </remarks>
        /// true if the <paramref name="value"/> is equivalent to <see cref="Boolean.TrueString"/> or "1" or false if value is equivalent to <see cref="Boolean.FalseString"/> or "0".
        /// <br/>-or-<br/>false if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>false if the conversion failed.
        /// </returns>
        public static bool ToBooleanOrFalse(Option<string> value)
        {
            return ToBooleanOr(value, false);
        }

        /// <summary>
        /// Converts the string representation of a logical value to its <see cref="Boolean"/> equivalent or true if the conversion does not succeed.
        /// </summary>
        /// <param name="value">A <see cref="string"/> containing the value to convert.</param>
        /// <returns>
        /// <remarks>
        /// <para>
        /// The <paramref name="value"/> can be preceded or followed by white space. The comparison is case-insensitive. For example, all of this values will be converted to true:
        /// "true", "True", "TRUE", " tRuE ", "1", " 1 ".
        /// </para>
        /// <note>
        /// This method has different semantic than <see cref="Convert.ToBoolean(string)"/> or <see cref="Boolean.Parse"/>.
        /// It accepts not only <see cref="Boolean.TrueString"/> and <see cref="Boolean.FalseString"/> as a valid input for conversion but also the strings "0" and "1".
        /// </note>
        /// </remarks>
        /// true if the <paramref name="value"/> is equivalent to <see cref="Boolean.TrueString"/> or "1" or false if value is equivalent to <see cref="Boolean.FalseString"/> or "0".
        /// <br/>-or-<br/>true if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>true if the conversion failed.
        /// </returns>
        public static bool ToBooleanOrTrue(Option<string> value)
        {
            return ToBooleanOr(value, true);
        }

        /// <summary>
        /// Converts the string representation of a GUID to the equivalent <see cref="Guid"/> structure. The return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <remarks>
        /// <note>
        /// This method has different semantic than <see cref="Guid.Parse"/>.
        /// It accepts not only GUIDs formated with format parameters "N", "D", "B", "P", or "X" as a valid input for conversion 
        /// but also strings that are short string representations of GUIDs.
        /// </note>
        /// <para>
        /// For more on short string GUID representation see <see cref="GuidUtility.ToShortString"/>.
        /// </para>
        /// </remarks>
        /// <param name="value">A <see cref="string"/> containing the value to convert.</param>
        /// <returns>
        /// GUID value equivalent to the GUID contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/>Null if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>Null if the conversion failed.
        /// </returns>
        public static Guid? ToGuid(Option<string> value)
        {
            // The TryParse() fails if the string parameter is null.
            // That means we don't need additional check if the Option is None.
            Guid result;
            if (Guid.TryParse(value.ValueOrNull, out result)) return result;

            return GuidUtility.FromShortString(value);
        }

        /// <summary>
        /// Converts the string representation of a GUID to the equivalent <see cref="Guid"/> structure or specified default value if the conversion does not succeed.
        /// </summary>
        /// <param name="value">A <see cref="string"/> containing the value to convert.</param>
        /// <param name="defaultValue">Default value to return if the conversion fails.</param>
        /// <returns>
        /// GUID value equivalent to the GUID contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/><paramref name="defaultValue"/> if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/><paramref name="defaultValue"/> if the conversion failed.
        /// </returns>
        public static Guid ToGuidOr(Option<string> value, Guid defaultValue)
        {
            return ToGuid(value).GetValueOrDefault(defaultValue);
        }

        /// <summary>
        /// Converts the string representation of a GUID to the equivalent <see cref="Guid"/> structure or <see cref="Guid.Empty"/> if the conversion does not succeed.
        /// </summary>
        /// <param name="value">A <see cref="string"/> containing the value to convert.</param>
        /// <returns>
        /// GUID value equivalent to the GUID contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/><see cref="Guid.Empty"/> if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/><see cref="Guid.Empty"/> if the conversion failed.
        /// </returns>
        public static Guid ToGuidOrEmpty(Option<string> value)
        {
            return ToGuidOr(value, Guid.Empty);
        }
    }
}
