using System;
using HumblePractices.Contracts;
using HumblePractices.Idioms;
using NUnit.Framework;

namespace HumblePractices.Tests.Unit.Contracts
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class OperationTests
    {
        #region IsValid
        [Test]
        public void IsValid_ValidityConditionIsFalse_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => Operation.IsValid(false, null));
        }

        [Test]
        public void IsValid_ValidityConditionIsTrue_DoesNothing()
        {
            Operation.IsValid(true, null);
        }

        [Test]
        public void IsValid_ExceptionMessageIsSome_ExceptionMessageIsTheSameMessage()
        {
            const string exceptionMessage = "exceptionMessage";
            string exceptionExceptionMessage = Assert.Throws<InvalidOperationException>(() => Operation.IsValid(false, Option<string>.Some(exceptionMessage))).Message;
            Assert.That(exceptionExceptionMessage, Is.EqualTo(exceptionMessage));
        }
        #endregion
    }
    // ReSharper restore InconsistentNaming
}
