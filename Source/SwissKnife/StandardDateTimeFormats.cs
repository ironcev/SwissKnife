using System.Linq;
using System.Reflection;

namespace SwissKnife
{
    /// <summary>
    /// Provides standard .NET date and time format strings.
    /// For more information on standard date and time formats see <a href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</a>.
    /// </summary>
    /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
    public static class StandardDateTimeFormats
    {
        /// <summary>
        /// Short date pattern ("d"). For example: 6/15/2009 (en-US).
        /// </summary>
        /// <remarks>
        /// For more information see: <a href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#ShortDate">The Short Date ("d") Format Specifier</a>.
        /// </remarks>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#ShortDate">The Short Date ("d") Format Specifier</seealso>
        public const string ShortDate = "d";

        /// <summary>
        /// Long date pattern ("D"). For example: Monday, June 15, 2009 (en-US).
        /// </summary>
        /// <remarks>
        /// For more information see: <a href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#LongDate">The Long Date ("D") Format Specifier</a>.
        /// </remarks>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#LongDate">The Long Date ("D") Format Specifier</seealso>
        public const string LongDate = "D";

        /// <summary>
        /// Full date short time pattern ("f"). For example: Monday, June 15, 2009 1:45 PM (en-US).
        /// </summary>
        /// <remarks>
        /// For more information see: <a href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#FullDateShortTime">The Full Date Short Time ("f") Format Specifier</a>.
        /// </remarks>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#FullDateShortTime">The Full Date Short Time ("f") Format Specifier</seealso>
        public const string FullDateShortTime = "f";

        /// <summary>
        /// Full date long time pattern ("F"). For example: Monday, June 15, 2009 1:45:30 PM (en-US).
        /// </summary>
        /// <remarks>
        /// For more information see: <a href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#FullDateLongTime">The Full Date Long Time ("F") Format Specifier</a>.
        /// </remarks>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#FullDateLongTime">The Full Date Long Time ("F") Format Specifier</seealso>
        public const string FullDateLongTime = "F";

        /// <summary>
        /// General date short time pattern ("g"). For example: 6/15/2009 1:45 PM (en-US).
        /// </summary>
        /// <remarks>
        /// For more information see: <a href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#GeneralDateShortTime">The General Date Short Time ("g") Format Specifier</a>.
        /// </remarks>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#GeneralDateShortTime">The General Date Short Time ("g") Format Specifier</seealso>
        public const string GeneralDateShortTime = "g";

        /// <summary>
        /// General date long time pattern ("G"). For example: 6/15/2009 1:45:30 PM (en-US).
        /// </summary>
        /// <remarks>
        /// For more information see: <a href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#GeneralDateLongTime">The General Date Long Time ("G") Format Specifier</a>.
        /// </remarks>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#GeneralDateLongTime">The General Date Long Time ("G") Format Specifier</seealso>
        public const string GeneralDateLongTime = "G";

        /// <summary>
        /// Month day pattern ("m"). For example: June 15 (en-US).
        /// </summary>
        /// <remarks>
        /// For more information see: <a href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#MonthDay">The Month ("M", "m") Format Specifier</a>.
        /// </remarks>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#MonthDay">The Month ("M", "m") Format Specifier</seealso>
        public const string MonthDay = "m";

        /// <summary>
        /// Round-trip date time pattern ("o"). For example: 2009-06-15T13:45:30.0000000-07:00.
        /// </summary>
        /// <remarks>
        /// For more information see: <a href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#Roundtrip">The Round-trip ("O", "o") Format Specifier</a>.
        /// </remarks>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#Roundtrip">The Round-trip ("O", "o") Format Specifier</seealso>
        public const string Roundtrip = "o";

        /// <summary>
        /// RFC1123 pattern ("r"). For example: Mon, 15 Jun 2009 20:45:30 GMT.
        /// </summary>
        /// <remarks>
        /// For more information see: <a href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#RFC1123">The RFC1123 ("R", "r") Format Specifier</a>.
        /// </remarks>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#RFC1123">The RFC1123 ("R", "r") Format Specifier</seealso>
        public const string Rfc1123 = "r";

        /// <summary>
        /// Sortable date time pattern ("s"). For example: 2009-06-15T13:45:30.
        /// </summary>
        /// <remarks>
        /// For more information see: <a href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#Sortable">The Sortable ("s") Format Specifier</a>.
        /// </remarks>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#Sortable">The Sortable ("s") Format Specifier</seealso>
        public const string Sortable = "s";

        /// <summary>
        /// Short time pattern ("t"). For example: 1:45 PM (en-US).
        /// </summary>
        /// <remarks>
        /// For more information see: <a href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#ShortTime">The Short Time ("t") Format Specifier</a>.
        /// </remarks>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#ShortTime">The Short Time ("t") Format Specifier</seealso>
        public const string ShortTime = "t";

        /// <summary>
        /// Long time pattern ("T"). For example: 1:45:30 PM (en-US).
        /// </summary>
        /// <remarks>
        /// For more information see: <a href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#LongTime">The Long Time ("T") Format Specifier</a>.
        /// </remarks>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#LongTime">The Long Time ("T") Format Specifier</seealso>
        public const string LongTime = "T";

        /// <summary>
        /// Universal sortable date time pattern ("u"). For example: 2009-06-15 20:45:30Z.
        /// </summary>
        /// <remarks>
        /// For more information see: <a href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#UniversalSortable">The Universal Sortable ("u") Format Specifier</a>.
        /// </remarks>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#UniversalSortable">The Universal Sortable ("u") Format Specifier</seealso>
        public const string UniversalSortable = "u";

        /// <summary>
        /// Universal full date time pattern ("U"). For example: Monday, June 15, 2009 8:45:30 PM (en-US).
        /// </summary>
        /// <remarks>
        /// For more information see: <a href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#UniversalFull">The Universal Full ("U") Format Specifier</a>.
        /// </remarks>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#UniversalFull">The Universal Full ("U") Format Specifier</seealso>
        public const string UniversalFull = "U";

        /// <summary>
        /// Year month pattern ("y"). For example: June, 2009 (en-US).
        /// </summary>
        /// <remarks>
        /// For more information see: <a href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#YearMonth">The Year Month ("Y", "y") Format Specifier</a>.
        /// </remarks>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx">Standard Date and Time Format Strings</seealso>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#YearMonth">The Year Month ("Y", "y") Format Specifier</seealso>
        public const string YearMonth = "y";

        /// <summary>
        /// Gets all standard .NET date and time format strings.
        /// </summary>
        public static readonly string[] AllStandardDateTimeFormats;

        static StandardDateTimeFormats()
        {
            AllStandardDateTimeFormats = typeof(StandardDateTimeFormats)
                                            .GetFields(BindingFlags.Public | BindingFlags.Static)
                                            .Where(fieldInfo => fieldInfo.IsStatic && fieldInfo.FieldType == typeof(string))
                                            .Select(fieldInfo => fieldInfo.GetValue(null))
                                            .Cast<string>()
                                            .ToArray();
        }
    }
}
