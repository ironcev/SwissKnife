namespace SwissKnife.Security.Cryptography // TODO-IG: All types in this namespace are in development. Review and refactoring is needed.
{
    /// <preliminary/>
    /// <summary>
    /// Different string formats used for encryption and hashing.
    /// </summary>
    public enum StringFormat
    {
        /// <summary>
        /// Hexadecimal string format.
        /// </summary>
        Hex,
        /// <summary>
        /// Base64 string format.
        /// </summary>
        Base64,
        /// <summary>
        /// Text string format.
        /// </summary>
        Text
    }
}