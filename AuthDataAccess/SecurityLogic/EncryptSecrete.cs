using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthDataAccess.SecurityLogic
{
    public static class EncryptSecrete
    {
        public static (string Salt, string Hash) HashSecrete(string MyIdentity)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            // Convert salt to hex string
            string saltHexString = BitConverter.ToString(salt).Replace("-", "").ToLower();

            using (var argon2 = new Argon2id(System.Text.Encoding.UTF8.GetBytes(MyIdentity)))
            {
                argon2.Salt = salt; // The actual salt bytes are still needed for the hash calculation
                argon2.DegreeOfParallelism = 8; // Number of cores to use
                argon2.MemorySize = 1024 * 1024; // Amount of memory to use in KB
                argon2.Iterations = 4;

                byte[] hashBytes = argon2.GetBytes(100); // Get 100-byte hash
                string hashHexString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower(); // Convert to hex (200 characters)
                return (saltHexString, hashHexString);
            }
        }

        public static bool VerifySecrete(string password, byte[] salt, string hexHash)
        {
            using (var argon2 = new Argon2id(System.Text.Encoding.UTF8.GetBytes(password)))
            {
                argon2.Salt = salt;
                argon2.DegreeOfParallelism = 8;
                argon2.MemorySize = 1024 * 1024;
                argon2.Iterations = 4;

                // Assuming you're generating a 100-byte hash for the 200-character hex string now
                byte[] testHash = argon2.GetBytes(100); // Get 100-byte hash to match the new hash size

      
                byte[] originalHash = HexStringToByteArray(hexHash);

                return CompareByteArrays(originalHash, testHash);
            }
        }

        private static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            uint diff = (uint)array1.Length ^ (uint)array2.Length;
            for (int i = 0; i < array1.Length && i < array2.Length; i++)
            {
                diff |= (uint)(array1[i] ^ array2[i]);
            }
            return diff == 0;
        }
        private static byte[] HexStringToByteArray(string hexString)
        {
            int numberChars = hexString.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            return bytes;
        }
    }
}
