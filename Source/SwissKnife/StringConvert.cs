using System;
using System.Globalization;
using SwissKnife.Time;

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
        // TODO-IG: Add a new comment about default DateTimeAssumption to all ToDateTimeXYZ() methods. Do this after you introduce <inheritdoc> in comments.
        // TODO-IG: Add the existing comment about dangerous conversion to all ToDateTimeXYZ() methods. Do this after you introduce <inheritdoc> in comments.
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
        /// <note type="caution">
        /// <para>
        /// Avoid using <see cref="String"/> to <see cref="DateTime"/> conversion without explicitly specifying the format and the format provider.
        /// They are made optional in all of the date time conversion methods only to support cases when the origin of the textual representation is not known.
        /// (In other words, for the cases when the format and the culture in which the <see cref="value"/> is originally created is not know.)
        /// Such cases should be an exception.
        /// </para>
        /// <para>
        /// If the format and the format specifier are not provided the conversion methods can only do their best guess in order to convert the <paramref name="value"/> to <see cref="DateTime"/>.
        /// It is possible (if not even likely) that the conversion will succeed but the returned value will not be the value that one would expect.
        /// (In other words, it will not be equivalent to the <see cref="DateTime"/> value originally used to create the <paramref name="value"/>.)
        /// </para>
        /// <para>
        /// Ideally, <see cref="string"/> to <see cref="DateTime"/> conversion should always be done by using the
        /// <see cref="ToDateTime(Option{string}, Option{string}, Option{IFormatProvider}, DateTimeAssumption)"/> override with all parameters provided.
        /// </para>
        /// </note>
        /// </para>
        /// <para>
        /// Date and time are returned as a Coordinated Universal Time (UTC).
        /// If the <paramref name="value"/> denotes a local time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeLocal"/>,
        /// the date and time are converted from the local time to UTC.
        /// If the input string denotes a UTC time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeUniversal"/>, no conversion occurs.
        /// </para>
        /// <para>
        /// The <see cref="DateTime.Kind"/> of the returned <see cref="DateTime"/> object is always <see cref="DateTimeKind.Utc"/>.
        /// </para>
        /// </remarks>
        /// <param name="value">A <see cref="string"/> containing the value to convert.</param>
        /// <returns>
        /// <see cref="DateTime"/> value equivalent to the <see cref="DateTime"/> contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/>null if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>null if the conversion failed.<br/>
        /// In case of successful conversion, the returned <see cref="DateTime"/> object will have <see cref="DateTime.Kind"/> set to <see cref="DateTimeKind.Utc"/>.
        /// </returns>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/2h3syy57(v=vs.110).aspx">Parsing Date and Time Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx">Custom Date and Time Format Strings</seealso>
        public static DateTime? ToDateTime(Option<string> value)
        {
            return ToDateTime(value, null, CultureInfo.CurrentCulture, DateTimeAssumption.AssumeUniversal);
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
        /// Date and time are returned as a Coordinated Universal Time (UTC).
        /// If the <paramref name="value"/> denotes a local time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeLocal"/>,
        /// the date and time are converted from the local time to UTC.
        /// If the input string denotes a UTC time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeUniversal"/>, no conversion occurs.
        /// </para>
        /// <para>
        /// The <see cref="DateTime.Kind"/> of the returned <see cref="DateTime"/> object is always <see cref="DateTimeKind.Utc"/>.
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
        /// <br/>-or-<br/>null if the conversion failed.<br/>
        /// In case of successful conversion, the returned <see cref="DateTime"/> object will have <see cref="DateTime.Kind"/> set to <see cref="DateTimeKind.Utc"/>.
        /// </returns>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/2h3syy57(v=vs.110).aspx">Parsing Date and Time Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx">Custom Date and Time Format Strings</seealso>
        public static DateTime? ToDateTime(Option<string> value, Option<string> format)
        {
            return ToDateTime(value, format, CultureInfo.CurrentCulture, DateTimeAssumption.AssumeUniversal);
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
        /// Date and time are returned as a Coordinated Universal Time (UTC).
        /// If the <paramref name="value"/> denotes a local time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeLocal"/>,
        /// the date and time are converted from the local time to UTC.
        /// If the input string denotes a UTC time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeUniversal"/>, no conversion occurs.
        /// </para>
        /// <para>
        /// The <see cref="DateTime.Kind"/> of the returned <see cref="DateTime"/> object is always <see cref="DateTimeKind.Utc"/>.
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
        /// <param name="dateTimeAssumption">An assumption about the date time stored in the <paramref name="value"/>; is it stored as a local or UTC time.</param>
        /// <returns>
        /// <see cref="DateTime"/> value equivalent to the <see cref="DateTime"/> contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/>null if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>null if the conversion failed.<br/>
        /// In case of successful conversion, the returned <see cref="DateTime"/> object will have <see cref="DateTime.Kind"/> set to <see cref="DateTimeKind.Utc"/>.
        /// </returns>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/2h3syy57(v=vs.110).aspx">Parsing Date and Time Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx">Custom Date and Time Format Strings</seealso>
        public static DateTime? ToDateTime(Option<string> value, Option<string> format, Option<IFormatProvider> formatProvider, DateTimeAssumption dateTimeAssumption)
        {
            // TODO-IG: Check that dateTimeAssumptio is a valid enumeration value.

            // The TryParseExact() fails if the string parameter is null.
            // That means we don't need additional check if the value is None.
            DateTime result;
            return DateTime.TryParseExact(value.ValueOrNull,
                                          format.IsNone ? StandardDateTimeFormats.AllStandardDateTimeFormats : new [] { format.Value },
                                          formatProvider.ValueOr(CultureInfo.CurrentCulture),
                                          DateTimeAssumptionHelper.ToDateTimeStyles(dateTimeAssumption) | DateTimeStyles.AdjustToUniversal,
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
        /// Date and time are returned as a Coordinated Universal Time (UTC).
        /// If the <paramref name="value"/> denotes a local time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeLocal"/>,
        /// the date and time are converted from the local time to UTC.
        /// If the input string denotes a UTC time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeUniversal"/>, no conversion occurs.
        /// </para>
        /// <para>
        /// The <see cref="DateTime.Kind"/> of the returned <see cref="DateTime"/> object is always <see cref="DateTimeKind.Utc"/>.
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
        /// <param name="dateTimeAssumption">An assumption about the date time stored in the <paramref name="value"/>; is it stored as a local or UTC time.</param>
        /// <param name="defaultValue">Default value to return if the conversion fails.</param>
        /// <returns>
        /// <see cref="DateTime"/> value equivalent to the <see cref="DateTime"/> contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/><paramref name="defaultValue"/> if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/><paramref name="defaultValue"/> if the conversion failed.<br/>
        /// In case of successful conversion, the returned <see cref="DateTime"/> object will have <see cref="DateTime.Kind"/> set to <see cref="DateTimeKind.Utc"/>.
        /// </returns>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/2h3syy57(v=vs.110).aspx">Parsing Date and Time Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx">Custom Date and Time Format Strings</seealso>
        public static DateTime ToDateTimeOr(Option<string> value, Option<string> format, Option<IFormatProvider> formatProvider, DateTimeAssumption dateTimeAssumption, DateTime defaultValue)
        {
            return ToDateTime(value, format, formatProvider, dateTimeAssumption).GetValueOrDefault(defaultValue);
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
        /// Date and time are returned as a Coordinated Universal Time (UTC).
        /// If the <paramref name="value"/> denotes a local time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeLocal"/>,
        /// the date and time are converted from the local time to UTC.
        /// If the input string denotes a UTC time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeUniversal"/>, no conversion occurs.
        /// </para>
        /// <para>
        /// The <see cref="DateTime.Kind"/> of the returned <see cref="DateTime"/> object is always <see cref="DateTimeKind.Utc"/>.
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
        /// <br/>-or-<br/><paramref name="defaultValue"/> if the conversion failed.<br/>
        /// In case of successful conversion, the returned <see cref="DateTime"/> object will have <see cref="DateTime.Kind"/> set to <see cref="DateTimeKind.Utc"/>.
        /// </returns>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/2h3syy57(v=vs.110).aspx">Parsing Date and Time Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx">Custom Date and Time Format Strings</seealso>
        public static DateTime ToDateTimeOr(Option<string> value, Option<string> format, DateTime defaultValue)
        {
            return ToDateTime(value, format, CultureInfo.CurrentCulture, DateTimeAssumption.AssumeUniversal).GetValueOrDefault(defaultValue);
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
        /// Date and time are returned as a Coordinated Universal Time (UTC).
        /// If the <paramref name="value"/> denotes a local time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeLocal"/>,
        /// the date and time are converted from the local time to UTC.
        /// If the input string denotes a UTC time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeUniversal"/>, no conversion occurs.
        /// </para>
        /// <para>
        /// The <see cref="DateTime.Kind"/> of the returned <see cref="DateTime"/> object is always <see cref="DateTimeKind.Utc"/>.
        /// </para>
        /// </remarks>
        /// <param name="value">A <see cref="string"/> containing the value to convert.</param>
        /// <param name="defaultValue">Default value to return if the conversion fails.</param>
        /// <returns>
        /// <see cref="DateTime"/> value equivalent to the <see cref="DateTime"/> contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/><paramref name="defaultValue"/> if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/><paramref name="defaultValue"/> if the conversion failed.<br/>
        /// In case of successful conversion, the returned <see cref="DateTime"/> object will have <see cref="DateTime.Kind"/> set to <see cref="DateTimeKind.Utc"/>.
        /// </returns>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/2h3syy57(v=vs.110).aspx">Parsing Date and Time Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx">Custom Date and Time Format Strings</seealso>
        public static DateTime ToDateTimeOr(Option<string> value, DateTime defaultValue)
        {
            return ToDateTime(value, null, CultureInfo.CurrentCulture, DateTimeAssumption.AssumeUniversal).GetValueOrDefault(defaultValue);
        }

        /// <summary>
        /// Converts the specified string representation of a date and time to its <see cref="DateTime"/> equivalent using the specified format and culture-specific format information.
        /// If the conversion fails, the method returns the current date and time, with the offset set to the local time's offset from Coordinated Universal Time (UTC).
        /// </summary>
        /// <remarks>
        /// <para>
        /// The current date and time is obtained from the <see cref="TimeGenerator.GetLocalNow"/>.
        /// This makes the method suitable for scenarios where taking control over date and time creation is necessary. Typical example would be mocking date and time generation for testing purposes.<br/>
        /// For more information see the <see cref="TimeGenerator"/> class.
        /// </para>
        /// <para>
        /// If the <paramref name="format"/> is specified (Some) the <paramref name="value"/> must match the specified format exactly.
        /// If the <paramref name="format"/> is not specified (None) the method will try to convert the <paramref name="value"/> by using any of the standard .NET date and time format string (see <see cref="StandardDateTimeFormats"/>).
        /// </para>
        /// <para>
        /// If the <paramref name="formatProvider"/> is not specified, the method will use the current thread culture (<see cref="CultureInfo.CurrentCulture"/>) as the format provider.
        /// </para>
        /// <para>
        /// Date and time are returned as a Coordinated Universal Time (UTC).
        /// If the <paramref name="value"/> denotes a local time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeLocal"/>,
        /// the date and time are converted from the local time to UTC.
        /// If the input string denotes a UTC time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeUniversal"/>, no conversion occurs.
        /// </para>
        /// <para>
        /// The <see cref="DateTime.Kind"/> of the returned <see cref="DateTime"/> object is always <see cref="DateTimeKind.Utc"/>.
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
        /// <param name="dateTimeAssumption">An assumption about the date time stored in the <paramref name="value"/>; is it stored as a local or UTC time.</param>
        /// <returns>
        /// <see cref="DateTime"/> value equivalent to the <see cref="DateTime"/> contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/>Current date and time if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>Current date and time if the conversion failed.<br/>
        /// In case of successful conversion, the returned <see cref="DateTime"/> object will have <see cref="DateTime.Kind"/> set to <see cref="DateTimeKind.Utc"/>.
        /// </returns>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/2h3syy57(v=vs.110).aspx">Parsing Date and Time Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx">Custom Date and Time Format Strings</seealso>
        public static DateTime ToDateTimeOrNow(Option<string> value, Option<string> format, Option<IFormatProvider> formatProvider, DateTimeAssumption dateTimeAssumption)
        {
            return ToDateTime(value, format, formatProvider, dateTimeAssumption).GetValueOrDefault(TimeGenerator.GetLocalNow().LocalDateTime);
        }

        /// <summary>
        /// Converts the specified string representation of a date and time to its <see cref="DateTime"/> equivalent using the specified format and
        /// the current thread culture (<see cref="CultureInfo.CurrentCulture"/>) for providing culture-specific format information.
        /// If the conversion fails, the method returns the current date and time, with the offset set to the local time's offset from Coordinated Universal Time (UTC).
        /// </summary>
        /// <remarks>
        /// <para>
        /// The current date and time is obtained from the <see cref="TimeGenerator.GetLocalNow"/>.
        /// This makes the method suitable for scenarios where taking control over date and time creation is necessary. Typical example would be mocking date and time generation for testing purposes.<br/>
        /// For more information see the <see cref="TimeGenerator"/> class.
        /// </para>
        /// <para>
        /// If the <paramref name="format"/> is specified (Some) the <paramref name="value"/> must match the specified format exactly.
        /// If the <paramref name="format"/> is not specified (None) the method will try to convert the <paramref name="value"/> by using any of the standard .NET date and time format strings (see <see cref="StandardDateTimeFormats"/>).
        /// </para>
        /// <para>
        /// Date and time are returned as a Coordinated Universal Time (UTC).
        /// If the <paramref name="value"/> denotes a local time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeLocal"/>,
        /// the date and time are converted from the local time to UTC.
        /// If the input string denotes a UTC time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeUniversal"/>, no conversion occurs.
        /// </para>
        /// <para>
        /// The <see cref="DateTime.Kind"/> of the returned <see cref="DateTime"/> object is always <see cref="DateTimeKind.Utc"/>.
        /// </para>
        /// </remarks>
        /// <param name="value">A <see cref="string"/> containing the value to convert.</param>
        /// <param name="format">
        /// The required date time format of the <paramref name="value"/>.
        /// If None, the method will try to use <a href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">any of the standard .NET date and time formats</a>.
        /// </param>
        /// <returns>
        /// <see cref="DateTime"/> value equivalent to the <see cref="DateTime"/> contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/>Current date and time if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>Current date and time if the conversion failed.<br/>
        /// In case of successful conversion, the returned <see cref="DateTime"/> object will have <see cref="DateTime.Kind"/> set to <see cref="DateTimeKind.Utc"/>.
        /// </returns>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/2h3syy57(v=vs.110).aspx">Parsing Date and Time Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx">Custom Date and Time Format Strings</seealso>
        public static DateTime ToDateTimeOrNow(Option<string> value, Option<string> format)
        {
            return ToDateTime(value, format, CultureInfo.CurrentCulture, DateTimeAssumption.AssumeUniversal).GetValueOrDefault(TimeGenerator.GetLocalNow().LocalDateTime);
        }

        /// <summary>
        /// Converts the specified string representation of a date and time to its <see cref="DateTime"/> equivalent.
        /// If the conversion fails, the method returns the current date and time, with the offset set to the local time's offset from Coordinated Universal Time (UTC).
        /// </summary>
        /// <remarks>
        /// <para>
        /// The current date and time is obtained from the <see cref="TimeGenerator.GetLocalNow"/>.
        /// This makes the method suitable for scenarios where taking control over date and time creation is necessary. Typical example would be mocking date and time generation for testing purposes.<br/>
        /// For more information see the <see cref="TimeGenerator"/> class.
        /// </para>
        /// <para>
        /// The method will try to convert the <paramref name="value"/> by using any of the standard .NET date and time format strings (see <see cref="StandardDateTimeFormats"/>).
        /// </para>
        /// <para>
        /// The method uses the current thread culture (<see cref="CultureInfo.CurrentCulture"/>) for providing culture-specific format information.
        /// </para>
        /// <para>
        /// Date and time are returned as a Coordinated Universal Time (UTC).
        /// If the <paramref name="value"/> denotes a local time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeLocal"/>,
        /// the date and time are converted from the local time to UTC.
        /// If the input string denotes a UTC time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeUniversal"/>, no conversion occurs.
        /// </para>
        /// <para>
        /// The <see cref="DateTime.Kind"/> of the returned <see cref="DateTime"/> object is always <see cref="DateTimeKind.Utc"/>.
        /// </para>
        /// </remarks>
        /// <param name="value">A <see cref="string"/> containing the value to convert.</param>
        /// <returns>
        /// <see cref="DateTime"/> value equivalent to the <see cref="DateTime"/> contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/>Current date and time if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>Current date and time if the conversion failed.<br/>
        /// In case of successful conversion, the returned <see cref="DateTime"/> object will have <see cref="DateTime.Kind"/> set to <see cref="DateTimeKind.Utc"/>.
        /// </returns>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/2h3syy57(v=vs.110).aspx">Parsing Date and Time Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx">Custom Date and Time Format Strings</seealso>
        public static DateTime ToDateTimeOrNow(Option<string> value)
        {
            return ToDateTime(value, null, CultureInfo.CurrentCulture, DateTimeAssumption.AssumeUniversal).GetValueOrDefault(TimeGenerator.GetLocalNow().LocalDateTime);
        }

        /// <summary>
        /// Converts the specified string representation of a date and time to its <see cref="DateTime"/> equivalent using the specified format and culture-specific format information.
        /// If the conversion fails, the method returns the current date and time expressed as Coordinated Universal Time (UTC) date and time whose offset is <see cref="TimeSpan.Zero"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The current date and time is obtained from the <see cref="TimeGenerator.GetUtcNow"/>.
        /// This makes the method suitable for scenarios where taking control over date and time creation is necessary. Typical example would be mocking date and time generation for testing purposes.<br/>
        /// For more information see the <see cref="TimeGenerator"/> class.
        /// </para>
        /// <para>
        /// If the <paramref name="format"/> is specified (Some) the <paramref name="value"/> must match the specified format exactly.
        /// If the <paramref name="format"/> is not specified (None) the method will try to convert the <paramref name="value"/> by using any of the standard .NET date and time format string (see <see cref="StandardDateTimeFormats"/>).
        /// </para>
        /// <para>
        /// If the <paramref name="formatProvider"/> is not specified, the method will use the current thread culture (<see cref="CultureInfo.CurrentCulture"/>) as the format provider.
        /// </para>
        /// <para>
        /// Date and time are returned as a Coordinated Universal Time (UTC).
        /// If the <paramref name="value"/> denotes a local time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeLocal"/>,
        /// the date and time are converted from the local time to UTC.
        /// If the input string denotes a UTC time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeUniversal"/>, no conversion occurs.
        /// </para>
        /// <para>
        /// The <see cref="DateTime.Kind"/> of the returned <see cref="DateTime"/> object is always <see cref="DateTimeKind.Utc"/>.
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
        /// <param name="dateTimeAssumption">An assumption about the date time stored in the <paramref name="value"/>; is it stored as a local or UTC time.</param>
        /// <returns>
        /// <see cref="DateTime"/> value equivalent to the <see cref="DateTime"/> contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/>Current date and time if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>Current date and time if the conversion failed.<br/>
        /// In case of successful conversion, the returned <see cref="DateTime"/> object will have <see cref="DateTime.Kind"/> set to <see cref="DateTimeKind.Utc"/>.
        /// </returns>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/2h3syy57(v=vs.110).aspx">Parsing Date and Time Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx">Custom Date and Time Format Strings</seealso>
        public static DateTime ToDateTimeOrUtcNow(Option<string> value, Option<string> format, Option<IFormatProvider> formatProvider, DateTimeAssumption dateTimeAssumption)
        {
            return ToDateTime(value, format, formatProvider, dateTimeAssumption).GetValueOrDefault(TimeGenerator.GetUtcNow().UtcDateTime);
        }

        /// <summary>
        /// Converts the specified string representation of a date and time to its <see cref="DateTime"/> equivalent using the specified format and
        /// the current thread culture (<see cref="CultureInfo.CurrentCulture"/>) for providing culture-specific format information.
        /// If the conversion fails, the method returns the current date and time expressed as Coordinated Universal Time (UTC) date and time whose offset is <see cref="TimeSpan.Zero"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The current date and time is obtained from the <see cref="TimeGenerator.GetUtcNow"/>.
        /// This makes the method suitable for scenarios where taking control over date and time creation is necessary. Typical example would be mocking date and time generation for testing purposes.<br/>
        /// For more information see the <see cref="TimeGenerator"/> class.
        /// </para>
        /// <para>
        /// If the <paramref name="format"/> is specified (Some) the <paramref name="value"/> must match the specified format exactly.
        /// If the <paramref name="format"/> is not specified (None) the method will try to convert the <paramref name="value"/> by using any of the standard .NET date and time format strings (see <see cref="StandardDateTimeFormats"/>).
        /// </para>
        /// <para>
        /// Date and time are returned as a Coordinated Universal Time (UTC).
        /// If the <paramref name="value"/> denotes a local time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeLocal"/>,
        /// the date and time are converted from the local time to UTC.
        /// If the input string denotes a UTC time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeUniversal"/>, no conversion occurs.
        /// </para>
        /// <para>
        /// The <see cref="DateTime.Kind"/> of the returned <see cref="DateTime"/> object is always <see cref="DateTimeKind.Utc"/>.
        /// </para>
        /// </remarks>
        /// <param name="value">A <see cref="string"/> containing the value to convert.</param>
        /// <param name="format">
        /// The required date time format of the <paramref name="value"/>.
        /// If None, the method will try to use <a href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">any of the standard .NET date and time formats</a>.
        /// </param>
        /// <returns>
        /// <see cref="DateTime"/> value equivalent to the <see cref="DateTime"/> contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/>Current date and time if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>Current date and time if the conversion failed.<br/>
        /// In case of successful conversion, the returned <see cref="DateTime"/> object will have <see cref="DateTime.Kind"/> set to <see cref="DateTimeKind.Utc"/>.
        /// </returns>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/2h3syy57(v=vs.110).aspx">Parsing Date and Time Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx">Custom Date and Time Format Strings</seealso>
        public static DateTime ToDateTimeOrUtcNow(Option<string> value, Option<string> format)
        {
            return ToDateTime(value, format, CultureInfo.CurrentCulture, DateTimeAssumption.AssumeUniversal).GetValueOrDefault(TimeGenerator.GetUtcNow().UtcDateTime);
        }

        /// <summary>
        /// Converts the specified string representation of a date and time to its <see cref="DateTime"/> equivalent.
        /// If the conversion fails, the method returns the current date and time expressed as Coordinated Universal Time (UTC) date and time whose offset is <see cref="TimeSpan.Zero"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The current date and time is obtained from the <see cref="TimeGenerator.GetUtcNow"/>.
        /// This makes the method suitable for scenarios where taking control over date and time creation is necessary. Typical example would be mocking date and time generation for testing purposes.<br/>
        /// For more information see the <see cref="TimeGenerator"/> class.
        /// </para>
        /// <para>
        /// The method will try to convert the <paramref name="value"/> by using any of the standard .NET date and time format strings (see <see cref="StandardDateTimeFormats"/>).
        /// </para>
        /// <para>
        /// The method uses the current thread culture (<see cref="CultureInfo.CurrentCulture"/>) for providing culture-specific format information.
        /// </para>
        /// <para>
        /// Date and time are returned as a Coordinated Universal Time (UTC).
        /// If the <paramref name="value"/> denotes a local time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeLocal"/>,
        /// the date and time are converted from the local time to UTC.
        /// If the input string denotes a UTC time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeUniversal"/>, no conversion occurs.
        /// </para>
        /// <para>
        /// The <see cref="DateTime.Kind"/> of the returned <see cref="DateTime"/> object is always <see cref="DateTimeKind.Utc"/>.
        /// </para>
        /// </remarks>
        /// <param name="value">A <see cref="string"/> containing the value to convert.</param>
        /// <returns>
        /// <see cref="DateTime"/> value equivalent to the <see cref="DateTime"/> contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/>Current date and time if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>Current date and time if the conversion failed.<br/>
        /// In case of successful conversion, the returned <see cref="DateTime"/> object will have <see cref="DateTime.Kind"/> set to <see cref="DateTimeKind.Utc"/>.
        /// </returns>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/2h3syy57(v=vs.110).aspx">Parsing Date and Time Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx">Custom Date and Time Format Strings</seealso>
        public static DateTime ToDateTimeOrUtcNow(Option<string> value)
        {
            return ToDateTime(value, null, CultureInfo.CurrentCulture, DateTimeAssumption.AssumeUniversal).GetValueOrDefault(TimeGenerator.GetUtcNow().UtcDateTime);
        }

        /// <summary>
        /// Converts the specified string representation of a date and time to its <see cref="DateTime"/> equivalent using the specified format and culture-specific format information.
        /// If the conversion fails, the method returns the current date.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The current date is obtained from the <see cref="TimeGenerator.GetToday"/>.
        /// This makes the method suitable for scenarios where taking control over date and time creation is necessary. Typical example would be mocking date and time generation for testing purposes.<br/>
        /// For more information see the <see cref="TimeGenerator"/> class.
        /// </para>
        /// <para>
        /// If the <paramref name="format"/> is specified (Some) the <paramref name="value"/> must match the specified format exactly.
        /// If the <paramref name="format"/> is not specified (None) the method will try to convert the <paramref name="value"/> by using any of the standard .NET date and time format string (see <see cref="StandardDateTimeFormats"/>).
        /// </para>
        /// <para>
        /// If the <paramref name="formatProvider"/> is not specified, the method will use the current thread culture (<see cref="CultureInfo.CurrentCulture"/>) as the format provider.
        /// </para>
        /// <para>
        /// Date and time are returned as a Coordinated Universal Time (UTC).
        /// If the <paramref name="value"/> denotes a local time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeLocal"/>,
        /// the date and time are converted from the local time to UTC.
        /// If the input string denotes a UTC time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeUniversal"/>, no conversion occurs.
        /// </para>
        /// <para>
        /// The <see cref="DateTime.Kind"/> of the returned <see cref="DateTime"/> object is always <see cref="DateTimeKind.Utc"/>.
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
        /// <param name="dateTimeAssumption">An assumption about the date time stored in the <paramref name="value"/>; is it stored as a local or UTC time.</param>
        /// <returns>
        /// <see cref="DateTime"/> value equivalent to the <see cref="DateTime"/> contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/>Current date if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>Current date if the conversion failed.<br/>
        /// In case of successful conversion, the returned <see cref="DateTime"/> object will have <see cref="DateTime.Kind"/> set to <see cref="DateTimeKind.Utc"/>.
        /// </returns>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/2h3syy57(v=vs.110).aspx">Parsing Date and Time Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx">Custom Date and Time Format Strings</seealso>
        public static DateTime ToDateTimeOrToday(Option<string> value, Option<string> format, Option<IFormatProvider> formatProvider, DateTimeAssumption dateTimeAssumption)
        {
            return ToDateTime(value, format, formatProvider, dateTimeAssumption).GetValueOrDefault(TimeGenerator.GetToday());
        }

        /// <summary>
        /// Converts the specified string representation of a date and time to its <see cref="DateTime"/> equivalent using the specified format and
        /// the current thread culture (<see cref="CultureInfo.CurrentCulture"/>) for providing culture-specific format information.
        /// If the conversion fails, the method returns the current date.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The current date is obtained from the <see cref="TimeGenerator.GetToday"/>.
        /// This makes the method suitable for scenarios where taking control over date and time creation is necessary. Typical example would be mocking date and time generation for testing purposes.<br/>
        /// For more information see the <see cref="TimeGenerator"/> class.
        /// </para>
        /// <para>
        /// If the <paramref name="format"/> is specified (Some) the <paramref name="value"/> must match the specified format exactly.
        /// If the <paramref name="format"/> is not specified (None) the method will try to convert the <paramref name="value"/> by using any of the standard .NET date and time format strings (see <see cref="StandardDateTimeFormats"/>).
        /// </para>
        /// <para>
        /// Date and time are returned as a Coordinated Universal Time (UTC).
        /// If the <paramref name="value"/> denotes a local time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeLocal"/>,
        /// the date and time are converted from the local time to UTC.
        /// If the input string denotes a UTC time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeUniversal"/>, no conversion occurs.
        /// </para>
        /// <para>
        /// The <see cref="DateTime.Kind"/> of the returned <see cref="DateTime"/> object is always <see cref="DateTimeKind.Utc"/>.
        /// </para>
        /// </remarks>
        /// <param name="value">A <see cref="string"/> containing the value to convert.</param>
        /// <param name="format">
        /// The required date time format of the <paramref name="value"/>.
        /// If None, the method will try to use <a href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">any of the standard .NET date and time formats</a>.
        /// </param>
        /// <returns>
        /// <see cref="DateTime"/> value equivalent to the <see cref="DateTime"/> contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/>Current date if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>Current date if the conversion failed.<br/>
        /// In case of successful conversion, the returned <see cref="DateTime"/> object will have <see cref="DateTime.Kind"/> set to <see cref="DateTimeKind.Utc"/>.
        /// </returns>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/2h3syy57(v=vs.110).aspx">Parsing Date and Time Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx">Custom Date and Time Format Strings</seealso>
        public static DateTime ToDateTimeOrToday(Option<string> value, Option<string> format)
        {
            return ToDateTime(value, format, CultureInfo.CurrentCulture, DateTimeAssumption.AssumeUniversal).GetValueOrDefault(TimeGenerator.GetToday());
        }

        /// <summary>
        /// Converts the specified string representation of a date and time to its <see cref="DateTime"/> equivalent.
        /// If the conversion fails, the method returns the current date.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The current date is obtained from the <see cref="TimeGenerator.GetToday"/>.
        /// This makes the method suitable for scenarios where taking control over date and time creation is necessary. Typical example would be mocking date and time generation for testing purposes.<br/>
        /// For more information see the <see cref="TimeGenerator"/> class.
        /// </para>
        /// <para>
        /// The method will try to convert the <paramref name="value"/> by using any of the standard .NET date and time format strings (see <see cref="StandardDateTimeFormats"/>).
        /// </para>
        /// <para>
        /// The method uses the current thread culture (<see cref="CultureInfo.CurrentCulture"/>) for providing culture-specific format information.
        /// </para>
        /// <para>
        /// Date and time are returned as a Coordinated Universal Time (UTC).
        /// If the <paramref name="value"/> denotes a local time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeLocal"/>,
        /// the date and time are converted from the local time to UTC.
        /// If the input string denotes a UTC time, through a time zone specifier or <see cref="DateTimeAssumption.AssumeUniversal"/>, no conversion occurs.
        /// </para>
        /// <para>
        /// The <see cref="DateTime.Kind"/> of the returned <see cref="DateTime"/> object is always <see cref="DateTimeKind.Utc"/>.
        /// </para>
        /// </remarks>
        /// <param name="value">A <see cref="string"/> containing the value to convert.</param>
        /// <returns>
        /// <see cref="DateTime"/> value equivalent to the <see cref="DateTime"/> contained in the <paramref name="value"/> if the conversion succeeded.
        /// <br/>-or-<br/>Current date if the <paramref name="value"/> is None option.
        /// <br/>-or-<br/>Current date if the conversion failed.<br/>
        /// In case of successful conversion, the returned <see cref="DateTime"/> object will have <see cref="DateTime.Kind"/> set to <see cref="DateTimeKind.Utc"/>.
        /// </returns>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/2h3syy57(v=vs.110).aspx">Parsing Date and Time Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx">Custom Date and Time Format Strings</seealso>
        public static DateTime ToDateTimeOrToday(Option<string> value)
        {
            return ToDateTime(value, null, CultureInfo.CurrentCulture, DateTimeAssumption.AssumeUniversal).GetValueOrDefault(TimeGenerator.GetToday());
        }
    }
}
