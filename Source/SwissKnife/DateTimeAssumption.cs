using System;

namespace SwissKnife
{
    /// <summary>
    /// Represents an assumption about a date time value stored in a string.
    /// The value can be stored either as a local time or Coordinated Universal Time (UTC).
    /// </summary>
    /// <remarks>
    /// Use this enumeration to customize <see cref="String"/> parsing for date and time parsing methods of the <see cref="StringConvert"/> class.
    /// </remarks>
    public enum DateTimeAssumption // TODO-IG: Should we add None? If yes, should it be the default option for conversion methods that do not have DateTimeAssumption argument? Probably not (adding None), but AssumeLocal should be the default value.
    {
        /// <summary>
        /// If no time zone is specified in the parsed string, the string is assumed to denote a local time. 
        /// </summary>
        AssumeLocal,
        /// <summary>
        /// If no time zone is specified in the parsed string, the string is assumed to denote a Coordinated Universal Time (UTC). 
        /// </summary>
        AssumeUniversal
    }
}
