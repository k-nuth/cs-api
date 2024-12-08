// Copyright (c) 2016-2024 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System.Linq;

namespace Knuth
{
    /// <summary>
    /// Helper class with utility methods
    /// </summary>
    public static class Validations
    {
        private const int HASH_HEX_LENGTH = 64;

        /// <summary>
        /// Check if the hex string represents a valid base58 address
        /// </summary>
        /// <param name="hex">Hex string</param>
        /// <returns>True iif hex is a valid base 58 address</returns>
        public static bool IsValidPaymentAddress(string hex) {
            if (string.IsNullOrWhiteSpace(hex)) {
                return false;
            }
            using (var address = new PaymentAddress(hex)) {
                return address.IsValid;
            }
        }

        /// <summary>
        /// Returns true iif hex represents a valid Bitcoin hash
        /// (i.e. 32 arbitrary bytes => 64 hex characters)
        /// </summary>
        /// <param name="hex">Hex string</param>
        /// <returns>True iif hex is a valid hash string</returns>
        public static bool IsValidHash(string hex) {
            if (string.IsNullOrWhiteSpace(hex) || hex.Length != HASH_HEX_LENGTH) {
                return false;
            }
            //Check if it is a hex string
            return hex.All("0123456789abcdefABCDEF".Contains);
        }
    }
}