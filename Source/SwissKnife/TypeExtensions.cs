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
        /// <exception cref="ArgumentNullException">If <paramref name="type"/> is null.</exception>
        public static bool IsStatic(this Type type)
        {
            #region Preconditions
            Argument.IsNotNull(type, "type");
            #endregion

            return type.IsAbstract && type.IsSealed;
        }
    }
}
