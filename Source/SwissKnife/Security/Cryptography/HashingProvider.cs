namespace SwissKnife.Security.Cryptography // TODO-IG: All types in this namespace are in development. Review and refactoring is needed.
{
    /// <preliminary/>
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