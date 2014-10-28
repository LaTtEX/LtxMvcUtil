using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LtxMvcUtil.Security
{
    /// <summary>
    /// Helper class providing miscellaneous encryption and hashing methods
    /// </summary>
    public static class SecurityHelper
    {
        private static readonly char[] AvailableCharacters =
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };

        /// <summary>
        /// Hashes a string based on a SHA256 algorithm
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ComputeHash(this string input)
        {
            HashAlgorithm hashAlgorithm = SHA256.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = hashAlgorithm.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Generates a random string based on the RNGCryptoServiceProvider
        /// </summary>
        /// <param name="length">Length of the required random string</param>
        /// <returns></returns>
        public static string GenerateRandomString(int length)
        {
            var identifier = new char[length];
            var randomData = new byte[length];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomData);
            }

            for (int idx = 0; idx < identifier.Length; idx++)
            {
                int pos = randomData[idx] % AvailableCharacters.Length;
                identifier[idx] = AvailableCharacters[pos];
            }

            return new string(identifier);
        }
    }
}
