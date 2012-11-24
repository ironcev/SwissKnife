using System;
using System.Collections.Generic;
using HumblePractices.Contracts;

namespace HumblePractices.Collections
{
    /// <summary>
    /// Contains extension methods that can be applyed on different types of collections.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Executes the <paramref name="action"/> on each element of the <paramref name="enumerable"/>.
        /// </summary>
        /// <param name="enumerable">The enumerable to execute the <paramref name="action"/> on.</param>
        /// <param name="action">The action to execute on the each element of the <paramref name="enumerable"/>.</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            #region Preconditions
            Argument.IsNotNull(enumerable, "enumerable");
            Argument.IsNotNull(action, "action");
            #endregion

            foreach (T element in enumerable)
                action(element);
        }
    }
}
