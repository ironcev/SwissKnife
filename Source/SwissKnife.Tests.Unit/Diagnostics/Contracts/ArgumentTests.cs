using System;
using NUnit.Framework;
using SwissKnife.Diagnostics.Contracts;

namespace SwissKnife.Tests.Unit.Diagnostics.Contracts
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class ArgumentTests
    {
        #region IsNotNullOrWhitespace
        [Test]
        public void IsNotNullOrWhitespace_ParameterValueIsNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Argument.IsNotNullOrWhitespace(null, null));
        }

        [Test]
        public void IsNotNullOrWhitespace_ParameterValueIsEmpty_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Argument.IsNotNullOrWhitespace(string.Empty, null));
        }

        [Test]
        public void IsNotNullOrWhitespace_ParameterValueIsWhitespace_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Argument.IsNotNullOrWhitespace(" ", null));
        }

        [Test]
        public void IsNotNullOrWhitespace_ParameterValueIsValid_DoesNothing()
        {
            Argument.IsNotNullOrWhitespace("a", null);
        }

        [Test]
        public void IsNotNullOrWhitespace_ParameterNameIsNone_ExceptionParameterNameIsNull()
        {
            string parameterName = Assert.Throws<ArgumentNullException>(() => Argument.IsNotNullOrWhitespace(null, Option<string>.None)).ParamName;
            Assert.That(parameterName, Is.Null);
        }

        [Test]
        public void IsNotNullOrWhitespace_ParameterNameIsSome_ExceptionParameterNameHasTheSameName()
        {
            const string parameterName = "parameterName";
            string exceptionParameterName = Assert.Throws<ArgumentException>(() => Argument.IsNotNullOrWhitespace(" ", Option<string>.Some(parameterName))).ParamName;
            Assert.That(exceptionParameterName, Is.EqualTo(parameterName));
        }
        #endregion

        #region IsNotNullOrEmpty
        [Test]
        public void IsNotNullOrEmpty_ParameterValueIsNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Argument.IsNotNullOrEmpty(null, null));
        }

        [Test]
        public void IsNotNullOrEmpty_ParameterValueIsEmpty_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Argument.IsNotNullOrEmpty(string.Empty, null));
        }

        [Test]
        public void IsNotNullOrEmpty_ParameterValueIsWhitespace_DoesNothing()
        {
            Argument.IsNotNullOrEmpty(" ", null);
        }

        [Test]
        public void IsNotNullOrEmpty_ParameterValueIsValid_DoesNothing()
        {
            Argument.IsNotNullOrEmpty("a", null);
        }

        [Test]
        public void IsNotNullOrEmpty_ParameterNameIsNone_ExceptionParameterNameIsNull()
        {
            string parameterName = Assert.Throws<ArgumentNullException>(() => Argument.IsNotNullOrEmpty(null, Option<string>.None)).ParamName;
            Assert.That(parameterName, Is.Null);
        }

        [Test]
        public void IsNotNullOrEmpty_ParameterNameIsSome_ExceptionParameterNameHasTheSameName()
        {
            const string parameterName = "parameterName";
            string exceptionParameterName = Assert.Throws<ArgumentNullException>(() => Argument.IsNotNullOrEmpty(null, Option<string>.Some(parameterName))).ParamName;
            Assert.That(exceptionParameterName, Is.EqualTo(parameterName));
        }
        #endregion

        #region IsNotNull
        [Test]
        public void IsNotNull_ParameterValueIsNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Argument.IsNotNull(null, null));
        }

        [Test]
        public void IsNotNull_ParameterValueIsValid_DoesNothing()
        {
            Argument.IsNotNull(new object(), null);
        }

        [Test]
        public void IsNotNull_ParameterNameIsNone_ExceptionParameterNameIsNull()
        {
            string parameterName = Assert.Throws<ArgumentNullException>(() => Argument.IsNotNull(null, Option<string>.None)).ParamName;
            Assert.That(parameterName, Is.Null);
        }

        [Test]
        public void IsNotNull_ParameterNameIsSome_ExceptionParameterNameHasTheSameName()
        {
            const string parameterName = "parameterName";
            string exceptionParameterName = Assert.Throws<ArgumentNullException>(() => Argument.IsNotNull(null, Option<string>.Some(parameterName))).ParamName;
            Assert.That(exceptionParameterName, Is.EqualTo(parameterName));
        }
        #endregion

        #region IsValid
        [Test]
        public void IsValid_ValidityConditionIsFalse_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Argument.IsValid(false, null, null));
        }

        [Test]
        public void IsValid_ValidityConditionIsTrue_DoesNothing()
        {
            Argument.IsValid(true, null, null);
        }

        [Test]
        public void IsValid_ExceptionMessageIsSome_ExceptionMessageIsTheSameMessage()
        {
            const string exceptionMessage = "exceptionMessage";
            string exceptionExceptionMessage = Assert.Throws<ArgumentException>(() => Argument.IsValid(false, Option<string>.Some(exceptionMessage), null)).Message;
            Assert.That(exceptionExceptionMessage, Is.EqualTo(exceptionMessage));
        }

        [Test]
        public void IsValid_ParameterNameIsNone_ExceptionParameterNameIsNull()
        {
            string parameterName = Assert.Throws<ArgumentException>(() => Argument.IsValid(false, null, Option<string>.None)).ParamName;
            Assert.That(parameterName, Is.Null);
        }

        [Test]
        public void IsValid_ParameterNameIsSome_ExceptionParameterNameHasTheSameName()
        {
            const string parameterName = "parameterName";
            string exceptionParameterName = Assert.Throws<ArgumentException>(() => Argument.IsValid(false, null, Option<string>.Some(parameterName))).ParamName;
            Assert.That(exceptionParameterName, Is.EqualTo(parameterName));
        }
        #endregion

        #region Is
        [Test]
        public void Is_ParameterValueIsNull_DoesNothing()
        {
            Argument.Is(null, typeof(object), Option<string>.None);
        }

        [Test]
        public void Is_ParameterValueIsExactlyOfType_DoesNothing()
        {
            Argument.Is(new object(), typeof(object), Option<string>.None);
        }

        /// <remarks>
        /// Covers issue #1.
        /// </remarks>
        [Test]
        public void Is_ParameterValueIsExactlyOfReferenceType_DoesNothing()
        {
          Argument.Is(new TestClass(), typeof(TestClass), Option<string>.None);
        }
        private class TestClass {}

        [Test]
        public void Is_ParameterValueIsExactlyOfValueType_DoesNothing()
        {
          Argument.Is(new TestStruct(), typeof(TestStruct), Option<string>.None);
        }
        private struct TestStruct {}

        [Test]
        public void Is_ParameterValueIsInterfaceOfType_DoesNothing()
        {
          ITestInterface baseTestObject = new BaseTestClass();
          Argument.Is((object)baseTestObject, typeof(ITestInterface), Option<string>.None);
          Argument.Is((object)baseTestObject, typeof(BaseTestClass), Option<string>.None);

          ITestInterface derivedTestObject = new DerivedTestClass();
          Argument.Is((object)derivedTestObject, typeof(ITestInterface), Option<string>.None);
          Argument.Is((object)derivedTestObject, typeof(BaseTestClass), Option<string>.None);
        }
        private interface ITestInterface {}
        private class BaseTestClass : ITestInterface {}
        private class DerivedTestClass : BaseTestClass { }

        [Test]
        public void Is_ParameterValueIsOfDerivedType_DoesNothing()
        {
            Argument.Is(new int(), typeof(object), Option<string>.None);
        }

        [Test]
        public void Is_ParameterValueIsNotOfType_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Argument.Is(new object(), typeof(int), Option<string>.None));
        }

        [Test]
        public void Is_TypeIsNull_ThrowsException()
        {
            var parameterName = Assert.Throws<ArgumentNullException>(() => Argument.Is(new object(), null, Option<string>.None)).ParamName;
            Assert.That(parameterName, Is.EqualTo("type"));
        }

        [Test]
        public void Is_ParameterNameIsNone_ExceptionParameterNameIsNull()
        {
            string parameterName = Assert.Throws<ArgumentException>(() => Argument.Is(new object(), typeof(int),  Option<string>.None)).ParamName;
            Assert.That(parameterName, Is.Null);
        }

        [Test]
        public void Is_ParameterNameIsSome_ExceptionParameterNameHasTheSameName()
        {
            const string parameterName = "parameterName";
            string exceptionParameterName = Assert.Throws<ArgumentException>(() => Argument.Is(new object(), typeof(int), Option<string>.Some(parameterName))).ParamName;
            Assert.That(exceptionParameterName, Is.EqualTo(parameterName));
        }
        #endregion

        #region IsInRange
        [Test]
        public void IsInRange_ParameterValueIsInRange_DoesNothing()
        {
            Argument.IsInRange(1, 0, 2, Option<string>.None);
        }

        [Test]
        public void IsInRange_ParameterValueIsLowerBound_DoesNothing()
        {
            Argument.IsInRange(0, 0, 2, Option<string>.None);
        }

        [Test]
        public void IsInRange_ParameterValueIsUpperBound_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Argument.IsInRange(2, 0, 2, Option<string>.None));
        }

        [Test]
        public void IsInRange_ParameterValueIsGreaterThanUpperBound_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Argument.IsInRange(3, 0, 2, Option<string>.None));
        }

        [Test]
        public void IsInRange_ParameterValueIsLowerThanLowerBound_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Argument.IsInRange(-1, 0, 2, Option<string>.None));
        }
        [Test]
        public void IsInRange_ParameterNameIsNone_ExceptionParameterNameIsNull()
        {
            string parameterName = Assert.Throws<ArgumentOutOfRangeException>(() => Argument.IsInRange(0, 0, 0, Option<string>.None)).ParamName;
            Assert.That(parameterName, Is.Null);
        }

        [Test]
        public void IsInRange_ParameterNameIsSome_ExceptionParameterNameHasTheSameName()
        {
            const string parameterName = "parameterName";
            string exceptionParameterName = Assert.Throws<ArgumentOutOfRangeException>(() => Argument.IsInRange(0, 0, 0, Option<string>.Some(parameterName))).ParamName;
            Assert.That(exceptionParameterName, Is.EqualTo(parameterName));
        }
        #endregion

        #region IsGreaterThanZero
        [Test]
        public void IsGreaterThanZero_ParameterValueIsGreaterThanZero_DoesNothing()
        {
            Argument.IsGreaterThanZero(1, Option<string>.None);
        }

        [Test]
        public void IsGreaterThanZero_ParameterValueIsZero_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Argument.IsGreaterThanZero(0, Option<string>.None));
        }

        [Test]
        public void IsGreaterThanZero_ParameterValueIsLowerThanZero_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Argument.IsGreaterThanZero(-1, Option<string>.None));
        }

        [Test]
        public void IsGreaterThanZero_ParameterNameIsNone_ExceptionParameterNameIsNull()
        {
            string parameterName = Assert.Throws<ArgumentOutOfRangeException>(() => Argument.IsGreaterThanZero(0, Option<string>.None)).ParamName;
            Assert.That(parameterName, Is.Null);
        }

        [Test]
        public void IsGreaterThanZero_ParameterNameIsSome_ExceptionParameterNameHasTheSameName()
        {
            const string parameterName = "parameterName";
            string exceptionParameterName = Assert.Throws<ArgumentOutOfRangeException>(() => Argument.IsGreaterThanZero(0, Option<string>.Some(parameterName))).ParamName;
            Assert.That(exceptionParameterName, Is.EqualTo(parameterName));
        }
        #endregion
    }
    // ReSharper restore InconsistentNaming
}
