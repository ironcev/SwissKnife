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
    }
    // ReSharper restore InconsistentNaming
}
