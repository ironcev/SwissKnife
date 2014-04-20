using System;

namespace SwissKnife.Time
{
    /// <summary>
    /// <see cref="TimeGenerator"/> that always generates the same time.
    /// </summary>
    /// <remarks>
    /// The constant time to generate is defined in the class constructor and cannot be changed afterwards.
    /// It can be inspected via the <see cref="ConstantTime"/> property.
    /// </remarks>
    /// <threadsafety static="true" instance="false"/>
    [Serializable]
    public class ConstantTimeGenerator : TimeGenerator
    {
        /// <summary>
        /// Gets the constant time to generate.
        /// </summary>
        public DateTimeOffset ConstantTime { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantTimeGenerator"/> class that always generates the <paramref name="constantTime"/>.
        /// </summary>
        /// <param name="constantTime">The constant time to generate with the offset set to the local time's offset from Coordinated Universal Time (UTC).</param>
        public ConstantTimeGenerator(DateTimeOffset constantTime)
        {
            ConstantTime = constantTime;
        }

        /// <summary>
        /// Gets a <see cref="DateTimeOffset"/> object that represents the current date and time.
        /// This object is same as the <see cref="ConstantTime"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="DateTimeOffset"/> object whose date and time is same as the <see cref="ConstantTime"/>.
        /// </returns>
        public override DateTimeOffset LocalNow()
        {
            return ConstantTime;
        }
    }
}
