using System.Collections.Generic;
using SwissKnife.Diagnostics.Contracts;

namespace SwissKnife.Security.Cryptography // TODO-IG: All types in this namespace are added because of an urgent need. Review and refactoring is needed. Originally developed by Marin Roncevic.
{
    /// <summary>
    /// Represents hashed data result.
    /// </summary>
    public sealed class HashingResult
    {
        private string _asHex;
        private string _asBase64;
        private string _asText;

        internal HashingResult(IEnumerable<byte> bytes)
        {
            Argument.IsNotNull(bytes, "bytes");

            Bytes = bytes;
        }

        public IEnumerable<byte> Bytes { get; private set; }

        internal static HashingResult DefaultHashResult
        {
            get { return new HashingResult(new byte[0]); }
        }

        /// <summary>
        /// Hex representation of the underlying bytes.
        /// </summary>
        public string HexValue
        {
            get { return _asHex ?? (_asHex = Util.BytesToHexString(Bytes)); }
        }

        /// <summary>
        /// Base64 representation of the underlying bytes.
        /// </summary>
        public string Base64Value
        {
            get { return _asBase64 ?? (_asBase64 = Util.BytesToBase64String(Bytes)); }
        }

        /// <summary>
        /// Text representation of the underlying bytes.
        /// </summary>
        public string TextValue
        {
            get { return _asText ?? (_asText = Util.BytesToTextString(Bytes, HashingHelper.DefaultEncoding)); }
        }

        internal string As(StringFormat stringFormat)
        {
            return Util.BytesToString(Bytes, stringFormat, HashingHelper.DefaultEncoding);
        }
    }
}