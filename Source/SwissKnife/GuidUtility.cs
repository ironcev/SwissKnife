using System;
using System.Text;

namespace SwissKnife
{
    /// <summary>
    /// Contains utility methods for working with globally unique identifiers (GUIDs).
    /// </summary>
    public static class GuidUtility
    {
        /// <summary>
        /// Creates new sequential <see cref="Guid"/>.
        /// </summary>
        /// <remarks>
        /// <p>
        /// <see cref="NewSequentialGuid"/> method uses an algorithm originally suggested in 
        /// <a href="http://www.informit.com/articles/article.asp?p=25862">Jimmy Nilsson's article</a> on <a href="http://www.informit.com">informit.com</a>.
        /// </p>
        /// <p>
        /// This algorithm is known as the <b>comb algorithm</b>. It is designed to make the use of GUIDs as database primary keys, foreign keys, 
        /// and indexes nearly as efficient as <see cref="Int32"/>.
        /// </p>
        /// </remarks>
        /// <returns>
        /// Sequential <see cref="Guid"/>.
        /// </returns>
        public static Guid NewSequentialGuid()
        {
            // This code is taken one-to-one from NHibernate/Id/GuidCombGenerator.cs
            // (https://github.com/nhibernate/nhibernate-core/tree/d6fa919f77ab86fbd2901a0ecd568416265e5465/src/NHibernate/Id/GuidCombGenerator.cs).

            byte[] guidArray = Guid.NewGuid().ToByteArray();

            DateTime baseDate = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;

            // Get the days and milliseconds which will be used to build the byte string.
            TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan milliseconds = now.TimeOfDay;

            // Convert them to byte arrays.
            // Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333.
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(milliseconds.TotalMilliseconds / 3.333333));

            // Reverse the bytes to match SQL Server's ordering.
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            // Copy the bytes into the GUID.
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

            return new Guid(guidArray);
        }


        /// <summary>
        /// Converts a <see cref="Guid"/> to its short string representation.
        /// </summary>
        /// <remarks>
        /// <p>
        /// A typical short string representation of a <see cref="Guid"/> looks like this one "K-ROb0hYA0KbtCGZpBVv2g".
        /// </p>
        /// <p>
        /// Short string representations always have the length of 22 characters and can contain only the following characters:<br/>
        /// <ul>
        /// <li>uppercase characters "A" to "Z"</li>
        /// <li>the lowercase characters "a" to "z"</li>
        /// <li>the numerals "0" to "9"</li>
        /// <li>the symbols "_" and "-"</li>
        /// </ul>
        /// </p>
        /// </remarks>
        /// <param name="guid">The <see cref="Guid"/> that has to be converted to its short string representation.</param>
        /// <returns>
        /// The short string representation of the <paramref name="guid"/>.
        /// </returns>
        public static string ToShortString(Guid guid)
        {
            return Convert.ToBase64String(guid.ToByteArray()) // Returns something similar to "K/ROb0hYA0KbtCGZpBVv2g==".
                            // The value will always have length of 24 characters and two trailing "==".
                            // Also, it can contain the "/" and "+" signs.
                            .Substring(0, 22)
                            .Replace('/', '_')
                            .Replace('+', '-');
        }

        /// <summary>
        /// Creates new <see cref="Guid"/> out of its short string representation.
        /// </summary>
        /// <param name="shortStringGuidRepresentation">Short string representation of a <see cref="Guid"/>.</param>
        /// <returns>
        /// <see cref="Guid"/> represented by the <paramref name="shortStringGuidRepresentation"/>.
        /// <br/>-or-<br/>
        /// Null if the <paramref name="shortStringGuidRepresentation"/> is null or does not represent a valid short string GUID representation.
        /// </returns>
        public static Guid? FromShortString(Option<string> shortStringGuidRepresentation)
        {
            if (shortStringGuidRepresentation.IsNone) return null;

            if (shortStringGuidRepresentation.Value.Length != 22) return null;

            StringBuilder sb = new StringBuilder(shortStringGuidRepresentation.Value);
            sb.Replace('_', '/');
            sb.Replace('-', '+');
            sb.Append("==");

            try
            {
                return new Guid(Convert.FromBase64String(sb.ToString()));
            }
            catch
            {
                return null;
            }
        }
    }
}
