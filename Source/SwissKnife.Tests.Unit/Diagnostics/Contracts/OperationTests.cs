using System;
using NUnit.Framework;
using SwissKnife.Diagnostics.Contracts;
using SwissKnife.Idioms;

namespace SwissKnife.Tests.Unit.Diagnostics.Contracts
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
