using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SwissKnife.Tests.Unit
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class TypeExtensionsTests
    {
        #region IsStatic
        [Test]
        public void IsStatic_TypeIsNull_ThrowsException()
        {
            string parameterName = Assert.Throws<ArgumentNullException>(() => TypeExtensions.IsStatic(null)).ParamName;
            Assert.That(parameterName, Is.EqualTo("type"));
        }

        [Test]
        public void IsStatic_StaticClass_ReturnsTrue()
        {
            Assert.That(typeof(StaticClass).IsStatic(), Is.True);
        }

        [Test]
        public void IsStatic_NonStaticClass_ReturnsFalse()
        {
            Assert.That(typeof(NonStaticClass).IsStatic(), Is.False);
        }

        [Test]
        public void IsStatic_AbstractClass_ReturnsFalse()
        {
            Assert.That(typeof(AbstractClass).IsStatic(), Is.False);
        }

        [Test]
        public void IsStatic_Interface_ReturnsFalse()
        {
            Assert.That(typeof(IInterface).IsStatic(), Is.False);
        }

        [Test]
        public void IsStatic_Struct_ReturnsFalse()
        {
            Assert.That(typeof(Struct).IsStatic(), Is.False);
        }

        [Test]
        public void IsStatic_ArrayOfNonStaticClass_ReturnsFalse()
        {
            Assert.That(typeof(NonStaticClass[]).IsStatic(), Is.False);
        }

        [Test]
        public void IsStatic_ArrayOfAbstractClass_ReturnsFalse()
        {
            Assert.That(typeof(AbstractClass[]).IsStatic(), Is.False);
        }

        [Test]
        public void IsStatic_ArrayOfInterface_ReturnsFalse()
        {
            Assert.That(typeof(IInterface[]).IsStatic(), Is.False);
        }

        [Test]
        public void IsStatic_ArrayOfStruct_ReturnsFalse()
        {
            Assert.That(typeof(Struct[]).IsStatic(), Is.False);
        }

        private static class StaticClass {}
        private class NonStaticClass {}
        private abstract class AbstractClass {}
        private interface IInterface {}
        private struct Struct {}
        #endregion

        #region IsBitField
        private enum NonBitFieldEnum { }
        [Flags]
        private enum BitFieldEnum { }

        [Test]
        public void IsBiField_TypeIsNull_ThrowsException()
        {
            string parameterName = Assert.Throws<ArgumentNullException>(() => TypeExtensions.IsBitField(null)).ParamName;
            Assert.That(parameterName, Is.EqualTo("type"));
        }

        [Test]
        public void IsBiField_TypeIsNotEnum_ReturnsFalse()
        {
            Assert.That(typeof(object).IsBitField(), Is.False);
        }

        [Test]
        public void IsBiField_TypeIsGenericType_ReturnsFalse()
        {
            Assert.That(typeof(List<object>).IsBitField(), Is.False);
        }

        [Test]
        public void IsBiField_TypeIsOpenGenericType_ReturnsFalse()
        {
            Assert.That(typeof(List<>).IsBitField(), Is.False);
        }

        [Test]
        public void IsBiField_NonBitFieldEnum_ReturnsFalse()
        {
            Assert.That(typeof(NonBitFieldEnum).IsBitField(), Is.False);
        }

        [Test]
        public void IsBiField_BitFieldEnum_ReturnsTrue()
        {
            Assert.That(typeof(BitFieldEnum).IsBitField(), Is.True);
        }
        #endregion
    }
    // ReSharper restore InconsistentNaming
}
