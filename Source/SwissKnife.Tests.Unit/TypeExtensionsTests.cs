using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
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

        #region IsFlagsEnum
        private enum NonFlagsEnum { }
        [Flags]
        private enum FlagsEnum { }

        [Test]
        public void IsFlagsEnum_TypeIsNull_ThrowsException()
        {
            string parameterName = Assert.Throws<ArgumentNullException>(() => TypeExtensions.IsFlagsEnum(null)).ParamName;
            Assert.That(parameterName, Is.EqualTo("type"));
        }

        [Test]
        public void IsFlagsEnum_TypeIsNotEnum_ReturnsFalse()
        {
            Assert.That(typeof(object).IsFlagsEnum(), Is.False);
        }

        [Test]
        public void IsFlagsEnum_TypeIsGenericType_ReturnsFalse()
        {
            Assert.That(typeof(List<object>).IsFlagsEnum(), Is.False);
        }

        [Test]
        public void IsFlagsEnum_TypeIsOpenGenericType_ReturnsFalse()
        {
            Assert.That(typeof(List<>).IsFlagsEnum(), Is.False);
        }

        [Test]
        public void IsFlagsEnum_NonFlagsEnum_ReturnsFalse()
        {
            Assert.That(typeof(NonFlagsEnum).IsFlagsEnum(), Is.False);
        }

        [Test]
        public void IsFlagsEnum_FlagsEnum_ReturnsTrue()
        {
            Assert.That(typeof(FlagsEnum).IsFlagsEnum(), Is.True);
        }

        [Test]
        public void IsFlagsEnum_EmitedNonFlagsEnum_ReturnsFalse()
        {
            Type enumType = EmitEnum(new[] { "Zero", "One", "Two" }, new[] { 0, 1, 2 }, false);
            Assert.That(enumType.IsFlagsEnum(), Is.False);
        }

        [Test]
        public void IsFlagsEnum_EmitedNonFlagsEnumWithNonIntegerUnderlyaingType_ReturnsFalse()
        {
            Type enumType = EmitEnum(new[] { "Zero", "One", "OnePointOne", "Two", "TwoPointTwo" }, new[] { 0d, 1d, 1.1d, 2d, 2.2d }, false);
            Assert.That(enumType.IsFlagsEnum(), Is.False);
        }

        [Test]
        public void IsFlagsEnum_EmitedFlagsEnum_ReturnsTrue()
        {
            Type enumType = EmitEnum(new[] { "None", "One", "Two", "All" }, new[] { 0, 1, 2, 3 }, true);
            Assert.That(enumType.IsFlagsEnum(), Is.True);
        }

        [Test]
        public void IsFlagsEnum_EmitedFlagsEnumWithNonIntegerUnderlyaingType_ReturnsFalse()
        {
            Type enumType = EmitEnum(new[] { "Zero", "One", "OnePointOne", "Two", "TwoPointTwo" }, new[] { 0d, 1d, 1.1d, 2d, 2.2d }, true);
            Assert.That(enumType.IsFlagsEnum(), Is.False);
        }
        #endregion

        #region UnderlyingTypesOfFlagsEnums
        [Test]
        public void UnderlyingTypesOfFlagsEnums_IsProperlyDefined()
        {
            CollectionAssert.AreEquivalent(
                TypeExtensions.UnderlyingTypesOfFlagsEnums,
                new[]
                {
                    typeof(byte),
                    typeof(sbyte),
                    typeof(short),
                    typeof(ushort),
                    typeof(int),
                    typeof(uint),
                    typeof(long),
                    typeof(ulong)
                }
            );
        }
        #endregion

        #region Helper Methods
        internal static Type EmitEnum<TUnderlyingType>(string[] literals, TUnderlyingType[] values, bool isFlagsEnum)
        {
            AssemblyName assemblyName = new AssemblyName("EnumExperimentAssembly_" + typeof(TUnderlyingType).Name);
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);

            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);
            EnumBuilder enumBuilder = moduleBuilder.DefineEnum("EnumWithUnderlyingType" + typeof(TUnderlyingType).Name, TypeAttributes.Public, typeof(TUnderlyingType));

            for (int i = 0; i < literals.Length; i++)
                enumBuilder.DefineLiteral(literals[i], values[i]);

            if (isFlagsEnum)
            {
                // ReSharper disable AssignNullToNotNullAttribute
                ConstructorInfo flagsAttributesConstructorInfo = typeof(FlagsAttribute).GetConstructor(new Type[0]);
                CustomAttributeBuilder customAttributeBuilder = new CustomAttributeBuilder(flagsAttributesConstructorInfo, new object[0]);
                // ReSharper restore AssignNullToNotNullAttribute
                enumBuilder.SetCustomAttribute(customAttributeBuilder);
            }

            Type enumType = enumBuilder.CreateType();

            Assert.That(Enum.GetUnderlyingType(enumType), Is.EqualTo(typeof(TUnderlyingType)));

            if (isFlagsEnum)
                Assert.That(enumType.GetCustomAttributes(typeof(FlagsAttribute), false).Length, Is.GreaterThanOrEqualTo(1));

            return enumType;
        }
        #endregion
    }
    // ReSharper restore InconsistentNaming
}
