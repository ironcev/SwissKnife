using System;
using System.Collections.Generic;
using NUnit.Framework;
using SwissKnife.IdentifierConversion;

namespace SwissKnife.Tests.Unit
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class IdentifierTests
    {
        private int TestProperty { get; set; }
        private static int StaticTestProperty { get; set; }
        private TestClass TestClassProperty { get; set; }

        [Test]
        public void AsStringOfT_ExpressionIsOnlyLambdaInputParameter_ReturnsEmptyString()
        {
            Assert.That(Identifier.ToString<TestClass>(x => x), Is.EqualTo(string.Empty));
        }

        [Test]
        public void AsStringOfT_ExpressionIsValueTypePropertyAccess_ReturnsPropertyName()
        {
            Assert.That(Identifier.ToString<TestClass>(x => x.ValueTypeProperty), Is.EqualTo("ValueTypeProperty"));
        }

        [Test]
        public void AsStringOfT_ExpressionIsCascadedValueTypePropertyAccess_ReturnsPropertyNamesSeparatedByDefaultSeparator()
        {
            Assert.That(Identifier.ToString<TestClass>(x => x.TestClassProperty.ValueTypeProperty), Is.EqualTo("TestClassProperty.ValueTypeProperty"));
        }

        // TODO-IG: Put this test to a proper place. It is here just temporary because of the need on internal projects.
        [Test]
        public void AsString_1()
        {
            Assert.That(Identifier.ToString(() => string.Empty), Is.EqualTo("Empty"));
        }

        [Test]
        public void AsString_2()
        {
            Assert.That(Identifier.ToString(() => TestProperty), Is.EqualTo("TestProperty"));
        }

        [Test]
        public void AsString_3()
        {
            Assert.That(Identifier.ToString(() => StaticTestProperty), Is.EqualTo("StaticTestProperty"));
        }

        [Test]
        public void AsString_3a()
        {
            var identifierOptions = new ConversionOptions
            {
                StaticMemberConversion = StaticMemberConversion.ParentTypeName
            };
            Assert.That(Identifier.ToString(() => StaticTestProperty, identifierOptions), Is.EqualTo(string.Format("{0}.StaticTestProperty", typeof(IdentifierTests).Name)));
        }

        [Test]
        public void AsString_3b()
        {
            var identifierOptions = new ConversionOptions
            {
                StaticMemberConversion = StaticMemberConversion.ParentTypeFullName
            };
            Assert.That(Identifier.ToString(() => StaticTestProperty, identifierOptions), Is.EqualTo(string.Format("{0}.StaticTestProperty", typeof(IdentifierTests).FullName)));
        }

        [Test]
        public void AsString_4()
        {
            Assert.That(Identifier.ToString(() => TestClass.StaticProperty), Is.EqualTo("StaticProperty"));
        }

        [Test]
        public void AsString_4a()
        {
            var identifierOptions = new ConversionOptions
            {
                StaticMemberConversion = StaticMemberConversion.ParentTypeName
            };
            Assert.That(Identifier.ToString(() => TestClass.StaticProperty, identifierOptions), Is.EqualTo(string.Format("{0}.StaticProperty", typeof(TestClass).Name)));
        }

        [Test]
        public void AsString_4b()
        {
            var identifierOptions = new ConversionOptions
            {
                StaticMemberConversion = StaticMemberConversion.ParentTypeFullName
            };
            Assert.That(Identifier.ToString(() => TestClass.StaticProperty, identifierOptions), Is.EqualTo(string.Format("{0}.StaticProperty", typeof(TestClass).FullName)));
        }

        [Test]
        public void AsString_5()
        {
            Assert.That(Identifier.ToString(() => TestClassProperty.StringProperty), Is.EqualTo("TestClassProperty.StringProperty"));
        }

        // TODO-IG: Rename all test methods below this line.

        [Test]
        public void AsStringOfT_WithSingleMemberAccess_ReturnsMemberName()
        {
            Assert.That(Identifier.ToString<TestClass>(x => x.StringProperty), Is.EqualTo("StringProperty"));
        }

        [Test]
        public void AsStringOfT_WithCascadedMemberAccess_ReturnsDotSeparatedMemberNames()
        {
            Assert.That(Identifier.ToString<TestClass>(x => x.TestClassProperty.StringProperty), Is.EqualTo("TestClassProperty.StringProperty"));
        }

        [Test]
        public void AsStringOfT_WithArrayIndexer_ReturnsIndexAccess()
        {
            Assert.That(Identifier.ToString<TestClass>(x => x.TestClassArrayProperty[0]), Is.EqualTo("TestClassArrayProperty[0]"));
        }

        [Test]
        public void AsStringOfT_WithValueTypeArrayIndexer_ReturnsIndexAccess()
        {
            Assert.That(Identifier.ToString<TestClass>(x => x.TestValueTypeArrayProperty[0]), Is.EqualTo("TestValueTypeArrayProperty[0]"));
        }

        [Test]
        public void AsStringOfT_WithValueTypeArrayIndexer_WithLargeNumber_ReturnsIndexAccessWithNoDigitGrouping()
        {
            Assert.That(Identifier.ToString<TestClass>(x => x.TestValueTypeArrayProperty[1000000]), Is.EqualTo("TestValueTypeArrayProperty[1000000]"));
        }

        [Test]
        public void AsStringOfT_WithPropertyAtArrayIndex_ReturnsIndexAccess()
        {
            Assert.That(Identifier.ToString<TestClass>(x => x.TestClassArrayProperty[0].StringProperty), Is.EqualTo("TestClassArrayProperty[0].StringProperty"));
        }

        [Test]
        public void AsStringOfT_WithPropertyAtComplexValueTypeArrayIndex_ReturnsIndexAccess()
        {
            Assert.That(Identifier.ToString<TestClass>(x => x.TestComplexValueTypeArrayProperty[0].Property), Is.EqualTo("TestComplexValueTypeArrayProperty[0].Property"));
        }

        [Test]
        public void AsStringOfT_WithPropertyAtComplexValueTypeArrayIndex_WithLargeNumber_ReturnsIndexAccessWithoutDigitGrouping()
        {
            Assert.That(Identifier.ToString<TestClass>(x => x.TestComplexValueTypeArrayProperty[1000000].Property), Is.EqualTo("TestComplexValueTypeArrayProperty[1000000].Property"));
        }

        [Test]
        public void AsStringOfT_WithIndexerAtBegin_ReturnsIndexer()
        {
            Assert.That(() => Identifier.ToString<TestClass>(x => x[0]), Is.EqualTo("[0]"));
        }

        [Test]
        public void AsStringOfT_WithIndexer_ReturnsIndexAccess()
        {
            Assert.That(Identifier.ToString<TestClass>(x => x.TestClassListProperty[0]), Is.EqualTo("TestClassListProperty[0]"));
        }

        [Test]
        public void AsStringOfT_WithValueTypeIndexer_ReturnsIndexAccess()
        {
            Assert.That(Identifier.ToString<TestClass>(x => x.TestValueTypeListProperty[0]), Is.EqualTo("TestValueTypeListProperty[0]"));
        }

        [Test]
        public void AsStringOfT_WithPropertyAtIndexerIndex_ReturnsIndexAccess()
        {
            Assert.That(Identifier.ToString<TestClass>(x => x.TestClassListProperty[0].StringProperty), Is.EqualTo("TestClassListProperty[0].StringProperty"));
        }

        [Test]
        public void AsStringOfT_WithSimpleDynamicallyResolvedIndexerIndex_ReturnsIndexAccess()
        {
            var index = GetRandomNumber();
            Assert.That(Identifier.ToString<TestClass>(x => x.TestClassListProperty[index]), Is.EqualTo(String.Format("TestClassListProperty[{0}]", index)));
        }

        [Test]
        public void AsStringOfT_WithSimpleDynamicallyResolvedArrayIndex_ReturnsIndexAccess()
        {
            var index = GetRandomNumber();
            Assert.That(Identifier.ToString<TestClass>(x => x.TestClassArrayProperty[index]), Is.EqualTo(String.Format("TestClassArrayProperty[{0}]", index)));
        }

        [Test]
        public void AsStringOfT_WithSimpleDynamicallyResolvedValueTypeArrayIndex_ReturnsIndexAccess()
        {
            var index = GetRandomNumber();
            Assert.That(Identifier.ToString<TestClass>(x => x.TestValueTypeArrayProperty[index]), Is.EqualTo(String.Format("TestValueTypeArrayProperty[{0}]", index)));
        }

        private static int GetRandomNumber()
        {
            var random = new Random();
            return random.Next();
        }

        // ReSharper disable UnusedAutoPropertyAccessor.Local
        // ReSharper disable ClassNeverInstantiated.Local
        // ReSharper disable UnusedParameter.Local
        private class TestClass
        {
            public static int StaticProperty { get; set; }

            public int ValueTypeProperty { get; set; }
            public string StringProperty { get; set; }
            public TestClass TestClassProperty { get; set; }
            public TestClass[] TestClassArrayProperty { get; set; }
            public List<TestClass> TestClassListProperty { get; set; }
            public int[] TestValueTypeArrayProperty { get; set; }
            public List<int> TestValueTypeListProperty { get; set; }
            public ComplexValueType[] TestComplexValueTypeArrayProperty { get; set; }

            public string this[int index]
            {
                get { return String.Empty; }
            }
        }

        private struct ComplexValueType
        {
            public int Property { get; set; }
        }
        // ReSharper restore UnusedParameter.Local
        // ReSharper restore ClassNeverInstantiated.Local
        // ReSharper restore UnusedAutoPropertyAccessor.Local
    }
    // ReSharper restore InconsistentNaming
}
