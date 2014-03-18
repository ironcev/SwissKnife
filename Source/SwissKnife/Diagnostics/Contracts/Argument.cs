#define CONTRACTS_FULL
using System;
using System.Diagnostics.Contracts;

namespace SwissKnife.Diagnostics.Contracts
{
    /// <summary>
    /// Contains static contract methods that validate preconditions on method arguments.
    /// Each static method of the <see cref="Argument"/> class throws an exception if a certain precondition is not fulfilled.
    /// </summary>
    public static class Argument
    {
        /// <summary>
        /// Checks if a <see cref="string"/> method parameter is not null, empty or white space.
        /// </summary>
        /// <param name="parameterValue">The value of the method parameter.</param>
        /// <param name="parameterName">The name of the method parameter.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parameterValue"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="parameterValue"/> is empty.<br/>-or-<br/><paramref name="parameterValue"/> is white space.</exception>
        [ContractArgumentValidator]
        public static void IsNotNullOrWhitespace(string parameterValue, Option<string> parameterName)
        {
            IsNotNullOrEmpty(parameterValue, parameterName);
            if (string.IsNullOrWhiteSpace(parameterValue)) // Argument is surely not null or empty. We are actually checking for white spaces.
                throw new ArgumentException("Parameter value must not be white space.", parameterName.ValueOrNull);
            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks if a <see cref="string"/> method parameter is not null or empty.
        /// </summary>
        /// <param name="parameterValue">The value of the method parameter.</param>
        /// <param name="parameterName">The name of the method parameter.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parameterValue"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="parameterValue"/> is empty string.</exception>
        [ContractArgumentValidator]
        public static void IsNotNullOrEmpty(string parameterValue, Option<string> parameterName)
        {
            IsNotNull(parameterValue, parameterName);
            if (string.IsNullOrEmpty(parameterValue)) // Argument is surely not null. We are actually checking for empty string.
                throw new ArgumentException("Parameter value must not be empty string.", parameterName.ValueOrNull);
            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks if a method parameter is not null.
        /// </summary>
        /// <param name="parameterValue">The value of the method parameter.</param>
        /// <param name="parameterName">The name of the method parameter.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parameterValue"/> is null.</exception>
        [ContractArgumentValidator]
        public static void IsNotNull(object parameterValue, Option<string> parameterName)
        {
            if (parameterValue == null)
                throw new ArgumentNullException(parameterName.ValueOrNull);
            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks if a method parameter satisfies the <paramref name="validityCondition"/>.
        /// </summary>
        /// <param name="validityCondition">Logical condition that is true if the method parameter is valid.</param>
        /// <param name="parameterName">The name of the method parameter.</param>
        /// <param name="exceptionMessage">The error message that describes the reason why the parameter is not valid.</param>
        /// <exception cref="ArgumentException"><paramref name="validityCondition"/> is false.</exception>
        [ContractArgumentValidator]
        public static void IsValid(bool validityCondition, Option<string> exceptionMessage, Option<string> parameterName)
        {
            if (!validityCondition)
                throw new ArgumentException(exceptionMessage.ValueOrNull, parameterName.ValueOrNull);
            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks if a method parameter is compatible with a given type.
        /// </summary>
        /// <remarks>
        /// A method parameter is compatible with a given type if it can be assigned to a variable of that type.
        /// Null is considered to be compatible with all types.
        /// Thus, if the <paramref name="parameterValue"/> is null, this method will never throw an exception.
        /// </remarks>
        /// <param name="parameterValue">The value of the method parameter.</param>
        /// <param name="type">The type to which the method parameter must be assignable.</param>
        /// <param name="parameterName">The name of the method parameter.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="parameterValue"/> cannot be assigned to an instance of the <paramref name="type"/>.</exception>
        public static void Is(Option<object> parameterValue, Type type, Option<string> parameterName)
        {
            #region Preconditions
            IsNotNull(type, "type");
            #endregion

            if (parameterValue.IsNone) return;

            if (!type.IsInstanceOfType(parameterValue.Value))
                throw new ArgumentException(string.Format("Parameter is not compatible with the type '{0}'. The type of the parameter was '{1}'.", type.AssemblyQualifiedName, parameterValue.Value.GetType().AssemblyQualifiedName), parameterName.ValueOrNull);
        }

        /// <summary>
        /// Checks if a method parameter is compatible with a given type.
        /// </summary>
        /// <remarks>
        /// A method parameter is compatible with a given type if it can be assigned to a variable of that type.
        /// Null is considered to be compatible with all types.
        /// Thus, if the <paramref name="parameterValue"/> is null, this method will never throw an exception.
        /// </remarks>
        /// <typeparam name="T">The type to which the method parameter must be assignable.</typeparam>
        /// <param name="parameterValue">The value of the method parameter.</param>
        /// <param name="parameterName">The name of the method parameter.</param>
        /// <exception cref="ArgumentException"><paramref name="parameterValue"/> cannot be assigned to an instance of <typeparamref name="T"/>.</exception>
        public static void Is<T>(Option<object> parameterValue, Option<string> parameterName)
        {
            Is(parameterValue, typeof(T), parameterName);
        }

        /// <summary>
        /// Checks that <see cref="int"/> method parameter is <b>greater than or equal to</b> the <paramref name="lowerBound"/> and <b>lower than</b> the <paramref name="upperBound"/>.
        /// </summary>
        /// <param name="parameterValue">The value of the method parameter.</param>
        /// <param name="lowerBound">The lower bound of the range.</param>
        /// <param name="upperBound">The upper bound of the range.</param>
        /// <param name="parameterName">The name of the method parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="parameterValue"/> is not in the range [<paramref name="lowerBound"/>, <paramref name="upperBound"/>).</exception>
        public static void IsInRange(int parameterValue, int lowerBound, int upperBound, Option<string> parameterName)
        {
            if (!(lowerBound <= parameterValue && parameterValue < upperBound))
                throw new ArgumentOutOfRangeException(parameterName.ValueOrNull, parameterValue, string.Format("Parameter value is out of the range [{0}, {1}>.", lowerBound, upperBound));
        }

        /// <summary>
        /// Checks that <see cref="int"/> method parameter is greater than zero.
        /// </summary>
        /// <param name="parameterValue">The value of the method parameter.</param>
        /// <param name="parameterName">The name of the method parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="parameterValue"/> is not greater than zero.</exception>
        public static void IsGreaterThanZero(int parameterValue, Option<string> parameterName)
        {
            if (parameterValue <= 0)
                throw new ArgumentOutOfRangeException(parameterName.ValueOrNull, parameterValue, "Parameter value must be greater than zero.");
        }
    }
}