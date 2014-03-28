using System;

namespace SwissKnife.Time
{
    /// <summary>
    /// Generates current date and time.
    /// </summary>
    /// <remarks>
    /// <p>
    /// Concrete implementations must override the <see cref="LocalNow"/> method to specify how the current data and time is generated.
    /// </p>
    /// <p>
    /// Consider using this class in all of the scenarios where taking control over date and time creation is necessary.
    /// Typical example would be mocking date and time generation for testing purposes.
    /// </p>
    /// </remarks>
    public abstract class TimeGenerator
    {
        /// <summary>
        /// Gets a <see cref="DateTimeOffset"/> object that represents the current date and time, with the offset set to the local time's offset from Coordinated Universal Time (UTC).
        /// </summary>
        /// <remarks>
        /// Override this method to specify how the current date and time is generated.
        /// </remarks>
        /// <returns>
        /// A <see cref="DateTimeOffset"/> object whose date and time represents the current local time and whose offset is the local time zone's offset from Coordinated Universal Time (UTC).
        /// </returns>
        public abstract DateTimeOffset LocalNow();

        /// <summary>
        /// Gets a <see cref="DateTimeOffset"/> object whose date and time are set to the current Coordinated Universal Time (UTC) date and time and whose offset is <see cref="TimeSpan.Zero"/>.
        /// </summary>
        /// <returns>
        /// An object whose date and time is the current Coordinated Universal Time (UTC) and whose offset is <see cref="TimeSpan.Zero"/>.
        /// </returns>
        public DateTimeOffset UtcNow()
        {
            return LocalNow().ToUniversalTime();
        }

        /// <summary>
        /// Gets the current date.
        /// </summary>
        /// <returns>
        /// An object that is set to today's date, with the time component set to 00:00:00.
        /// </returns>
        public DateTime Today()
        {
            return LocalNow().Date;
        }
    }
}
