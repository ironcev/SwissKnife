using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SwissKnife.Diagnostics.Contracts;

namespace SwissKnife.Collections
{
    /// <summary>
    /// Contains extension methods that can be applied on different types of collections.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Adds <paramref name="itemsToAdd"/> to the <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="T">The type of the items in the <paramref name="collection"/>.</typeparam>
        /// <param name="collection">The <see cref="ICollection{T}"/> to which to add <paramref name="itemsToAdd"/>.</param>
        /// <param name="itemsToAdd">Items that has to be added to the <paramref name="collection"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is null.<br/>-or-<br/><paramref name="itemsToAdd"/> is null.</exception>
        /// <exception cref="NotSupportedException"><paramref name="collection"/> is read-only.</exception>
        public static void AddMany<T>(this ICollection<T> collection, IEnumerable<T> itemsToAdd)
        {
            #region Preconditions
            Argument.IsNotNull(collection, "collection");
            Argument.IsNotNull(itemsToAdd, "itemsToAdd");
            #endregion

            foreach (T item in itemsToAdd)
                collection.Add(item);
        }

        /// <summary>
        /// Gets the value from the <paramref name="dictionary"/> associated with the specified <paramref name="key"/>. If the value does not exist in the <paramref name="dictionary"/> it will be added to it.
        /// </summary>
        /// <remarks>
        /// <p>
        /// If the <paramref name="dictionary"/> does not contain the <paramref name="key"/>, a new <see cref="KeyValuePair{TKey,TValue}"/> will be added to the <paramref name="dictionary"/>.
        /// The key and the value of that new <see cref="KeyValuePair{TKey,TValue}"/> will be set to <paramref name="key"/> and the result of the <paramref name="getValueToAdd"/> respectively.
        /// The <paramref name="getValueToAdd"/> is called only if the <paramref name="dictionary"/> does not already contain the <paramref name="key"/>.
        /// </p>
        /// <p>
        /// <b>Note</b>
        /// If the <paramref name="getValueToAdd"/> throws any exception, that exception will be propagated to the caller.
        /// </p>
        /// </remarks>
        /// <typeparam name="TKey">The type of keys in the <paramref name="dictionary"/>.</typeparam>
        /// <typeparam name="TValue">The type of values in the <paramref name="dictionary"/>.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey,TValue}"/> from which the value has to be get.</param>
        /// <param name="key">The key associated with the value.</param>
        /// <param name="getValueToAdd">
        /// The function that returns the value that has to be inserted into the <paramref name="dictionary"/> if it does not contain the <paramref name="key"/>.
        /// This function will be called only if the <paramref name="dictionary"/> does not already contain the <paramref name="key"/>.
        /// </param>
        /// <returns>
        /// Already existing value associated to the <paramref name="key"/> or the newly added value returned by the <paramref name="getValueToAdd"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="dictionary"/> is null.<br/>-or-<br/><paramref name="key"/> is null.<br/>-or-<br/><paramref name="getValueToAdd"/> is null.</exception>
        /// <exception cref="NotSupportedException"><paramref name="dictionary"/> is read-only.</exception>
        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> getValueToAdd)
        {
            #region Preconditions
            Argument.IsNotNull(dictionary, "dictionary");
            Argument.IsNotNull(key, "key");
            Argument.IsNotNull(getValueToAdd, "getValueToAdd");
            #endregion

            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, getValueToAdd());

            return dictionary[key];
        }

        /// <summary>
        /// Splits <see cref="IEnumerable{T}"/> into groups of specified size.
        /// </summary>
        /// <remarks>
        /// <p>
        /// This method is implemented by using deferred execution. The immediate return value is an object that stores all the information that is required to perform the action.
        /// The query represented by this method is not executed until the object is enumerated either by calling its <b>GetEnumerator</b> method directly or by using <b>foreach</b>
        /// in Visual C# or <b>For Each</b> in Visual Basic.
        /// </p>
        /// <p>
        /// The <paramref name="groupSize"/> can be greater than the number of elements in the <paramref name="source"/>. In that case, the result contains only one group which is the same as the <paramref name="source"/>.
        /// </p>
        /// <p>
        /// Splitting preserves the order of the elements.
        /// </p>
        /// </remarks>
        /// <typeparam name="T">The type of the elements contained in the <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}"/> to split into groups.</param>
        /// <param name="groupSize">The number of elements in each group except eventually the last one. The last group can have less elements than the group size.</param>
        /// <returns>Enumerable whose each element is an <see cref="IEnumerable{T}"/> that represents a single group.<br/>-or-<br/>Empty enumerable if the <paramref name="source"/> is empty.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="groupSize"/> is not greater than zero.</exception>
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int groupSize)
        {
            #region Preconditions
            Argument.IsNotNull(source, "source");
            Argument.IsGreaterThanZero(groupSize, "groupSize");
            #endregion

            // The source is implicitly captured in a closure. This is exactly what we want, because we want to have method implemented by using deferred execution.
            // The method actually enumerates the source several times, first time to get the first element in each group and later on to take the members of each group.
            // This is an acceptable trade-off keeping in mind that we easily got implementation with deferred execution.
            // ReSharper disable PossibleMultipleEnumeration
            return source.Where((x, i) => i % groupSize == 0).Select((x, i) => source.Skip(i * groupSize).Take(groupSize));
            // ReSharper restore PossibleMultipleEnumeration
        }

        // It's perfectly fine if different threads start with the same seed (in case that the Random objects are created very shortly one ofter another).
        private static readonly ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random());
        /// <summary>
        /// Randomizes the order of elements in <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <remarks>
        /// <p>
        /// This method is implemented by using deferred execution. The immediate return value is an object that stores all the information that is required to perform the action.
        /// The query represented by this method is not executed until the object is enumerated either by calling its <b>GetEnumerator</b> method directly or by using <b>foreach</b>
        /// in Visual C# or <b>For Each</b> in Visual Basic.
        /// </p>
        /// <p>
        /// The method does not guarantee that the order of elements in the returned sequence will always be different than the order in the original sequence.
        /// If the sequence has exactly one element, the order will be the same in both the <paramref name="source"/> and the returned value.
        /// In case of small number of elements in the <paramref name="source"/>, like two or three, there is a probability of getting back the same order of elements like in the <paramref name="source"/>.
        /// This probability drops rapidly as the number of elements grows.
        /// </p>
        /// </remarks>
        /// <typeparam name="T">The type of the elements contained in the <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}"/> to randomize.</param>
        /// <returns>Randomized <see cref="IEnumerable{T}"/>.</returns>
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
        {
            #region Preconditions
            Argument.IsNotNull(source, "source");
            #endregion
            
            return source.OrderBy(x => random.Value.Next());
        }

        /// <summary>
        /// Splits <see cref="IEnumerable{T}"/> into specified number of groups.
        /// </summary>
        /// <remarks>
        /// <p>
        /// This method is implemented by using deferred execution. The immediate return value is an object that stores all the information that is required to perform the action.
        /// The query represented by this method is not executed until the object is enumerated either by calling its <b>GetEnumerator</b> method directly or by using <b>foreach</b>
        /// in Visual C# or <b>For Each</b> in Visual Basic.
        /// </p>
        /// <p>
        /// The <paramref name="numberOfGroups"/> can be greater than or equal to the number of elements in the <paramref name="source"/>.
        /// In both case, the result contains groups of size 1.
        /// The number of returned groups will be equal to the number of elements in the <paramref name="source"/>.
        /// </p>
        /// <p>
        /// All groups except maybe the last one will have same number of elements. The last group can have less elements than other groups.
        /// </p>
        /// <p>
        /// Splitting preserves the order of the elements.
        /// </p>
        /// </remarks>
        /// <typeparam name="T">The type of the elements contained in the <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}"/> to split into groups.</param>
        /// <param name="numberOfGroups">The number of resulting groups. The exact number of resulting is either equal to this value or to the number of elements in the <paramref name="source"/>.</param>
        /// <returns>Enumerable whose each element is an <see cref="IEnumerable{T}"/> that represents a single group.<br/>-or-<br/>Empty enumerable if the <paramref name="source"/> is empty.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="numberOfGroups"/> is not greater than zero.</exception>
        public static IEnumerable<IEnumerable<T>> SplitByNumberOfGroups<T>(this IEnumerable<T> source, int numberOfGroups)
        {
            #region Preconditions
            Argument.IsNotNull(source, "source");
            Argument.IsGreaterThanZero(numberOfGroups, "numberOfGroups");            
            #endregion

            List<T> sourceAsList = source.ToList();
            int numberOfElements = sourceAsList.Count;

            if (numberOfElements == 0)
                yield break;

            // We cannot have more groups then elements.
            numberOfGroups = Math.Min(numberOfElements, numberOfGroups);

            // Since numberOfGroups is greater than zero and numberOfElements is greater than zero,
            // we know that newly calculated numberOfGroups will never be zero.
            // Therefore, the below devision will never be devision by zero.

            int groupSize = (int)Math.Ceiling(numberOfElements / (double)numberOfGroups);

            foreach (var group in Split(sourceAsList, groupSize))
                yield return group;
        }
    }
}
