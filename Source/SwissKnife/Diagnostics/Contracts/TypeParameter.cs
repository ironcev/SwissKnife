#define CONTRACTS_FULL
using System;

namespace SwissKnife.Diagnostics.Contracts
{
    /// <summary>
    /// Contains static contract methods that validate preconditions on type parameters.
    /// Each static method of the <see cref="TypeParameter"/> class throws an exception if a certain precondition is not fulfilled.
    /// </summary>
    /// <threadsafety static="true"/>
    public static class TypeParameter
    {
        /// <summary>
        /// Checks if a type parameter represents an enumeration.
        /// </summary>
        /// <param name="typeParameter">The type that has to be checked if it represents an enumeration.</param>
        /// <param name="typeParameterName">The name of the type parameter. E.g. "T", "TInput", "TOutput", or "TResult"
        /// in case of generic parameters or e.g. "enumType" in case of method arguments.</param>
        /// <exception cref="ArgumentNullException"><paramref name="typeParameter"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="typeParameter"/> does not represent an enumeration.</exception>
        public static void IsEnum(Type typeParameter, Option<string> typeParameterName)
        {
            Argument.IsNotNull(typeParameter, typeParameterName);

            if (!typeParameter.IsEnum)
                throw new ArgumentException(string.Format("The type parameter does not represent an enumeration. The type parameter was '{0}'.", typeParameter.AssemblyQualifiedName),
                                            typeParameterName.ValueOrNull);
        }

        /// <inheritdoc cref="IsEnum(Type, Option{string})" select="summary"/>
        /// <typeparam name="T">The type that has to be checked if it represents an enumeration.</typeparam>
        /// <inheritdoc cref="IsEnum(Type, Option{string})" select="param[@name='typeParameterName']"/>
        /// <param name="typeParameterName">The name of the type parameter. E.g. "T", "TInput", "TOutput", or "TResult".</param>
        /// <exception cref="ArgumentException"><typeparam name="T"/> does not represent an enumeration.</exception>
        public static void IsEnum<T>(Option<string> typeParameterName)
        {
            IsEnum(typeof(T), typeParameterName);
        }
    }
}
