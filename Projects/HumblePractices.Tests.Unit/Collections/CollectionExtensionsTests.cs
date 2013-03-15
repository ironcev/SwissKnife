using System;
using System.Collections.Generic;
using System.Linq;
using HumblePractices.Collections;
using NUnit.Framework;

namespace HumblePractices.Tests.Unit.Collections
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class CollectionExtensionsTests
    {
        #region ForEach<T>
        [Test]
        public void ForEach_EnumerableIsNull_ThrowsException()
        {
            string parameterName = Assert.Throws<ArgumentNullException>(() => CollectionExtensions.ForEach<object>(null, o => { })).ParamName;
            Assert.That(parameterName, Is.EqualTo("enumerable"));
        }

        [Test]
        public void ForEach_ActionIsNull_ThrowsException()
        {
            string parameterName = Assert.Throws<ArgumentNullException>(() => new object[0].ForEach(null)).ParamName;
            Assert.That(parameterName, Is.EqualTo("action"));
        }

        [Test]
        public void ForEach_EnumerableIsEmpty_DoesNotCallTheAction()
        {
            int callCounter = 0;
            new object[0].ForEach(x => callCounter++);
            Assert.That(callCounter, Is.EqualTo(0));
        }

        [Test]
        public void ForEach_EnumerableIsNotEmpty_ActionIsCalledForEveryElementInTheEnumerable()
        {
            int callCounter = 0;
            List<object> invokedObjects = new List<object>();
            var enumeration = new[] {new object(), new object()};
            enumeration.ForEach(x => { invokedObjects.Add(x); callCounter++; });
            Assert.That(callCounter, Is.EqualTo(enumeration.Count()));
            CollectionAssert.AreEqual(enumeration, invokedObjects);
        }
        #endregion

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
    }
    // ReSharper restore InconsistentNaming
}
