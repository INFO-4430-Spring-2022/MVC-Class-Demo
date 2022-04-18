using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace Tools {
    public static class Hasher {

        public static string HashIt(string input) {
            string hash = input;
            // create parts to hash
            byte[] hashParts = System.Text.Encoding.UTF8.GetBytes(hash);
            byte[] hashDone;
            // hash up the parts - SHA1 is weak, use something stronger. 
            hashDone = SHA1.HashData(hashParts);
            string hashedString = DataConverter.ToHex(hashDone);
            return hashedString;
        }

        public static string HashIt(string input, string salt, int stretches) {
            // add data (salt) to input
            string hashWithSalt = input + salt;
            string strongerHash = hashWithSalt;
            // keep hashing (stretching)
            for (int i = 0; i < stretches; i++) {
                strongerHash = HashIt(strongerHash);
            }
            return strongerHash;
        }

        public static string HashIt(string input, string salt, int stretches,
                                        int lengthOfHash) {
            // convert salt
            byte[] saltBytes = Convert.FromBase64String(salt);
            // create hash from input, salt, and stretches; use SHA 256 hash.
            // get back specified length of hash.
            byte[] bestHash = KeyDerivation.Pbkdf2(input, saltBytes, KeyDerivationPrf.HMACSHA256,
                                                    stretches, lengthOfHash);
            // convert to base-64 to easier storage (like in database). 
            return Convert.ToBase64String(bestHash);
        }

        public static string GetSalt() {
            string newSalt;
            byte[] saltArray = new byte[144 / 8]; // 144 bit salt (8 bits : 1 Byte) 
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(saltArray); // fill salt array with random bits.
            return Convert.ToBase64String(saltArray); // 144 bits -> 18 bytes -> 24 chars
        }
    }
}