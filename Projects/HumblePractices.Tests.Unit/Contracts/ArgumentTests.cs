using System;
using HumblePractices.Contracts;
using HumblePractices.Idioms;
using NUnit.Framework;

namespace HumblePractices.Tests.Unit.Contracts
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
    }
    // ReSharper restore InconsistentNaming
}
