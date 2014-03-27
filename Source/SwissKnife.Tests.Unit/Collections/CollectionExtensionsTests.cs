using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using NUnit.Framework;
using SwissKnife.Collections;

namespace SwissKnife.Tests.Unit.Collections
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class CollectionExtensionsTests
    {
        #region AddMany<T>
        [Test]
        public void AddMany_CollectionIsNull_ThrowsException()
        {
            string parameterName = Assert.Throws<ArgumentNullException>(() => CollectionExtensions.AddMany(null, new object[0])).ParamName;
            Assert.That(parameterName, Is.EqualTo("collection"));
        }

        [Test]
        public void AddMany_ItemsToAddIsNull_ThrowsException()
        {
            string parameterName = Assert.Throws<ArgumentNullException>(() => new List<object>().AddMany(null)).ParamName;
            Assert.That(parameterName, Is.EqualTo("itemsToAdd"));
        }

        [Test]
        public void AddMany_CollectionIsEmptyItemsToAddIsEmpty_DoesNotAddAnything()
        {
            var collection = new List<object>();
            collection.AddMany(new object[0]);
            CollectionAssert.IsEmpty(collection);
        }

        [Test]
        public void AddMany_CollectionIsNotEmptyItemsToAddIsEmpty_DoesNotAddAnything()
        {
            var originalCollectionContent = new List<object> { new object() };
            var collection = new List<object>(originalCollectionContent);
            collection.AddMany(new object[0]);
            CollectionAssert.AreEqual(originalCollectionContent, collection);
        }

        [Test]
        public void AddMany_CollectionIsEmptyItemsToAddIsNotEmpty_AddsItemsToAdd()
        {
            var collection = new List<object>();
            var itemsToAdd = new [] {new object()};
            collection.AddMany(itemsToAdd);
            CollectionAssert.AreEqual(collection, itemsToAdd);
        }

        [Test]
        public void AddMany_CollectionIsNotEmptyItemsToAddIsNotEmpty_AddsItemsToAdd()
        {
            var originalCollectionContent = new List<object> { new object() };
            var collection = new List<object>(originalCollectionContent);
            var itemsToAdd = new[] { new object() };
            collection.AddMany(itemsToAdd);
            CollectionAssert.AreEqual(originalCollectionContent.Concat(itemsToAdd), collection);
        }
        #endregion

        #region GetValue<TKey, TValue>
        [Test]
        public void GetValue_DictionaryIsNull_ThrowsException()
        {
            string parameterName = Assert.Throws<ArgumentNullException>(() => CollectionExtensions.GetValue(null, new object(), () => new object())).ParamName;
            Assert.That(parameterName, Is.EqualTo("dictionary"));
        }

        [Test]
        public void GetValue_KeyIsNull_ThrowsException()
        {
            string parameterName = Assert.Throws<ArgumentNullException>(() => new Dictionary<object, object>().GetValue(null, () => new object())).ParamName;
            Assert.That(parameterName, Is.EqualTo("key"));
        }

        [Test]
        public void GetValue_GetValueToAddIsNull_ThrowsException()
        {
            string parameterName = Assert.Throws<ArgumentNullException>(() => new Dictionary<object, object>().GetValue(new object(), null)).ParamName;
            Assert.That(parameterName, Is.EqualTo("getValueToAdd"));
        }

        [Test]
        public void GetValue_DictionaryContainsKey_ReturnsExistingValue()
        {
            var key = new object();
            var value = new object();
            var dictionary = new Dictionary<object, object> { {key, value} };
            var result = dictionary.GetValue(key, () => null);
            Assert.That(result, Is.SameAs(value));
        }

        [Test]
        public void GetValue_DictionaryDoesNotContainKey_ReturnsResultOfGetValueToAdd()
        {
            var valueToAdd = new object();
            var result = new Dictionary<object, object>().GetValue(new object(), () => valueToAdd);
            Assert.That(result, Is.SameAs(valueToAdd));
        }

        [Test]
        public void GetValue_DictionaryContainsKey_GetValueToAddIsNotCalled()
        {
            int counter = 0;
            var key = new object();
            var dictionary = new Dictionary<object, object> { { key, null } };
            dictionary.GetValue(key, () =>
                                        {
                                            counter++;
                                            return null;
                                        });
            Assert.That(counter, Is.EqualTo(0));
        }
        #endregion

        #region Split<T>
        [Test]
        public void Split_SourceIsNull_ThrowsException()
        {
            string parameterName = Assert.Throws<ArgumentNullException>(() => CollectionExtensions.Split<object>(null, 1)).ParamName;
            Assert.That(parameterName, Is.EqualTo("source"));
        }

        [Test]
        public void Split_GroupSizeIsLessThanZero_ThrowsException()
        {
            string parameterName = Assert.Throws<ArgumentOutOfRangeException>(() => CollectionExtensions.Split(Enumerable.Empty<object>(), -1)).ParamName;
            Assert.That(parameterName, Is.EqualTo("groupSize"));
        }

        [Test]
        public void Split_GroupSizeIsZero_ThrowsException()
        {
            string parameterName = Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Empty<object>().Split(0)).ParamName;
            Assert.That(parameterName, Is.EqualTo("groupSize"));
        }

        [Test]
        public void Split_SourceIsEmpty_ReturnsEmptyEnumerable()
        {
            var result = Enumerable.Empty<object>().Split(1).ToList();

            CollectionAssert.IsEmpty(result);
        }

        [Test]
        public void Split_GroupSizeIsSameAsSourceCount_ReturnsSingleGroupSameAsSource()
        {
            var source = new [] { 1, 2, 3 };
            var result = source.Split(source.Length).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(result[0], source);
        }

        [Test]
        public void Split_GroupSizeIsGreaterAsSourceCount_ReturnsSingleGroupSameAsSource()
        {
            var source = new[] { 1, 2, 3 };
            var result = source.Split(source.Length + 1).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(result[0], source);
        }

        [Test]
        public void Split_GroupSizeIsMultipleOfSourceCount_ReturnsGroupsOfSameSize()
        {
            var source = new [] { 1, 2, 3, 4, 5, 6 };
            var result = source.Split(source.Length / 2).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
            CollectionAssert.AreEqual(result[0], source.Take(3));
            CollectionAssert.AreEqual(result[1], source.Skip(3).Take(3));
            CollectionAssert.AreEqual(result.SelectMany(group => group.ToList()), source);
        }

        [Test]
        public void Split_GroupSizeIsNotMultipleOfSourceCount_LastGroupHasSmallerSize()
        {
            var source = new[] { 1, 2, 3, 4, 5, 6, 7 };
            var result = source.Split(source.Length / 2).ToList();

            Assert.That(result.Count, Is.EqualTo(3));
            CollectionAssert.AreEqual(result[0], source.Take(3));
            CollectionAssert.AreEqual(result[1], source.Skip(3).Take(3));
            CollectionAssert.AreEqual(result[2], source.Skip(6).Take(3));
            CollectionAssert.AreEqual(result.SelectMany(group => group.ToList()), source);
        }

        [Test]
        public void Split_DefersExecution()
        {
            // ReSharper disable PossibleMultipleEnumeration
            var source = new List<int>();

            var query = source.Split(3);

            source.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7 });

            var result = query.ToList();

            Assert.That(result.Count, Is.EqualTo(3));
            CollectionAssert.AreEqual(result[0], source.Take(3));
            CollectionAssert.AreEqual(result[1], source.Skip(3).Take(3));
            CollectionAssert.AreEqual(result[2], source.Skip(6).Take(3));
            CollectionAssert.AreEqual(result.SelectMany(group => group.ToList()), source);

            source.AddRange(new[] { 8, 9, 10 });

            result = query.ToList();

            Assert.That(result.Count, Is.EqualTo(4));
            CollectionAssert.AreEqual(result[0], source.Take(3));
            CollectionAssert.AreEqual(result[1], source.Skip(3).Take(3));
            CollectionAssert.AreEqual(result[2], source.Skip(6).Take(3));
            CollectionAssert.AreEqual(result[3], source.Skip(9).Take(3));
            CollectionAssert.AreEqual(result.SelectMany(group => group.ToList()), source);
            // ReSharper restore PossibleMultipleEnumeration
        }
        #endregion

        #region Randomize<T>
        [Test]
        public void Randomize_SourceIsNull_ThrowsException()
        {
            string parameterName = Assert.Throws<ArgumentNullException>(() => CollectionExtensions.Randomize<object>(null)).ParamName;
            Assert.That(parameterName, Is.EqualTo("source"));
        }

        [Test]
        public void Randomize_SourceIsEmpty_ReturnsEmptyEnumerable()
        {
            CollectionAssert.IsEmpty(Enumerable.Empty<object>().Randomize());
        }

        [Test]
        public void Randomize_SourceHasOneElement_ReturnsEnumerableWithOnlyThatElement()
        {
            var source = new[] { 1 };

            CollectionAssert.AreEqual(source.Randomize(), source);
        }

        [Test]
        public void Randomize_ReturnsEquivalentEnumerable()
        {
            var source = new[] { 1, 2, 3 };

            CollectionAssert.AreEquivalent(source.Randomize(), source);
        }

        [Test]
        public void Randomize_ReturnsRandomizedEnumerable()
        {
            AssertThatRandomizeReturnsRandomizedEnumerable(Enumerable.Range(0, 100));
        }

        [Test]
        public void Randomize_IsThreadSafe()
        {
            // Let's run the previous test on 100 concurrent threads.
            for (int i = 0; i < 100; i++)
                new Thread(() => AssertThatRandomizeReturnsRandomizedEnumerable(Enumerable.Range(0, 100))).Start();
        }

        /// <remarks>
        /// Strictly seen, this test is not deterministic and it could fail even if the <see cref="CollectionExtensions.Randomize{T}"/> method works properly.
        /// The method does not guarantee that the new sequence will have different order of elements than the original one.
        /// However, for "large enough" number of elements, we expect this to always be the case.
        /// </remarks>
        private static void AssertThatRandomizeReturnsRandomizedEnumerable(IEnumerable<int> originalEnumerable)
        {
            var previousSource = originalEnumerable.ToArray();
            for (int i = 0; i < 20; i++) // Let's create 20 subsequent randomized results and ensure that each has different order than its source (the previous one)                
            {
                var randomized = previousSource.Randomize().ToArray();
                CollectionAssert.AreEquivalent(previousSource, randomized);
                CollectionAssert.AreNotEqual(previousSource, randomized);
            }                        
        }

        [Test]
        public void Randomize_DefersExecution()
        {
            // ReSharper disable PossibleMultipleEnumeration
            var source = new List<int>();

            var query = source.Randomize();

            source.AddRange(new[] { 1, 2, 3 });

            var result = query.ToList();

            CollectionAssert.AreEquivalent(source, result);

            source.AddRange(new[] { 4, 5, 6 });

            result = query.ToList();

            CollectionAssert.AreEquivalent(source, result);
            // ReSharper restore PossibleMultipleEnumeration
        }
        #endregion
    }
    // ReSharper restore InconsistentNaming
}
