namespace SwissKnife.Security.Cryptography // TODO-IG: All types in this namespace are added because of an urgent need. Review and refactoring is needed. Originally developed by Marin Roncevic.
{
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