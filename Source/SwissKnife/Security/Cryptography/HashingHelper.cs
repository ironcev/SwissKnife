using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SwissKnife.Security.Cryptography // TODO-IG: All types in this namespace are added because of an urgent need. Review and refactoring is needed. Originally developed by Marin Roncevic.
{
    /// <summary>
    /// Helper class for hashing and verifying hashed data.
    /// </summary>
    public static class HashingHelper
    {
        internal static readonly Encoding DefaultEncoding = Encoding.UTF8;

        /// <summary>
        /// Returns hashing result based on <paramref name="data"/> and <paramref name="hashingProvider"/>.
        /// </summary>
        public static HashingResult Hash(string data, HashingProvider hashingProvider)
        {
            if (data == null) return HashingResult.DefaultHashResult;

            var hashAlgorithm = GetHashAlgorithm(hashingProvider);

            return new HashingResult(hashAlgorithm.ComputeHash(DefaultEncoding.GetBytes(data)));
        }

        private static HashAlgorithm GetHashAlgorithm(HashingProvider hashingProvider)
        {
            switch (hashingProvider)
            {
                case HashingProvider.Sha1:
                    return new SHA1Managed();
                case HashingProvider.Sha256:
                    return new SHA256Managed();
                case HashingProvider.Sha384:
                    return new SHA384Managed();
                case HashingProvider.Sha512:
                    return new SHA512Managed();
                case HashingProvider.Md5:
                    return new MD5CryptoServiceProvider();
            }

            throw new InvalidEnumArgumentException(string.Format("Not supported hashing provider: {0}", hashingProvider));
        }

        /// <summary>
        /// Returns true if <paramref name="inputToVerify"/> value is equal to the <paramref name="hashedValue"/> represented as a string.
        /// </summary>
        public static bool VerifyHash(string inputToVerify, string hashedValue, StringFormat hashedValueStringFormat, HashingProvider hashProvider)
        {
            return Hash(inputToVerify, hashProvider).As(hashedValueStringFormat) == hashedValue;
        }

        /// <summary>
        /// Returns true if <paramref name="inputToVerify"/> value is equal to the <paramref name="hashedValue"/> represented as a byte array.
        /// </summary>
        public static bool VerifyHash(string inputToVerify, byte[] hashedValue, HashingProvider hashProvider)
        {
            return Util.AreBytesEqual(Hash(inputToVerify, hashProvider).Bytes.ToArray(), hashedValue);
        }
    }
}
