using System;
using System.Globalization;

namespace SwissKnife
{
    /// <summary>
    /// Contains methods that convert a <see cref="string"/> to another base data type. All the methods are guaranteed not to throw exceptions.
    /// If the conversion is not possible, either a fall-back value defined by the caller is returned or an option that indicates whether the conversion succeeded or not.
    /// </summary>
    /// <threadsafety static="true"/>
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
            // That means we don't need additional check if the value is None.
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
            // That means we don't need additional check if the value is None.
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
            // That means we don't need additional check if the value is None.
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
            // That means we don't need additional check if the value is None.
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
            // That means we don't need additional check if the value is None.
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
            // That means we don't need additional check if the value is None.
            Guid result;
            if (Guid.TryParse(value.ValueOrNull, out result)) return result;

            return GuidUtility.FromShortString(value);
        }

        /// <summary>
        /// Converts the string representation of a GUID to the equivalent <see cref="Guid"/> structure or specified default value if the conversion does not succeed.
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
        /// <br/>-or-<br/><see cref="Guid.Empty"/> if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/><see cref="Guid.Empty"/> if the conversion failed.
        /// </returns>
        public static Guid ToGuidOrEmpty(Option<string> value)
        {
            return ToGuidOr(value, Guid.Empty);
        }

        // TODO-IG: Test all ToDateTimeXYZ() methods.
        /// <summary>
        /// Converts the specified string representation of a date and time to its <see cref="DateTime"/> equivalent.
        /// The return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The method will try to convert the <paramref name="value"/> by using any of the standard .NET date and time format strings (see <see cref="StandardDateTimeFormats"/>).
        /// </para>
        /// <para>
        /// The method uses the current thread culture (<see cref="CultureInfo.CurrentCulture"/>) for providing culture-specific format information.
        /// </para>
        /// <para>
        /// The parsing is done by using the <see cref="System.Globalization.DateTimeStyles.None"/>.
        /// </para>
        /// </remarks>
        /// <param name="value">A <see cref="string"/> containing the value to convert.</param>
        /// <returns>
        /// <see cref="DateTime"/> value equivalent to the <see cref="DateTime"/> contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/>null if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>null if the conversion failed.
        /// </returns>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/2h3syy57(v=vs.110).aspx">Parsing Date and Time Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx">Custom Date and Time Format Strings</seealso>
        public static DateTime? ToDateTime(Option<string> value)
        {
            return ToDateTime(value, null, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts the specified string representation of a date and time to its <see cref="DateTime"/> equivalent using the specified format and
        /// the current thread culture (<see cref="CultureInfo.CurrentCulture"/>) for providing culture-specific format information.
        /// The return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the <paramref name="format"/> is specified (Some) the <paramref name="value"/> must match the specified format exactly.
        /// If the <paramref name="format"/> is not specified (None) the method will try to convert the <paramref name="value"/> by using any of the standard .NET date and time format strings (see <see cref="StandardDateTimeFormats"/>).
        /// </para>
        /// <para>
        /// The parsing is done by using the <see cref="System.Globalization.DateTimeStyles.None"/>.
        /// </para>
        /// </remarks>
        /// <param name="value">A <see cref="string"/> containing the value to convert.</param>
        /// <param name="format">
        /// The required date time format of the <paramref name="value"/>.
        /// If None, the method will try to use <a href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">any of the standard .NET date and time formats</a>.
        /// </param>
        /// <returns>
        /// <see cref="DateTime"/> value equivalent to the <see cref="DateTime"/> contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/>null if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>null if the conversion failed.
        /// </returns>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/2h3syy57(v=vs.110).aspx">Parsing Date and Time Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx">Custom Date and Time Format Strings</seealso>
        public static DateTime? ToDateTime(Option<string> value, Option<string> format)
        {
            return ToDateTime(value, format, CultureInfo.CurrentCulture);
        }

        // TODO-IG: Test with format provider set to invariant culture.
        /// <summary>
        /// Converts the specified string representation of a date and time to its <see cref="DateTime"/> equivalent using the specified format and culture-specific format information.
        /// The return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the <paramref name="format"/> is specified (Some) the <paramref name="value"/> must match the specified format exactly.
        /// If the <paramref name="format"/> is not specified (None) the method will try to convert the <paramref name="value"/> by using any of the standard .NET date and time format string (see <see cref="StandardDateTimeFormats"/>).
        /// </para>
        /// <para>
        /// If the <paramref name="formatProvider"/> is not specified, the method will use the current thread culture (<see cref="CultureInfo.CurrentCulture"/>) as the format provider.
        /// </para>
        /// <para>
        /// The parsing is done by using the <see cref="System.Globalization.DateTimeStyles.None"/>.
        /// </para>
        /// </remarks>
        /// <param name="value">A <see cref="string"/> containing the value to convert.</param>
        /// <param name="format">
        /// The required date time format of the <paramref name="value"/>.
        /// If None, the method will try to use <a href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">any of the standard .NET date and time formats</a>.
        /// </param>
        /// <param name="formatProvider">
        /// An object that supplies culture-specific formatting information about the <paramref name="value"/>.
        /// If None, the method will use the current thread culture (<see cref="CultureInfo.CurrentCulture"/>).
        /// </param>
        /// <returns>
        /// <see cref="DateTime"/> value equivalent to the <see cref="DateTime"/> contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/>null if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>null if the conversion failed.
        /// </returns>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/2h3syy57(v=vs.110).aspx">Parsing Date and Time Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx">Custom Date and Time Format Strings</seealso>
        public static DateTime? ToDateTime(Option<string> value, Option<string> format, Option<IFormatProvider> formatProvider)
        {
            // The TryParseExact() fails if the string parameter is null.
            // That means we don't need additional check if the value is None.
            DateTime result;
            return DateTime.TryParseExact(value.ValueOrNull,
                                          format.IsNone ? StandardDateTimeFormats.AllStandardDateTimeFormats : new [] { format.Value },
                                          formatProvider.ValueOr(CultureInfo.CurrentCulture),
                                          DateTimeStyles.None,
                                          out result) ? result : (DateTime?)null;
        }

        /// <summary>
        /// Converts the specified string representation of a date and time to its <see cref="DateTime"/> equivalent using the specified format and culture-specific format information.
        /// If the conversion fails, the specified default value is returned.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the <paramref name="format"/> is specified (Some) the <paramref name="value"/> must match the specified format exactly.
        /// If the <paramref name="format"/> is not specified (None) the method will try to convert the <paramref name="value"/> by using any of the standard .NET date and time format string (see <see cref="StandardDateTimeFormats"/>).
        /// </para>
        /// <para>
        /// If the <paramref name="formatProvider"/> is not specified, the method will use the current thread culture (<see cref="CultureInfo.CurrentCulture"/>) as the format provider.
        /// </para>
        /// <para>
        /// The parsing is done by using the <see cref="System.Globalization.DateTimeStyles.None"/>.
        /// </para>
        /// </remarks>
        /// <param name="value">A <see cref="string"/> containing the value to convert.</param>
        /// <param name="format">
        /// The required date time format of the <paramref name="value"/>.
        /// If None, the method will try to use <a href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">any of the standard .NET date and time formats</a>.
        /// </param>
        /// <param name="formatProvider">
        /// An object that supplies culture-specific formatting information about the <paramref name="value"/>.
        /// If None, the method will use the current thread culture (<see cref="CultureInfo.CurrentCulture"/>).
        /// </param>
        /// <param name="defaultValue">Default value to return if the conversion fails.</param>
        /// <returns>
        /// <see cref="DateTime"/> value equivalent to the <see cref="DateTime"/> contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/><paramref name="defaultValue"/> if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/><paramref name="defaultValue"/> if the conversion failed.
        /// </returns>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/2h3syy57(v=vs.110).aspx">Parsing Date and Time Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx">Custom Date and Time Format Strings</seealso>
        public static DateTime ToDateTimeOr(Option<string> value, Option<string> format, Option<IFormatProvider> formatProvider, DateTime defaultValue)
        {
            return ToDateTime(value, format, formatProvider).GetValueOrDefault(defaultValue);
        }

        /// <summary>
        /// Converts the specified string representation of a date and time to its <see cref="DateTime"/> equivalent using the specified format and
        /// the current thread culture (<see cref="CultureInfo.CurrentCulture"/>) for providing culture-specific format information.
        /// If the conversion fails, the specified default value is returned.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the <paramref name="format"/> is specified (Some) the <paramref name="value"/> must match the specified format exactly.
        /// If the <paramref name="format"/> is not specified (None) the method will try to convert the <paramref name="value"/> by using any of the standard .NET date and time format strings (see <see cref="StandardDateTimeFormats"/>).
        /// </para>
        /// <para>
        /// The parsing is done by using the <see cref="System.Globalization.DateTimeStyles.None"/>.
        /// </para>
        /// </remarks>
        /// <param name="value">A <see cref="string"/> containing the value to convert.</param>
        /// <param name="format">
        /// The required date time format of the <paramref name="value"/>.
        /// If None, the method will try to use <a href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">any of the standard .NET date and time formats</a>.
        /// </param>
        /// <param name="defaultValue">Default value to return if the conversion fails.</param>
        /// <returns>
        /// <see cref="DateTime"/> value equivalent to the <see cref="DateTime"/> contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/><paramref name="defaultValue"/> if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/><paramref name="defaultValue"/> if the conversion failed.
        /// </returns>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/2h3syy57(v=vs.110).aspx">Parsing Date and Time Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx">Custom Date and Time Format Strings</seealso>
        public static DateTime ToDateTimeOr(Option<string> value, Option<string> format, DateTime defaultValue)
        {
            return ToDateTime(value, format, CultureInfo.CurrentCulture).GetValueOrDefault(defaultValue);
        }

        /// <summary>
        /// Converts the specified string representation of a date and time to its <see cref="DateTime"/> equivalent.
        /// If the conversion fails, the specified default value is returned.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The method will try to convert the <paramref name="value"/> by using any of the standard .NET date and time format strings (see <see cref="StandardDateTimeFormats"/>).
        /// </para>
        /// <para>
        /// The method uses the current thread culture (<see cref="CultureInfo.CurrentCulture"/>) for providing culture-specific format information.
        /// </para>
        /// <para>
        /// The parsing is done by using the <see cref="System.Globalization.DateTimeStyles.None"/>.
        /// </para>
        /// </remarks>
        /// <param name="value">A <see cref="string"/> containing the value to convert.</param>
        /// <param name="defaultValue">Default value to return if the conversion fails.</param>
        /// <returns>
        /// <see cref="DateTime"/> value equivalent to the <see cref="DateTime"/> contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/><paramref name="defaultValue"/> if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/><paramref name="defaultValue"/> if the conversion failed.
        /// </returns>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/2h3syy57(v=vs.110).aspx">Parsing Date and Time Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx">Custom Date and Time Format Strings</seealso>
        public static DateTime ToDateTimeOr(Option<string> value, DateTime defaultValue)
        {
            return ToDateTime(value, null, CultureInfo.CurrentCulture).GetValueOrDefault(defaultValue);
        }
    }
}
