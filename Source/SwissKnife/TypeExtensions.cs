using System;
using System.Linq;
using SwissKnife.Diagnostics.Contracts;

namespace SwissKnife
{
    /// <summary>
    /// Contains extension methods for <see cref="System.Type"/>.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets the <see cref="Type"/>s that can appear as underlying types in flags enumerations; 
        /// that is, in enumerations that can be treated as bit fields or, in other words, sets of flags.
        /// </summary>
        /// <remarks>
        /// <para>
        /// According to the ECMA standard, the CLR supports underlying types of e.g. float or double even most languages don't choose to expose it.
        /// Still, it is possible to emit enumerations with <see cref="FlagsAttribute"/> that have e.g. float as the underlying type.
        /// Such enumerations shouldn't be treated as flags enumerations although they have <see cref="FlagsAttribute"/> set.
        /// This property lists the types that qualify as underlying types for flags enumerations.
        /// </para>
        /// <para>
        /// Those types are:<br/>
        /// <ul>
        /// <li><see cref="byte"/></li>
        /// <li><see cref="sbyte"/></li>
        /// <li><see cref="short"/></li>
        /// <li><see cref="ushort"/></li>
        /// <li><see cref="int"/></li>
        /// <li><see cref="uint"/></li>
        /// <li><see cref="long"/></li>
        /// <li><see cref="ulong"/></li>
        /// </ul>
        /// </para>
        /// </remarks>
        /// <returns>
        /// An array of types that can be used as underlying types in flags enumerations.
        /// </returns>
        public static Type[] UnderlyingTypesOfFlagsEnums { get; private set; }

        static TypeExtensions()
        {
            UnderlyingTypesOfFlagsEnums = new []
            {
                typeof(byte),
                typeof(sbyte),
                typeof(short),
                typeof(ushort),
                typeof(int),
                typeof(uint),
                typeof(long),
                typeof(ulong)
            };
        }

        /// <summary>
        /// Gets a value indicating whether the <paramref name="type"/> is static.
        /// </summary>
        /// <returns>
        /// true if the <paramref name="type"/> is static; otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
        public static bool IsStatic(this Type type)
        {
            Argument.IsNotNull(type, "type");

            return type.IsAbstract && type.IsSealed;
        }
        
        /// <summary>
        /// Gets a value indicating whether a <paramref name="type"/> represents a flags enumeration; 
        /// that is, an enumeration that can be treated as a bit field or, in other words, a set of flags.
        /// </summary>
        /// <remarks>
        /// The <paramref name="type"/> is a flags enumeration if it is an enumeration (see <see cref="Enum"/>) with the <see cref="FlagsAttribute"/> applied.
        /// In addition, the underlying type of the enumeration must be one of the types defined in <see cref="UnderlyingTypesOfFlagsEnums"/>.
        /// </remarks>
        /// <returns>
        /// true if the <paramref name="type"/> is a flags enumeration (bit field); otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
        /// <exception cref="InvalidOperationException">The <paramref name="type"/> is loaded into the reflection-only context.</exception>        
        public static bool IsFlagsEnum(this Type type)
        {
            Argument.IsNotNull(type, "type");

            return type.IsEnum && 
                   type.GetCustomAttributes(typeof(FlagsAttribute), false).Length > 0 &&
                   UnderlyingTypesOfFlagsEnums.Contains(Enum.GetUnderlyingType(type));
        }
    }
}
