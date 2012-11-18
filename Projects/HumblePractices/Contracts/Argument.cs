#define CONTRACTS_FULL
using System;
using System.Diagnostics.Contracts;
using HumblePractices.Idioms;

namespace HumblePractices.Contracts
{
    /// <summary>
    /// Contains static contract argument validator methods that represent preconditions on method arguments.
    /// Each static method of the <see cref="Argument"/> class throws an exception if a certain precondition is not fulfilled.
    /// </summary>
    public static class Argument
    {
        /// <summary>
        /// Checks that <see cref="string"/> method parameter is not null or white space.
        /// </summary>
        /// <param name="parameterValue">Value of the method parameter.</param>
        /// <param name="parameterName">Name of the method parameter.</param>
        /// <exception cref="ArgumentException">If <see cref="parameterValue"/> is null or white space.</exception>
        [ContractArgumentValidator]
        public static void IsNotNullOrWhitespace(string parameterValue, Option<string> parameterName)
        {
            IsNotNull(parameterValue, parameterName);
            if (string.IsNullOrWhiteSpace(parameterValue)) // Argument is surely not null. We are actually checking for white spaces only.
                throw new ArgumentException("Parameter value must not be white space.", parameterName.ValueOrNull);
            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks that <see cref="string"/> method parameter is not null or empty.
        /// </summary>
        /// <param name="parameterValue">Value of the method parameter.</param>
        /// <param name="parameterName">Name of the method parameter.</param>
        /// <exception cref="ArgumentException">If <see cref="parameterValue"/> is null or empty.</exception>
        [ContractArgumentValidator]
        public static void IsNotNullOrEmpty(string parameterValue, Option<string> parameterName)
        {
            IsNotNull(parameterValue, parameterName);
            if (string.IsNullOrEmpty(parameterValue)) // Argument is surely not null. We are actually checking for empty string only.
                throw new ArgumentException("Parameter value must not be empty string.", parameterName.ValueOrNull);
            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks that method parameter is not null.
        /// </summary>
        /// <param name="parameterValue">Value of the method parameter.</param>
        /// <param name="parameterName">Name of the method parameter.</param>
        /// <exception cref="ArgumentNullException">If <see cref="parameterValue"/> is null.</exception>
        [ContractArgumentValidator]
        public static void IsNotNull(object parameterValue, Option<string> parameterName)
        {
            if (parameterValue == null)
                throw new ArgumentNullException(parameterName.ValueOrNull);
            Contract.EndContractBlock();
        }
    }
}