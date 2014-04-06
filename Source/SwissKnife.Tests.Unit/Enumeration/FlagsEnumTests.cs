using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SwissKnife.Enumeration;

namespace SwissKnife.Tests.Unit.Enumeration
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class FlagsEnumTests
    {        
        #region Experiments - Memory Representations of Enums
        [Flags]
        private enum BitFieldWithNegativeItems
        {
            Zero = 0,
            MinusOne = -1,
            MinusTwo = -2,
        }

        [Flags]
        private enum BitFieldWithPositiveItems
        {
            Zero = 0,
            PlusOne = 1,
            PlusTwo = 2,
        }

        /// <remarks>
        /// I just wanted to get a hands on feeling on how unsigned integers are represented in memory.
        /// .NET uses "two's complement" to represent negative numbers: https://en.wikipedia.org/wiki/Signed_number_representations#Two.27s_complement.
        /// The numbers are represented in little-endian system: https://en.wikipedia.org/wiki/Byte_order.
        /// </remarks>
        [Test]
        [Ignore("Experiment")]
        public void Memory_Representation_Of_Int32()
        {
            PrintVisualizedInt32(0);
            PrintVisualizedInt32(1);
            PrintVisualizedInt32(2);
            PrintVisualizedInt32(3);
            PrintVisualizedInt32(4);
            PrintVisualizedInt32(127);
            PrintVisualizedInt32(255);
            PrintVisualizedInt32(256);

            PrintVisualizedInt32(-256);
            PrintVisualizedInt32(-255);
            PrintVisualizedInt32(-127);
            PrintVisualizedInt32(-4);
            PrintVisualizedInt32(-3);
            PrintVisualizedInt32(-2);
            PrintVisualizedInt32(-1);
            PrintVisualizedInt32(-0);
        }

        /// <remarks>
        /// As expected and foreseen, this doesn't work. Still, it's nice to see it printed out :-)
        /// </remarks>
        [Test]
        [Ignore("Experiment")]
        public void Setting_Flags_On_Negative_Items_Using_Usual_Bit_Operations()
        {
            PrintVisualizedBitFieldWithNegativeItems("Zero", BitFieldWithNegativeItems.Zero);
            PrintBitsAreSetOnNegativeItems(BitFieldWithNegativeItems.Zero, BitFieldWithNegativeItems.Zero);

            PrintVisualizedBitFieldWithNegativeItems("MinusOne", BitFieldWithNegativeItems.MinusOne);
            PrintBitsAreSetOnNegativeItems(BitFieldWithNegativeItems.MinusOne, BitFieldWithNegativeItems.MinusOne);
            PrintBitsAreSetOnNegativeItems(BitFieldWithNegativeItems.MinusOne, BitFieldWithNegativeItems.Zero);

            PrintVisualizedBitFieldWithNegativeItems("MinusTwo", BitFieldWithNegativeItems.MinusTwo);
            PrintBitsAreSetOnNegativeItems(BitFieldWithNegativeItems.MinusTwo, BitFieldWithNegativeItems.MinusTwo);            
            PrintBitsAreSetOnNegativeItems(BitFieldWithNegativeItems.MinusTwo, BitFieldWithNegativeItems.Zero);

            PrintVisualizedBitFieldWithNegativeItems("MinusOne, MinusTwo", BitFieldWithNegativeItems.MinusTwo | BitFieldWithNegativeItems.MinusOne);
            PrintBitsAreSetOnNegativeItems(BitFieldWithNegativeItems.MinusTwo | BitFieldWithNegativeItems.MinusOne, BitFieldWithNegativeItems.Zero);
            PrintBitsAreSetOnNegativeItems(BitFieldWithNegativeItems.MinusTwo | BitFieldWithNegativeItems.MinusOne, BitFieldWithNegativeItems.MinusOne);
            PrintBitsAreSetOnNegativeItems(BitFieldWithNegativeItems.MinusTwo | BitFieldWithNegativeItems.MinusOne, BitFieldWithNegativeItems.MinusTwo);

            const BitFieldWithNegativeItems minusOneAndMinusTwoSet = BitFieldWithNegativeItems.MinusOne | BitFieldWithNegativeItems.MinusTwo;
            Console.WriteLine("MinusOne | MinusTwo");
            PrintVisualizedBitFieldWithNegativeItems("MinusOne, MinusTwo", minusOneAndMinusTwoSet);
            const BitFieldWithNegativeItems removedMinusOneFromMinusOneAndMinusTwoSet = minusOneAndMinusTwoSet & ~BitFieldWithNegativeItems.MinusOne;
            Console.WriteLine("(MinusOne | MinusTwo) & ~MinusOne");
            PrintVisualizedBitFieldWithNegativeItems("MinusTwo", removedMinusOneFromMinusOneAndMinusTwoSet);
        }

        /// <remarks>
        /// Just to be sure that the Earth is still round :-)
        /// </remarks>
        [Test]
        [Ignore("Experiment")]
        public void Setting_Flags_On_Positive_Items_Using_Usual_Bit_Operations()
        {
            PrintVisualizedBitFieldWithPositiveItems("Zero", BitFieldWithPositiveItems.Zero);
            PrintBitsAreSetOnPositiveItems(BitFieldWithPositiveItems.Zero, BitFieldWithPositiveItems.Zero);

            PrintVisualizedBitFieldWithPositiveItems("PlusOne", BitFieldWithPositiveItems.PlusOne);
            PrintBitsAreSetOnPositiveItems(BitFieldWithPositiveItems.PlusOne, BitFieldWithPositiveItems.PlusOne);
            PrintBitsAreSetOnPositiveItems(BitFieldWithPositiveItems.PlusOne, BitFieldWithPositiveItems.Zero);

            PrintVisualizedBitFieldWithPositiveItems("PlusTwo", BitFieldWithPositiveItems.PlusTwo);
            PrintBitsAreSetOnPositiveItems(BitFieldWithPositiveItems.PlusTwo, BitFieldWithPositiveItems.PlusTwo);           
            PrintBitsAreSetOnPositiveItems(BitFieldWithPositiveItems.PlusTwo, BitFieldWithPositiveItems.Zero);

            PrintVisualizedBitFieldWithPositiveItems("PlusOne, PlusTwo", BitFieldWithPositiveItems.PlusTwo | BitFieldWithPositiveItems.PlusOne);
            PrintBitsAreSetOnPositiveItems(BitFieldWithPositiveItems.PlusTwo | BitFieldWithPositiveItems.PlusOne, BitFieldWithPositiveItems.Zero);
            PrintBitsAreSetOnPositiveItems(BitFieldWithPositiveItems.PlusTwo | BitFieldWithPositiveItems.PlusOne, BitFieldWithPositiveItems.PlusOne);
            PrintBitsAreSetOnPositiveItems(BitFieldWithPositiveItems.PlusTwo | BitFieldWithPositiveItems.PlusOne, BitFieldWithPositiveItems.PlusTwo);

            const BitFieldWithPositiveItems PlusOneAndPlusTwoSet = BitFieldWithPositiveItems.PlusOne | BitFieldWithPositiveItems.PlusTwo;
            Console.WriteLine("PlusOne | PlusTwo");
            PrintVisualizedBitFieldWithPositiveItems("PlusOne, PlusTwo", PlusOneAndPlusTwoSet);
            const BitFieldWithPositiveItems removedPlusOneFromPlusOneAndPlusTwoSet = PlusOneAndPlusTwoSet & ~BitFieldWithPositiveItems.PlusOne;
            Console.WriteLine("(PlusOne | PlusTwo) & ~PlusOne");
            PrintVisualizedBitFieldWithPositiveItems("PlusTwo", removedPlusOneFromPlusOneAndPlusTwoSet);
        }

        private static void PrintBitsAreSetOnPositiveItems(BitFieldWithPositiveItems flags, BitFieldWithPositiveItems setFlags)
        {
            Console.WriteLine("{0, 20} in {1, 20} -> {2}", setFlags, flags, (flags & setFlags) == setFlags ? "set" : "not set");
        }

        private static unsafe void PrintVisualizedBitFieldWithPositiveItems(string expectedValueAsString, BitFieldWithPositiveItems value)
        {
            Console.WriteLine("{0, 20} {1, 20} = {2}", expectedValueAsString, value, VisualizeByteArray((byte*)&value, sizeof(int)));
        }

        private static void PrintBitsAreSetOnNegativeItems(BitFieldWithNegativeItems flags, BitFieldWithNegativeItems setFlags)
        {
            Console.WriteLine("{0, 20} in {1, 20} -> {2}", setFlags, flags, (flags & setFlags) == setFlags ? "set" : "not set");
        }

        private static unsafe void PrintVisualizedBitFieldWithNegativeItems(string expectedValueAsString, BitFieldWithNegativeItems value)
        {
            Console.WriteLine("{0, 20} {1, 20} = {2}", expectedValueAsString, value, VisualizeByteArray((byte*)&value, sizeof(int)));
        }

        private static unsafe void PrintVisualizedInt32(int value)
        {
            Console.WriteLine("{0, 10} = {1}", value, VisualizeByteArray((byte*)&value, sizeof(int)));
        }

        private static readonly byte[] singleBitBytes = { 128, 64, 32, 16, 8, 4, 2, 1 };
        private static unsafe string VisualizeByteArray(byte* firstByte, int numberOfBytes)
        {            
            byte* current = firstByte;
            byte* end = current + numberOfBytes;
            
            StringBuilder sb = new StringBuilder();
            while (current < end)
            {
                foreach (var singleBitByte in singleBitBytes)
                    sb.Append((*current & singleBitByte) == singleBitByte ? '1' : '0');

                sb.Append(' ');
                current++;
            }

            return sb.ToString();
        }
        #endregion

        #region Experiments - Enums with Exotic Underlying Types
        /// <remarks>
        /// According to the ECMA standard, the CLR supports underlying type of float or double even most languages don't choose to expose it.
        /// It looks to me that the .NET library classes are not sure what to do when they get such enumeration.
        /// </remarks>
        [Test]
        [Ignore("Experiment")]
        public static void Create_Enum_With_UnderlyingType_Double()
        {
            // Creation of the enumeration itself works fine.
            Type enumType = TypeExtensionsTests.EmitEnum(new[] { "Zero", "One", "OnePointOne", "Two", "TwoPointTwo" }, new[] { 0d, 1d, 1.1d, 2d, 2.2d }, false);

            // Now comes the a funny part. Usage is a bit problematic.
            // Enum.GetValues() will survive the call. But the returned values don't look healthy in the debugger.
            foreach (object value in Enum.GetValues(enumType))
            {
                // The line below will crash because it cannot get the name of the enumeration item.
                /*
                 System.ArgumentException : The value passed in must be an enum base or an underlying type for an enum, such as an Int32.
                 Parameter name: value
                    at System.RuntimeType.GetEnumName(Object value)
                    at System.Enum.GetName(Type enumType, Object value)
                    at System.Enum.InternalFormat(RuntimeType eT, Object value)
                    at System.Enum.ToString()
                    at System.Enum.ToString(String format)
                    at System.Enum.ToString(String format, IFormatProvider provider)
                    at System.Text.StringBuilder.AppendFormat(IFormatProvider provider, String format, Object[] args)
                    at System.String.Format(IFormatProvider provider, String format, Object[] args)
                    at System.IO.TextWriter.WriteLine(String format, Object arg0, Object arg1)
                    at System.IO.TextWriter.SyncTextWriter.WriteLine(String format, Object arg0, Object arg1)
                    at System.Console.WriteLine(String format, Object arg0, Object arg1)
                    at SwissKnife.Tests.Unit.Enumeration.BitFieldTests.Create_Enum_With_UnderlyingType_Double() in BitFieldTests.cs: line 182
                 */
                //Console.WriteLine("{0} {1}", value, (double)value);

                // The conversion to double works, but it doesn't give the proper values back.
                // It always returns zero.
                Console.WriteLine((double)value);
            }
        }

        [Test]
        [Ignore("Experiment")]
        public static void Create_FlagsEnum_With_UnderlyingType_Double()
        {
            // See all the remarks written above.

            Type enumType = TypeExtensionsTests.EmitEnum(new[] { "Zero", "One", "OnePointOne", "Two", "TwoPointTwo" }, new[] { 0d, 1d, 1.1d, 2d, 2.2d }, true);

            foreach (object value in Enum.GetValues(enumType))
            {
                Console.WriteLine((double)value);
            }
        }

        /// <remarks>
        /// In this case everything works like expected.
        /// </remarks>
        [Test]
        [Ignore("Experiment")]
        public static void Create_Enum_With_UnderlyingType_Int32()
        {
            Type enumType = TypeExtensionsTests.EmitEnum(new[] { "Zero", "One", "Two" }, new[] { 0, 1, 2 }, false);

            foreach (object value in Enum.GetValues(enumType))
            {
                Console.WriteLine("{0} {1}", value, (int)value);
            }
        }

        [Test]
        [Ignore("Experiment")]
        public static void Create_FlagsEnum_With_UnderlyingType_Int32()
        {
            Type enumType = TypeExtensionsTests.EmitEnum(new[] { "None", "One", "Two", "FirstTwo", "All" }, new[] { 0, 1, 2, 3, 3 }, true);

            foreach (object value in Enum.GetValues(enumType))
            {
                Console.WriteLine("{0} {1}", value, (int)value);
            }
        }
        #endregion

        #region IsFlagsEnum
        private enum NonFlagsEnumeration { }
        [Flags]
        private enum FlagsEnumeration { }

        [Test]
        public void IsFlagsEnum_TypeIsNull_ThrowsException()
        {
            string parameterName = Assert.Throws<ArgumentNullException>(() => FlagsEnum.IsFlagsEnum(null)).ParamName;
            Assert.That(parameterName, Is.EqualTo("enumType"));
        }

        [Test]
        public void IsFlagsEnum_TypeIsNotEnum_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => FlagsEnum.IsFlagsEnum(typeof(object)));
            Assert.That(exception.ParamName, Is.EqualTo("enumType"));
            Assert.That(exception.Message.Contains("does not represent an enumeration"));
        }

        [Test]
        public void IsFlagsEnum_TypeIsGenericType_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => FlagsEnum.IsFlagsEnum(typeof(List<object>)));
            Assert.That(exception.ParamName, Is.EqualTo("enumType"));
            Assert.That(exception.Message.Contains("does not represent an enumeration"));
        }

        [Test]
        public void IsFlagsEnum_TypeIsOpenGenericType_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => FlagsEnum.IsFlagsEnum(typeof(List<>)));
            Assert.That(exception.ParamName, Is.EqualTo("enumType"));
            Assert.That(exception.Message.Contains("does not represent an enumeration"));
        }

        [Test]
        public void IsFlagsEnum_NonFlagsEnum_ReturnsFalse()
        {
            Assert.That(FlagsEnum.IsFlagsEnum(typeof(NonFlagsEnumeration)), Is.False);
        }

        [Test]
        public void IsFlagsEnum_FlagsEnum_ReturnsTrue()
        {
            Assert.That(FlagsEnum.IsFlagsEnum(typeof(FlagsEnumeration)), Is.True);
        }

        [Test]
        public void IsFlagsEnum_EmitedNonFlagsEnum_ReturnsFalse()
        {
            Type enumType = TypeExtensionsTests.EmitEnum(new[] { "Zero", "One", "Two" }, new[] { 0, 1, 2 }, false);
            Assert.That(FlagsEnum.IsFlagsEnum(enumType), Is.False);
        }

        [Test]
        public void IsFlagsEnum_EmitedNonFlagsEnumWithNonIntegerUnderlyaingType_ReturnsFalse()
        {
            Type enumType = TypeExtensionsTests.EmitEnum(new[] { "Zero", "One", "OnePointOne", "Two", "TwoPointTwo" }, new[] { 0d, 1d, 1.1d, 2d, 2.2d }, false);
            Assert.That(FlagsEnum.IsFlagsEnum(enumType), Is.False);
        }

        [Test]
        public void IsFlagsEnum_EmitedFlagsEnum_ReturnsTrue()
        {
            Type enumType = TypeExtensionsTests.EmitEnum(new[] { "None", "One", "Two", "All" }, new[] { 0, 1, 2, 3 }, true);
            Assert.That(FlagsEnum.IsFlagsEnum(enumType), Is.True);
        }

        [Test]
        public void IsFlagsEnum_EmitedFlagsEnumWithNonIntegerUnderlyaingType_ReturnsFalse()
        {
            Type enumType = TypeExtensionsTests.EmitEnum(new[] { "Zero", "One", "OnePointOne", "Two", "TwoPointTwo" }, new[] { 0d, 1d, 1.1d, 2d, 2.2d }, true);
            Assert.That(FlagsEnum.IsFlagsEnum(enumType), Is.False);
        }

        #endregion

        #region IsFlagsEnumOfTEnum
        // ReSharper disable UnusedTypeParameter
        private struct GenericStruct<T> { }
        // ReSharper restore UnusedTypeParameter

        [Test]
        public void IsFlagsEnumOfTEnum_TypeIsNotEnum_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => FlagsEnum.IsFlagsEnum<int>());
            Assert.That(exception.ParamName, Is.EqualTo("TEnum"));
            Assert.That(exception.Message.Contains("does not represent an enumeration"));
        }

        [Test]
        public void IsFlagsEnumOfTEnum_TypeIsGenericStrucutreType_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => FlagsEnum.IsFlagsEnum<GenericStruct<int>>());
            Assert.That(exception.ParamName, Is.EqualTo("TEnum"));
            Assert.That(exception.Message.Contains("does not represent an enumeration"));
        }

        [Test]
        public void IsFlagsEnumOfTEnum_NonBitFieldEnum_ReturnsFalse()
        {
            Assert.That(FlagsEnum.IsFlagsEnum<NonFlagsEnumeration>(), Is.False);
        }

        [Test]
        public void IsFlagsEnumOfTEnum_BitFieldEnum_ReturnsTrue()
        {
            Assert.That(FlagsEnum.IsFlagsEnum<FlagsEnumeration>(), Is.True);
        }
        #endregion
    }
    // ReSharper restore InconsistentNaming
}
