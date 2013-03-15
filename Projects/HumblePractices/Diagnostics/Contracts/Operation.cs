#define CONTRACTS_FULL
using System;
using System.Diagnostics.Contracts;
using HumblePractices.Idioms;

namespace HumblePractices.Diagnostics.Contracts
{
    /// <summary>
    /// Contains static contract argument validator methods that represent preconditions on method arguments.
    /// Each static method of the <see cref="Operation"/> class throws an exception if a certain precondition is not fulfilled.
    /// </summary>
    public class Operation
    {
        /// <summary>
        /// Checks that operation satisfies <paramref name="validityCondition"/>.
        /// </summary>
        /// <param name="validityCondition">Logical condition that is true if the operation is valid.</param>
        /// <param name="message">Exception message if the operation is invalid.</param>
        /// <exception cref="InvalidOperationException">If <paramref name="validityCondition"/> is false.</exception>
        [ContractArgumentValidator]
        public static void IsValid(bool validityCondition, Option<string> message)
        {
            if (!validityCondition)
                throw new InvalidOperationException(message.ValueOrNull);
            Contract.EndContractBlock();
        }
    }
}
