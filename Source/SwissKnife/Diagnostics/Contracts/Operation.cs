#define CONTRACTS_FULL
using System;
using System.Diagnostics.Contracts;

namespace SwissKnife.Diagnostics.Contracts
{
    /// <summary>
    /// Contains static contract methods that validate operations done by methods.
    /// Each static method of the <see cref="Operation"/> class throws an exception if a certain precondition is not fulfilled.
    /// </summary>
    /// <threadsafety static="true"/>
    public class Operation
    {
        /// <summary>
        /// Checks that an operation satisfies a logical condition.
        /// </summary>
        /// <param name="validityCondition">The logical condition that is true if the operation is valid.</param>
        /// <param name="message">The exception message if the operation is invalid.</param>
        /// <exception cref="InvalidOperationException"><paramref name="validityCondition"/> is false.</exception>
        [ContractArgumentValidator]
        public static void IsValid(bool validityCondition, Option<string> message)
        {
            if (!validityCondition)
                throw new InvalidOperationException(message.ValueOrNull);
            Contract.EndContractBlock();
        }
    }
}
