using System;
using SwissKnife.Diagnostics.Contracts;

namespace SwissKnife
{
    /// <summary>
    /// Contains extension methods for <see cref="System.Type"/>.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets a value indicating whether the <paramref name="type"/> is static.
        /// </summary>
        /// <returns>
        /// true if the <paramref name="type"/> is static; otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
        public static bool IsStatic(this Type type)
        {
            #region Preconditions
            Argument.IsNotNull(type, "type");
            #endregion

            return type.IsAbstract && type.IsSealed;
        }

        /// <summary>
        /// Gets a value indicating whether the <paramref name="type"/> represents an enumeration that can be treated as a bit field; that is, a set of flags.
        /// </summary>
        /// <remarks>
        /// The <paramref name="type"/> is a bit field if it is an enumeration (<see cref="Enum"/>) with the <see cref="FlagsAttribute"/> applied.
        /// </remarks>
        /// <returns>
        /// true if the <paramref name="type"/> is a bit field; otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
        /// <exception cref="InvalidOperationException">The <paramref name="type"/> is loaded into the reflection-only context.</exception>
        public static bool IsBitField(this Type type)
        {
            #region Preconditions
            Argument.IsNotNull(type, "type");
            #endregion

            return type.IsEnum && type.GetCustomAttributes(typeof(FlagsAttribute), false).Length > 0;
        }
    }
}
