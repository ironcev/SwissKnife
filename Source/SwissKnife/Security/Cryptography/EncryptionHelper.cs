using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SwissKnife.Diagnostics.Contracts;

// TODO-IG: Originall inspired by http://www.codeproject.com/Articles/10154/NET-Encryption-Simplified 
namespace SwissKnife.Security.Cryptography // TODO-IG: All types in this namespace are added because of an urgent need. Review and refactoring is needed. Originally developed by Marin Roncevic.
{
    /// <summary>
    /// Helper class for encrypting/decrypting data.
    /// </summary>
    public static class EncryptionHelper
    {
        private static readonly Encoding DefaultEncoding = Encoding.UTF8;
        private const string DefaultInitializationVector = "zA%1-q=T@";

        /// <summary>
        /// Returns <paramref name="data"/> encrypted in target <paramref name="stringFormat"/> based on <paramref name="key"/> and <paramref name="encryptionProvider"/>.
        /// </summary>
        public static string Encrypt(string data, string key, EncryptionProvider encryptionProvider, StringFormat stringFormat)
        {
            Argument.IsNotNull(data, "data");
            Argument.IsNotNull(key, "key");

            var encryptionAlgorithm = GetEncryptionAlgorithm(encryptionProvider);

            var dataBytes = DefaultEncoding.GetBytes(data);

            encryptionAlgorithm.Key = CreateCryptoKey(encryptionAlgorithm, key);
            encryptionAlgorithm.IV = CreateCryptoIv(encryptionAlgorithm, DefaultInitializationVector);

            string result;

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encryptionAlgorithm.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataBytes, 0, dataBytes.Length);
                }

                result = Util.BytesToString(ms.ToArray(), stringFormat);
            }

            return result;
        }

        /// <summary>
        /// Returns <paramref name="data"/> decrypted based on encryption original <paramref name="stringFormat"/>, <paramref name="key"/> and <paramref name="encryptionProvider"/>.
        /// </summary>
        public static string Decrypt(string data, string key, EncryptionProvider encryptionProvider, StringFormat stringFormat)
        {
            Argument.IsNotNull(data, "data");
            Argument.IsNotNull(key, "key");

            var encryptionAlgorithm = GetEncryptionAlgorithm(encryptionProvider);

            var dataBytes = Util.StringToBytes(data, stringFormat, DefaultEncoding).ToArray();

            encryptionAlgorithm.Key = CreateCryptoKey(encryptionAlgorithm, key);
            encryptionAlgorithm.IV = CreateCryptoIv(encryptionAlgorithm, DefaultInitializationVector);

            var result = new byte[dataBytes.Length];

            using (var ms = new MemoryStream(dataBytes, 0, dataBytes.Length))
            {
                using (var cs = new CryptoStream(ms, encryptionAlgorithm.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    cs.Read(result, 0, dataBytes.Length);
                }
            }

            // Clean trailing zeros.
            result = Util.CleanBytes(result).ToArray();

            return Util.BytesToTextString(result, DefaultEncoding);
        }

        private static SymmetricAlgorithm GetEncryptionAlgorithm(EncryptionProvider encryptionProvider)
        {
            switch (encryptionProvider)
            {
                case EncryptionProvider.Des:
                    return new DESCryptoServiceProvider();
                case EncryptionProvider.Rc2:
                    return new RC2CryptoServiceProvider();
                case EncryptionProvider.Rijndael:
                    return new RijndaelManaged();
                case EncryptionProvider.TripleDes:
                    return new TripleDESCryptoServiceProvider();
            }

            throw new InvalidEnumArgumentException(string.Format("Not supported encryption provider: {0}", encryptionProvider));
        }

        private static byte[] CreateCryptoKey(SymmetricAlgorithm cryptoAlgorithm, string key)
        {
            var keyBytes = DefaultEncoding.GetBytes(key);
            var maxBytes = cryptoAlgorithm.LegalKeySizes[0].MaxSize / 8;
            var minBytes = cryptoAlgorithm.LegalKeySizes[0].MinSize / 8;

            return GetBytesRange(keyBytes, minBytes, maxBytes);
        }

        private static byte[] CreateCryptoIv(SymmetricAlgorithm cryptoAlgorithm, string iv)
        {
            var ivBytes = DefaultEncoding.GetBytes(iv);
            var bytes = cryptoAlgorithm.BlockSize / 8;

            return GetBytesRange(ivBytes, bytes, bytes);
        }

        private static byte[] GetBytesRange(byte[] input, int minBytes, int maxBytes)
        {
            var result = new List<byte>(input);
            
            if (maxBytes > 0 && result.Count > maxBytes)
            {
                result.Clear();
                var temp = new byte[maxBytes];
                Array.Copy(input, temp, temp.Length);
                result.AddRange(temp);
            }

            if (minBytes > 0 && result.Count < minBytes)
            {
                byte[] temp = new byte[minBytes];
                byte[] resultArray = result.ToArray();
                Array.Copy(resultArray, temp, resultArray.Length);
                result.Clear();
                result.AddRange(temp);
            }

            return result.ToArray();
        }
    }
}