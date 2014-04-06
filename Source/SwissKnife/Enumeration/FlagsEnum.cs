using System;
using SwissKnife.Diagnostics.Contracts;

namespace SwissKnife.Enumeration
{
    /// <summary>
    /// Contains utility methods for working with flags enumerations; 
    /// that is, with enumerations that can be treated as bit fields or, in other words, sets of flags.
    /// </summary>
    /// <remarks>
    /// A flag enumeration is an enumeration (see <see cref="Enum"/>) with the <see cref="FlagsAttribute"/> applied.
    /// In addition, the underlying type of the enumeration must be one of the types defined in <see cref="TypeExtensions.UnderlyingTypesOfFlagsEnums"/>.
    /// </remarks>
    public class FlagsEnum
    {
        /// <summary>
        /// Gets a value indicating whether an enumeration type represents a flags enumeration.
        /// </summary>
        /// <remarks>
        /// <b>Note</b><br/>
        /// This method throws exception if the <typeparamref name="TEnum"/> is not an <see cref="Enum"/> type.
        /// If you want to check if an arbitrary type is a flags enumeration, use the <see cref="TypeExtensions.IsFlagsEnum"/> method instead.
        /// </remarks>
        /// <returns>
        /// true if the <typeparamref name="TEnum"/> is a flags enumeration; otherwise false.
        /// </returns>
        /// <exception cref="ArgumentException"><typeparamref name="TEnum"/>> is not an <see cref="Enum"/> type.</exception>
        public static bool IsFlagsEnum<TEnum>() where TEnum : struct
        {
            TypeParameter.IsEnum<TEnum>("TEnum");

            return typeof(TEnum).IsFlagsEnum();
        }

        /// <summary>
        /// Gets a value indicating whether an enumeration type represents a flags enumeration.
        /// </summary>
        /// <remarks>
        /// <b>Note</b><br/>
        /// This method throws exception if the <paramref name="enumType"/> is not an <see cref="Enum"/> type.
        /// If you want to check if an arbitrary type is a flags enumeration, use the <see cref="TypeExtensions.IsFlagsEnum"/> method instead.
        /// </remarks>
        /// <returns>
        /// true if the <paramref name="enumType"/> is a flags enumeration; otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="enumType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="enumType"/> is not an <see cref="Enum"/> type.</exception>
        public static bool IsFlagsEnum(Type enumType)
        {
            TypeParameter.IsEnum(enumType, "enumType");

            return enumType.IsFlagsEnum();
        }
    }
}
