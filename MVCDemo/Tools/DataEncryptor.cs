using System;
using System.IO;
using System.Security.Cryptography;

namespace Tools {
    /// <summary>
    /// Encrypts and Decripts text strings into Hex strings of encrypted values.
    /// </summary>
    public class DataEncryptor {
        // Built based off of the code on the page.
        // https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-6.0

        // Key and IV needed to for encryption and decryption.
        // these need to be set at the start of the app. 
        // If these change the encryption and decryptions will not match. 
        private static byte[] _Key; // needs to be a length of 32.
        private static byte[] _IV; // needs to be a length of 16 bytes or 128 bits.

        /// <summary>
        /// Encrypts data using a static Key and IV.
        /// </summary>
        /// <param name="data">Data to encrypt</param>
        /// <returns>Base-64 equivalent of encrypted data.</returns>
        /// <remarks>Use SetKey method to set Key and IV from consistent location like program start class.</remarks>
        public static string Protect(string data) {
            // parameter and field checking.
            if (data == null) {
                throw new ArgumentNullException("data");
            } else if (data.Length == 0) {
                throw new ArgumentException("data");
            } else if (_Key == null) {
                throw new MissingMemberException("Key not set at Startup.");
            } else if (_IV == null) {
                throw new MissingMemberException("IV not set at Startup.");
            }
            byte[] encryptedBytes;
            Aes crypt = Aes.Create();
            crypt.Key = _Key;
            crypt.IV = _IV;
            ICryptoTransform cryptoTransform = crypt.CreateEncryptor(crypt.Key, crypt.IV);
            // create stream to hold encrypted data
            using (MemoryStream msEncrypting = new MemoryStream()) {
                // create stream to write encrypted results to
                using (CryptoStream crypStream =
                    new CryptoStream(msEncrypting, cryptoTransform, CryptoStreamMode.Write)) {
                    // create stream to write encrypted data into
                    using (StreamWriter swEncrypting = new StreamWriter(crypStream)) {
                        // write the data into the encryption 
                        swEncrypting.Write(data);
                    }
                    // store the encrypted results out for use.
                    encryptedBytes = msEncrypting.ToArray();
                }
            }
            return Convert.ToBase64String(encryptedBytes); //DataConverter.ToHex(encryptedBytes);
        }

        /// <summary>
        /// Decrypts data using a static Key and IV.
        /// </summary>
        /// <param name="data">Encrypted data to decrypt.</param>
        /// <returns>String equivalent of decrypted data.</returns>
        /// <remarks>Use SetKey method to set Key and IV from consistent location like program start class.</remarks>
        public static string Unprotect(string data) {
            // parameter and field checking
            if (data == null) {
                throw new ArgumentNullException("data");
            } else if (data.Length == 0) {
                throw new ArgumentException("data");
            } else if (_Key == null) {
                throw new MissingMemberException("Key not set at Startup.");
            } else if (_IV == null) {
                throw new MissingMemberException("IV not set at Startup.");
            }
            byte[] encryptedBytes = Convert.FromBase64String(data); //DataConverter.FromHex(data);
            string decryptedData;
            Aes decrypt = Aes.Create();
            // set key and iv
            decrypt.Key = _Key;
            decrypt.IV = _IV;
            ICryptoTransform cryptoTransform = decrypt.CreateDecryptor(decrypt.Key, decrypt.IV);
            // create stream from data
            using (MemoryStream msDecrypting = new MemoryStream(encryptedBytes)) {
                // create stream to read decryption
                using (CryptoStream decryptStream =
                    new CryptoStream(msDecrypting, cryptoTransform, CryptoStreamMode.Read)) {
                    // read decryption string
                    using (StreamReader srDecryption = new StreamReader(decryptStream)) {
                        // set text to decripted answer.
                        decryptedData = srDecryption.ReadToEnd();
                    }
                }
            }
            return decryptedData;
        }

        /// <summary>
        /// Set key and iv from external source
        /// </summary>
        /// <param name="key">Key for Encryption (should be 32 characters | 256 bits) </param>
        /// <param name="iv">Initial Vector (should be 16 characters | 128 bits)</param>
        public static void SetKey(string key, string iv) {
            if (key == null) throw new ArgumentNullException("key");
            if (key.Length == 0) throw new ArgumentException("key");
            if (iv == null) throw new ArgumentNullException("iv");
            if (iv.Length == 0) throw new ArgumentException("iv");
            if (key.Length % 8 != 0) throw new ArgumentException("hmm");

            _Key = System.Text.Encoding.UTF8.GetBytes(key);
            _IV = System.Text.Encoding.UTF8.GetBytes(iv);
        }

        /// <summary>
        /// Set key and iv from external source using Base-64 strings.
        /// </summary>
        /// <param name="key">Base-64 Key for Encryption (should be 32 characters | 256 bits)</param>
        /// <param name="iv">Base-64 Initial Vector (should be 24 characters | 128 bits)</param>
        public static void SetKeyBase64(string key, string iv) {
            if (key == null) throw new ArgumentNullException("key");
            if (key.Length == 0) throw new ArgumentException("key");
            if (iv == null) throw new ArgumentNullException("iv");
            if (iv.Length == 0) throw new ArgumentException("iv");

            // must be at least 44 characters, but that is 33 bytes. 
            // IV can only be 32 Bytes.
            while (key.Length < 44) {  // make sure at least 44 characters
                key = key + key;
            }
            if (key.Length % 44 != 0) key = key.Substring(0, 44);

            byte[] tempKey = Convert.FromBase64String(key);

            // must be at least 24 characters, but that is 18 bytes. 
            // IV can only be 16 Bytes.
            while (iv.Length < 24) {  // make sure at least 24 characters
                iv = iv + iv;
            }
            if (iv.Length % 24 != 0) iv = iv.Substring(0, 24);

            byte[] tempIV = Convert.FromBase64String(iv);
            _Key = new byte[32];
            _IV = new byte[16];
            Array.Copy(tempKey, _Key, 32); // force to be 32 bytes
            Array.Copy(tempIV, _IV, 16); // force to be 16 bytes
        }

        internal static Rfc2898DeriveBytes GetKey(string input, string salt, int stretches,
                                        int lengthOfHash) {
            // convert salt
            byte[] saltBytes = System.Text.Encoding.UTF8.GetBytes(salt);
            // create hasher from input, salt, and stretches
            Rfc2898DeriveBytes rfcHasher;
            rfcHasher = new Rfc2898DeriveBytes(input, saltBytes, stretches);
            return rfcHasher;
        }

    }

}