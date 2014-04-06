using System;
using NUnit.Framework;
using SwissKnife.Diagnostics.Contracts;

namespace SwissKnife.Tests.Unit.Diagnostics.Contracts
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class TypeParameterTests
    {
        #region IsEnum
        private enum EnumWithoutFlags { }

        [Flags]
        private enum EnumWithFlags { }

        [Test]
        public void IsEnum_TypeParameterIsEnumWithoutFlags_DoesNothing()
        {
            TypeParameter.IsEnum(typeof(EnumWithoutFlags), Option<string>.None);
        }

        [Test]
        public void IsEnum_TypeParameterIsEnumWithFlags_DoesNothing()
        {
            TypeParameter.IsEnum(typeof(EnumWithFlags), Option<string>.None);
        }

        [Test]
        public void IsEnum__TypeParameterIsNull_TypeParameterNameIsNone__ThrowsExceptionWithNullParameterName()
        {
            var parameterName = Assert.Throws<ArgumentNullException>(() => TypeParameter.IsEnum(null, Option<string>.None)).ParamName;
            Assert.That(parameterName, Is.EqualTo(null));
        }

        [Test]
        public void IsEnum__TypeParameterIsNull_TypeParameterNameIsEmptyString__ThrowsExceptionWithEmptyParameterName()
        {
            var parameterName = Assert.Throws<ArgumentNullException>(() => TypeParameter.IsEnum(null, string.Empty)).ParamName;
            Assert.That(parameterName, Is.EqualTo(string.Empty));
        }

        [Test]
        public void IsEnum__TypeParameterIsNull_TypeParameterNameIsSomeString__ThrowsExceptionWithThatStringAsParameterName()
        {
            var parameterName = Assert.Throws<ArgumentNullException>(() => TypeParameter.IsEnum(null, "someParameterName")).ParamName;
            Assert.That(parameterName, Is.EqualTo("someParameterName"));
        }

        [Test]
        public void IsEnum_TypeParameterIsNotEnumReferenceType_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => TypeParameter.IsEnum(typeof(object), Option<string>.None));
            Console.WriteLine(exception.Message);
            Assert.That(exception.Message.Contains("does not represent an enumeration"));
        }

        [Test]
        public void IsEnum_TypeParameterIsNotEnumValueType_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => TypeParameter.IsEnum(typeof(int), Option<string>.None));
            Console.WriteLine(exception.Message);
            Assert.That(exception.Message.Contains("does not represent an enumeration"));
        }

        [Test]
        public void Is_ParameterNameIsNone_ExceptionParameterNameIsNull()
        {
            string parameterName = Assert.Throws<ArgumentException>(() => TypeParameter.IsEnum(typeof(object), Option<string>.None)).ParamName;
            Assert.That(parameterName, Is.Null);
        }

        [Test]
        public void Is_ParameterNameIsSome_ExceptionParameterNameHasTheSameName()
        {
            const string typeParameterName = "TInput";
            string exceptionParameterName = Assert.Throws<ArgumentException>(() => TypeParameter.IsEnum(typeof(object), Option<string>.From(typeParameterName))).ParamName;
            Assert.That(exceptionParameterName, Is.EqualTo(typeParameterName));
        }
        #endregion
    }
    // ReSharper restore InconsistentNaming
}
