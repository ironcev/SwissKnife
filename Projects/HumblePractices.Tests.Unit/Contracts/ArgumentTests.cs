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
        public void IsNotNullOrWhitespace_ParameterNameIsNone_ExceptionParameterNameIsNull()
        {
            string parameterName = Assert.Throws<ArgumentException>(() => Argument.IsNotNullOrWhitespace(" ", Option<string>.None)).ParamName;
            Assert.That(parameterName, Is.Null);
        }

        [Test]
        public void IsNotNullOrWhitespace_ParameterNameIsSome_ExceptionParameterNameHasTheSameName()
        {
            const string parameterName = "parameterName";
            string exceptionParameterName = Assert.Throws<ArgumentException>(() => Argument.IsNotNullOrWhitespace(" ", Option<string>.Some(parameterName))).ParamName;
            Assert.That(exceptionParameterName, Is.EqualTo(parameterName));
        }
    }
    // ReSharper restore InconsistentNaming
}
