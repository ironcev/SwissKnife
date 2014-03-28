using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
