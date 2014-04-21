namespace SwissKnife.Security.Cryptography
{
    /// <summary>
    /// Encryption providers.
    /// </summary>
    public enum EncryptionProvider // TODO-IG: All types in this namespace are added because of an urgent need. Review and refactoring is needed.
    {
        Des,
        TripleDes,
        Rc2,
        Rijndael
    }
}