using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            string parameterName = Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Empty<object>().Split(-1)).ParamName;
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
        public void Randomize_IsThreadSafe() // TODO-IG: This test is currently meaningless. Rewrite it once when the Conscen is implemented.
        {
            // Let's run the previous test on 100 concurrent threads.
            for (int i = 0; i < 100; i++)
                new Thread(Randomize_ReturnsRandomizedEnumerable).Start();
        }

        /// <remarks>
        /// Strictly seen, this test is not deterministic and it could fail even if the <see cref="CollectionExtensions.Randomize{T}"/> method works properly.
        /// The method does not guarantee that the new sequence will have different order of elements than the original one.
        /// However, for "large enough" number of elements, we expect this to always be the case.
        /// </remarks>
        private static void AssertThatRandomizeReturnsRandomizedEnumerable(IEnumerable<int> originalEnumerable)
        {
            var previousSource = originalEnumerable.ToArray();
            for (int i = 0; i < 20; i++) // Let's create 20 subsequent randomized results and ensure that each has different order than its source (the previous one).
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

        #region SplitByNumberOfGroups<T>
        [Test]
        public void SplitByNumberOfGroups_SourceIsNull_ThrowsException()
        {
            // ReSharper disable ReturnValueOfPureMethodIsNotUsed
            string parameterName = Assert.Throws<ArgumentNullException>(() => CollectionExtensions.SplitByNumberOfGroups<object>(null, 1).ToList()).ParamName;
            // ReSharper restore ReturnValueOfPureMethodIsNotUsed
            Assert.That(parameterName, Is.EqualTo("source"));
        }

        [Test]
        public void SplitByNumberOfGroups_NumberOfGroupsIsLessThanZero_ThrowsException()
        {
            // ReSharper disable ReturnValueOfPureMethodIsNotUsed
            string parameterName = Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Empty<object>().SplitByNumberOfGroups(-1).ToList()).ParamName;
            // ReSharper restore ReturnValueOfPureMethodIsNotUsed
            Assert.That(parameterName, Is.EqualTo("numberOfGroups"));
        }

        [Test]
        public void SplitByNumberOfGroups_NumberOfGroupsIsZero_ThrowsException()
        {
            // ReSharper disable ReturnValueOfPureMethodIsNotUsed
            string parameterName = Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Empty<object>().SplitByNumberOfGroups(0).ToList()).ParamName;
            // ReSharper restore ReturnValueOfPureMethodIsNotUsed
            Assert.That(parameterName, Is.EqualTo("numberOfGroups"));
        }

        [Test]
        public void SplitByNumberOfGroups_SourceIsEmpty_ReturnsEmptyEnumerable()
        {
            var result = Enumerable.Empty<object>().SplitByNumberOfGroups(1).ToList();

            CollectionAssert.IsEmpty(result);
        }

        [Test]
        public void SplitByNumberOfGroups_NumberOfGroupsIsOne_ReturnsSingleGroupSameAsSource()
        {
            var source = new[] { 1, 2, 3 };
            var result = source.SplitByNumberOfGroups(1).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(result[0], source);
        }

        [Test]
        public void SplitByNumberOfGroups_NumberOfGroupsIsSameAsSourceCount_ReturnsSingleElementGroups()
        {
            var source = new[] { 1, 2, 3 };
            var result = source.SplitByNumberOfGroups(source.Length).ToList();

            Assert.That(result.Count, Is.EqualTo(source.Length));
            foreach (var group in result)
                Assert.That(group.Count(), Is.EqualTo(1));
            CollectionAssert.AreEqual(result.SelectMany(group => group.ToList()), source);
        }

        [Test]
        public void SplitByNumberOfGroups_NumberOfGroupsIsGreaterThanSourceCount_ReturnsSingleElementGroups()
        {
            var source = new[] { 1, 2, 3 };
            var result = source.SplitByNumberOfGroups(source.Length + 1).ToList();

            Assert.That(result.Count, Is.EqualTo(source.Length));
            foreach (var group in result)
                Assert.That(group.Count(), Is.EqualTo(1));
            CollectionAssert.AreEqual(result.SelectMany(group => group.ToList()), source);
        }

        [Test]
        public void SplitByNumberOfGroups__NumberOfGroupsIsLessThenSourceCount_And_SourceCountIsMultipleOfNumberOfGroups__ReturnsGroupsOfSameSize()
        {
            var source = new[] { 1, 2, 3, 4, 5, 6 };
            var result = source.SplitByNumberOfGroups(source.Length / 3).ToList(); // 6 / 3 = 2 groups

            Assert.That(result.Count, Is.EqualTo(2));
            CollectionAssert.AreEqual(result[0], source.Take(3));
            CollectionAssert.AreEqual(result[1], source.Skip(3).Take(3));
            CollectionAssert.AreEqual(result.SelectMany(group => group.ToList()), source);
        }

        [Test]
        public void SplitByNumberOfGroups__NumberOfGroupsIsLessThenSourceCount_And_SourceCountIsNotMultipleOfNumberOfGroups__LastGroupHasSmallerSize()
        {
            var source = new[] { 1, 2, 3, 4, 5, 6, 7 };
            var result = source.SplitByNumberOfGroups(3).ToList(); // {1,2,3} {4,5,6} {7}

            Assert.That(result.Count, Is.EqualTo(3));
            CollectionAssert.AreEqual(result[0], source.Take(3));
            CollectionAssert.AreEqual(result[1], source.Skip(3).Take(3));
            CollectionAssert.AreEqual(result[2], source.Skip(6).Take(3));
            Assert.That(result[0].Count(), Is.EqualTo(result[1].Count()));
            Assert.That(result[2].Count(), Is.LessThan(result[0].Count()));
            CollectionAssert.AreEqual(result.SelectMany(group => group.ToList()), source);
        }

        [Test]
        public void SplitByNumberOfGroups_DefersExecution()
        {
            // ReSharper disable PossibleMultipleEnumeration
            var source = new List<int>();

            var query = source.SplitByNumberOfGroups(3);

            source.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7 });

            var result = query.ToList();

            Assert.That(result.Count, Is.EqualTo(3));
            CollectionAssert.AreEqual(result[0], source.Take(3));
            CollectionAssert.AreEqual(result[1], source.Skip(3).Take(3));
            CollectionAssert.AreEqual(result[2], source.Skip(6).Take(3));
            CollectionAssert.AreEqual(result.SelectMany(group => group.ToList()), source);

            source.AddRange(new[] { 8, 9, 10 });

            result = query.ToList();

            Assert.That(result.Count, Is.EqualTo(3));
            CollectionAssert.AreEqual(result[0], source.Take(4));
            CollectionAssert.AreEqual(result[1], source.Skip(4).Take(4));
            CollectionAssert.AreEqual(result[2], source.Skip(8).Take(4));
            CollectionAssert.AreEqual(result.SelectMany(group => group.ToList()), source);
            // ReSharper restore PossibleMultipleEnumeration
        }
        #endregion

        #region ToEnumerable<T>
        [Test]
        public void ToEnumerableOfT_EnumeratorIsNull_ThrowsException()
        {
            // ReSharper disable ReturnValueOfPureMethodIsNotUsed
            string parameterName = Assert.Throws<ArgumentNullException>(() => CollectionExtensions.ToEnumerable<object>(null).ToList()).ParamName;
            // ReSharper restore ReturnValueOfPureMethodIsNotUsed
            Assert.That(parameterName, Is.EqualTo("enumerator"));   
        }

        [Test]
        public void ToEnumerableOfT_EmptyEnumerator_ReturnsEmptyEnumerable()
        {
            CollectionAssert.IsEmpty(Enumerable.Empty<object>().GetEnumerator().ToEnumerable());
        }

        [Test]
        public void ToEnumerableOfT_ReturnsEnumeratedEnumerable()
        {
            var enumerable = new List<int> { 1, 2, 3 };

            CollectionAssert.AreEqual(enumerable, enumerable.GetEnumerator().ToEnumerable());
        }

        [Test]
        public void ToEnumerableOfT_RepedetlyCalledOnSameUnmodifedCollection_ReturnsEnumeratedEnumerable()
        {
            var enumerable = new List<int> { 1, 2, 3 };

            var enumerator = enumerable.GetEnumerator();

            CollectionAssert.AreEqual(enumerable, enumerator.ToEnumerable());
            CollectionAssert.AreEqual(enumerable, enumerator.ToEnumerable());
            CollectionAssert.AreEqual(enumerable, enumerator.ToEnumerable());
        }

        [Test]
        public void ToEnumerableOfT_CollectionWasModified_ThrowsException()
        {
            // ReSharper disable PossibleMultipleEnumeration
            var enumerable = new List<int>();

            var enumerator = enumerable.GetEnumerator();

            enumerable.AddRange(new [] { 1, 2, 3 });

            string exceptionMessage = Assert.Throws<InvalidOperationException>(() => enumerator.ToEnumerable()).Message;
            Assert.That(exceptionMessage.Contains("Collection was modified"));
        }

        #endregion

        #region ToEnumerable
        [Test]
        public void ToEnumerable_EnumeratorIsNull_ThrowsException()
        {
            // ReSharper disable ReturnValueOfPureMethodIsNotUsed
            string parameterName = Assert.Throws<ArgumentNullException>(() => CollectionExtensions.ToEnumerable(null).OfType<object>().ToList()).ParamName;
            // ReSharper restore ReturnValueOfPureMethodIsNotUsed
            Assert.That(parameterName, Is.EqualTo("enumerator"));
        }

        [Test]
        public void ToEnumerable_EmptyEnumerator_ReturnsEmptyEnumerable()
        {
            CollectionAssert.IsEmpty(new ArrayList().GetEnumerator().ToEnumerable());
        }

        [Test]
        public void ToEnumerable_ReturnsEnumeratedEnumerable()
        {
            ArrayList enumerable = new ArrayList { 1, 2, 3 };

            CollectionAssert.AreEqual(enumerable, enumerable.GetEnumerator().ToEnumerable());
        }

        [Test]
        public void ToEnumerable_RepedetlyCalledOnSameUnmodifedCollection_ReturnsEnumeratedEnumerable()
        {
            ArrayList enumerable = new ArrayList { 1, 2, 3 };

            var enumerator = enumerable.GetEnumerator();

            CollectionAssert.AreEqual(enumerable, enumerator.ToEnumerable());
            CollectionAssert.AreEqual(enumerable, enumerator.ToEnumerable());
            CollectionAssert.AreEqual(enumerable, enumerator.ToEnumerable());
        }

        [Test]
        public void ToEnumerable_CollectionWasModified_ThrowsException()
        {
            // ReSharper disable PossibleMultipleEnumeration
            ArrayList enumerable = new ArrayList();

            var enumerator = enumerable.GetEnumerator();

            enumerable.AddRange(new[] { 1, 2, 3 });

            string exceptionMessage = Assert.Throws<InvalidOperationException>(() => enumerator.ToEnumerable()).Message;
            Assert.That(exceptionMessage.Contains("Collection was modified"));
        }

        #endregion

        #region Random<T> where T : class
        [Test]
        public void Random_SourceIsNull_ThrowsException()
        {
            string parameterName = Assert.Throws<ArgumentNullException>(() => CollectionExtensions.Random<object>(null, o => true)).ParamName;
            Assert.That(parameterName, Is.EqualTo("source"));
        }

        [Test]
        public void Random_PredicateIsNull_ThrowsException()
        {
            string parameterName = Assert.Throws<ArgumentNullException>(() => Enumerable.Empty<object>().Random(null)).ParamName;
            Assert.That(parameterName, Is.EqualTo("predicate"));
        }

        [Test]
        public void Random_SourceIsEmpty_ReturnsNone()
        {            
            Assert.That(Enumerable.Empty<object>().Random(o => true).IsNone);
        }

        [Test]
        public void Random__SourceHasOneElement_And_ElementSatisfiesPredicate__ReturnsThatElement()
        {
            var source = new[] { new object() };
            Assert.That(source.Random(o => true).Value, Is.SameAs(source[0]));
        }

        [Test]
        public void Random__SourceHasMoreThanOneElement_And_AllElementsSatisfyPredicate__ReturnsAnElementFromTheSource()
        {
            var source = new[] { new object(), new object(), new object() };
            CollectionAssert.Contains(source, source.Random(o => true).Value);
        }

        [Test]
        public void Random__SourceHasMoreThanOneElement_And_SomeElementsSatisfyPredicate__ReturnsAnElementFromTheSourceThatSatisfyPredicate()
        {
            var source = new [] { new object(), new object(), new object() };
            var elementsThatSatisfyPredicate = new[] { source[0], source[2] };
            CollectionAssert.Contains(elementsThatSatisfyPredicate, source.Random(elementsThatSatisfyPredicate.Contains).Value);
        }

        [Test]
        public void Random__SourceHasOneElement_And_ElementDoesNotSatisfyPredicate__ReturnsNone()
        {
            var source = new[] { new object() };
            Assert.That(source.Random(o => false).IsNone);
        }

        [Test]
        public void Random__SourceHasMoreThanOneElement_And_AllElementsDoNotSatisfyPredicate__ReturnsNone()
        {
            var source = new[] { new object(), new object(), new object() };
            Assert.That(source.Random(o => false).IsNone);
        }

        [Test]
        public void Random_ReturnsRandomElement()
        {
            AssertThatRandomReturnsRandomElement(CreateLargeCollectionOfDifferentObjects());
        }

        public static IEnumerable<object> CreateLargeCollectionOfDifferentObjects()
        {
            for (int i = 0; i < 1000; i++) yield return new object();
        }

        [Test]
        public void Random_IsThreadSafe() // TODO-IG: This test is currently meaningless. Rewrite it once when the Conscen is implemented.
        {
            // Let's run the previous test on 100 concurrent threads.
            for (int i = 0; i < 100; i++)
                new Thread(Random_ReturnsRandomElement).Start();
        }

        /// <remarks>
        /// Strictly seen, this test is not deterministic and it could fail even if the <see cref="CollectionExtensions.Random{T}(IEnumerable{T}, Predicate{T})"/> method works properly.
        /// The method does not guarantee that the next random value obtained from the same collection will be different than the previous one.
        /// However, for "large enough" number of elements that are all different, we expect this to always be the case.
        /// </remarks>
        private static void AssertThatRandomReturnsRandomElement(IEnumerable<object> enumerable)
        {
            var previousRandom = enumerable.Random(o => true);
            for (int i = 0; i < 20; i++) // Let's get 20 subsequent random elements and ensure that each is different then the previous one.
            {
                var actualRandom = enumerable.Random(o => true);
                Assert.That(actualRandom, Is.Not.EqualTo(previousRandom));
            }
        }
        #endregion

        #region RandomElement<T> where T : struct
        [Test]
        public void RandomElement_SourceIsNull_ThrowsException()
        {
            string parameterName = Assert.Throws<ArgumentNullException>(() => CollectionExtensions.RandomElement<int>(null, o => true)).ParamName;
            Assert.That(parameterName, Is.EqualTo("source"));
        }

        [Test]
        public void RandomElement_PredicateIsNull_ThrowsException()
        {
            string parameterName = Assert.Throws<ArgumentNullException>(() => Enumerable.Empty<int>().RandomElement(null)).ParamName;
            Assert.That(parameterName, Is.EqualTo("predicate"));
        }

        [Test]
        public void RandomElement_SourceIsEmpty_ReturnsNone()
        {
            Assert.That(Enumerable.Empty<int>().RandomElement(o => true).HasValue, Is.False);
        }

        [Test]
        public void RandomElement__SourceHasOneElement_And_ElementSatisfiesPredicate__ReturnsThatElement()
        {
            var source = new[] { 1 };
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(source.RandomElement(o => true).Value, Is.EqualTo(source[0]));
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void RRandomElement__SourceHasMoreThanOneElement_And_AllElementsSatisfyPredicate__ReturnsAnElementFromTheSource()
        {
            var source = new[] { 1, 2, 3 };
            // ReSharper disable PossibleInvalidOperationException
            CollectionAssert.Contains(source, source.RandomElement(o => true).Value);
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void RandomElement__SourceHasMoreThanOneElement_And_SomeElementsSatisfyPredicate__ReturnsAnElementFromTheSourceThatSatisfyPredicate()
        {
            var source = new[] { 1, 2, 3 };
            var elementsThatSatisfyPredicate = new[] { source[0], source[2] };
            // ReSharper disable PossibleInvalidOperationException
            CollectionAssert.Contains(elementsThatSatisfyPredicate, source.RandomElement(elementsThatSatisfyPredicate.Contains).Value);
            // ReSharper restore PossibleInvalidOperationException
        }

        [Test]
        public void RandomElement__SourceHasOneElement_And_ElementDoesNotSatisfyPredicate__ReturnsNone()
        {
            var source = new[] { 1 };
            Assert.That(source.RandomElement(o => false).HasValue, Is.False);
        }

        [Test]
        public void RandomElement__SourceHasMoreThanOneElement_And_AllElementsDoNotSatisfyPredicate__ReturnsNone()
        {
            var source = new[] { 1, 2, 3 };
            Assert.That(source.RandomElement(o => false).HasValue, Is.False);
        }

        [Test]
        public void RandomElement_ReturnsRandomElement()
        {
            AssertThatRandomElementReturnsRandomElement(CreateLargeCollectionOfDifferentValueObjects());
        }

        public static IEnumerable<int> CreateLargeCollectionOfDifferentValueObjects()
        {
            return Enumerable.Range(1, 1000);
        }

        [Test]
        public void RandomElement_IsThreadSafe() // TODO-IG: This test is currently meaningless. Rewrite it once when the Conscen is implemented.
        {
            // Let's run the previous test on 100 concurrent threads.
            for (int i = 0; i < 100; i++)
                new Thread(RandomElement_ReturnsRandomElement).Start();
        }

        /// <remarks>
        /// Strictly seen, this test is not deterministic and it could fail even if the <see cref="CollectionExtensions.RandomElement{T}(IEnumerable{T}, Predicate{T})"/> method works properly.
        /// The method does not guarantee that the next random value obtained from the same collection will be different than the previous one.
        /// However, for "large enough" number of elements that are all different, we expect this to always be the case.
        /// </remarks>
        private static void AssertThatRandomElementReturnsRandomElement(IEnumerable<int> enumerable)
        {
            var previousRandom = enumerable.RandomElement(o => true);
            for (int i = 0; i < 20; i++) // Let's get 20 subsequent random elements and ensure that each is different then the previous one.
            {
                var actualRandom = enumerable.RandomElement(o => true);
                Assert.That(actualRandom, Is.Not.EqualTo(previousRandom));
            }
        }
        #endregion
    }
    // ReSharper restore InconsistentNaming
}
