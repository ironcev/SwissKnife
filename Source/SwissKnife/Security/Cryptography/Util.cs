using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwissKnife.Diagnostics.Contracts;

namespace SwissKnife.Security.Cryptography // TODO-IG: All types in this namespace are added because of an urgent need. Review and refactoring is needed. Originally developed by Marin Roncevic.
{
    internal static class Util
    {
        internal static string BytesToString(IEnumerable<byte> value, StringFormat stringFormat)
        {
            return BytesToString(value, stringFormat, Encoding.Default);
        }

        internal static string BytesToString(IEnumerable<byte> value, StringFormat stringFormat, Encoding encoding)
        {
            switch (stringFormat)
            {
                case StringFormat.Base64:
                    return BytesToBase64String(value);
                case StringFormat.Hex:
                    return BytesToHexString(value);
                default:
                    return BytesToTextString(value, encoding);
            }
        }

        internal static string BytesToHexString(IEnumerable<byte> value)
        {
            if (value == null) return null;
            
            var sb = new StringBuilder();
            
            foreach (var b in value)
            {
                sb.Append(string.Format("{0:X2}", b));
            }

            return sb.ToString();
        }

        internal static string BytesToBase64String(IEnumerable<byte> value)
        {
            return value == null ? null : Convert.ToBase64String(value.ToArray());
        }

        internal static string BytesToTextString(IEnumerable<byte> value, Encoding encoding)
        {
            Argument.IsNotNull(encoding, "encoding");

            return value == null ? null : encoding.GetString(value.ToArray());
        }

        internal static IEnumerable<byte> StringToBytes(string input, StringFormat stringFormat)
        {
            return StringToBytes(input, stringFormat, Encoding.Default);
        }

        internal static IEnumerable<byte> StringToBytes(string input, StringFormat stringFormat, Encoding encoding)
        {
            switch (stringFormat)
            {
                case StringFormat.Base64:
                    return Base64ToBytes(input);
                case StringFormat.Hex:
                    return HexToBytes(input);
                default:
                    return TextToBytes(input, encoding);
            }
        }

        internal static IEnumerable<byte> HexToBytes(string input)
        {
            if (string.IsNullOrEmpty(input)) return new byte[]{};

            var numberOfChars = input.Length;
            var bytes = new byte[numberOfChars / 2];

            for (var i = 0; i < numberOfChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(input.Substring(i, 2), 16);
            }

            return bytes;
        }

        internal static IEnumerable<byte> Base64ToBytes(string input)
        {
            return Convert.FromBase64String(input);
        }

        internal static IEnumerable<byte> TextToBytes(string input, Encoding encoding)
        {
           Argument.IsNotNull(encoding, "encoding");
            
            return encoding.GetBytes(input);
        }

        internal static bool AreBytesEqual(byte[] array1, byte[] array2)
        {
            if (array1 == array2) return true;
            if (array1 == null || array2 == null) return false;
            if (array1.Length != array2.Length) return false;
            
            return !array1.Where((t, i) => t != array2[i]).Any();
        }

        internal static IEnumerable<byte> CleanBytes(IEnumerable<byte> result)
        {
            return result.Where(b => b != 0);
        }
    }
}
