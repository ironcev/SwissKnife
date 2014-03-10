namespace SwissKnife.Security.Cryptography // TODO-IG: All types in this namespace are added because of an urgent need. Review and refactoring is needed. Originally developed by Marin Roncevic.
{
    /// <summary>
    /// Hashing providers.
    /// </summary>
    public enum HashingProvider
    {
        /// <summary>
        /// 160 bit, moderate security, medium speed.
        /// </summary>
        Sha1,
        /// <summary>
        /// 256 bit, high security, slow speed.
        /// </summary>
        Sha256,
        /// <summary>
        /// 384 bit, high security, slow speed.
        /// </summary>
        Sha384,
        /// <summary>
        /// 512 bit, extreme security, slow speed.
        /// </summary>
        Sha512, 
        /// <summary>
        /// 128 bit, moderate security, medium speed.
        /// </summary>
        Md5
    }
}