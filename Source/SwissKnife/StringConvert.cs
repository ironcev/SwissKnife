namespace SwissKnife
{
    /// <summary>
    /// Contains methods that convert a <see cref="string"/> to another base data type. All the methods are guaranteed not to throw exceptions.
    /// If the conversion is not possible, a fall-back value defined by the caller is returned.
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
            // The TryParse() fails if the string parameter is null, is not of the correct format, or represents a number less than int.MinValue or greater than int.MaxValue.
            // Long story short, we don't need additional check if the Option is None.
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
            // The TryParse() fails if the string parameter is null, is not of the correct format, or represents a number less than int.MinValue or greater than int.MaxValue.
            // Long story short, we don't need additional check if the Option is None.
            int result;
            return int.TryParse(value.ValueOrNull, out result) ? result : defaultValue;
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
            // The TryParse() fails if the string parameter is null, is not of the correct format, or represents a number less than long.MinValue or greater than long.MaxValue.
            // Long story short, we don't need additional check if the Option is None.
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
            // The TryParse() fails if the string parameter is null, is not of the correct format, or represents a number less than long.MinValue or greater than long.MaxValue.
            // Long story short, we don't need additional check if the Option is None.
            long result;
            return long.TryParse(value.ValueOrNull, out result) ? result : defaultValue;
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
    }
}
