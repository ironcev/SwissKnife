using System;
using SwissKnife.Diagnostics.Contracts;

namespace SwissKnife.Time
{
    /// <summary>
    /// Generates current date and time.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Concrete implementations must override the <see cref="LocalNow"/> method to specify how the current data and time is generated.
    /// </para>
    /// <para>
    /// Consider using this class in all of the scenarios where taking control over date and time creation is necessary.
    /// Typical example would be mocking date and time generation for testing purposes.
    /// </para>
    /// </remarks>
    [Serializable]
    public abstract class TimeGenerator
    {
        private static readonly object syncLock = new object();
        private static Func<DateTimeOffset> getLocalNow = () => DateTimeOffset.Now;

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

        /// <summary>
        /// Gets and sets a delegate for returning object that represents the current date and time, with the offset set to the local time's offset from Coordinated Universal Time (UTC).
        /// </summary>
        /// <remarks>
        /// <para>
        /// This delegate is never null. By default, it returns the current date and time on the current computer.
        /// </para>
        /// <note type="caution">
        /// If the delegate throws an exception, that exception will be propagated to the caller.
        /// </note>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public static Func<DateTimeOffset> GetLocalNow
        {
            get
            {
                lock (syncLock)
                {
                    return getLocalNow;
                }
            }
            set
            {
                Argument.IsNotNull(value, "value");

                lock (syncLock)
                {                    
                    getLocalNow = value;                    
                }
            }
        }

        /// <summary>
        /// Gets a <see cref="DateTimeOffset"/> object whose date and time are set to the current Coordinated Universal Time (UTC) date and time and whose offset is <see cref="TimeSpan.Zero"/>.
        /// The current data and time is calculated by using the <see cref="GetLocalNow"/> delegate.
        /// </summary>
        /// <remarks>
        /// <note type="caution">
        /// If the <see cref="GetLocalNow"/> delegate throws an exception, that exception will be propagated to the caller.
        /// </note>
        /// </remarks>
        /// <returns>
        /// An object whose date and time is the current Coordinated Universal Time (UTC) and whose offset is <see cref="TimeSpan.Zero"/>.
        /// </returns>
        public static DateTimeOffset GetUtcNow()
        {
            return GetLocalNow().ToUniversalTime();
        }

        /// <summary>
        /// Gets the current date.
        /// The current data is calculated by using the <see cref="GetLocalNow"/> delegate.
        /// </summary>
        /// <remarks>
        /// <note type="caution">
        /// If the <see cref="GetLocalNow"/> delegate throws any exception, that exception will be propagated to the caller.
        /// </note>
        /// </remarks>
        /// <returns>
        /// An object that is set to today's date, with the time component set to 00:00:00.
        /// </returns>
        public static DateTime GetToday()
        {
            return GetLocalNow().Date;
        }
    }
}
