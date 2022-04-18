
using System;
using System.Text;

namespace Tools {
    public static class DataConverter {
        // https://stackoverflow.com/questions/311165/how-do-you-convert-a-byte-array-to-a-hexadecimal-string-and-vice-versa

        /// <summary>
        /// Converts the bytes into an easier readable format. (Hex Values)
        /// </summary>
        /// <param name="encryptedBytes">Values of encrypted data.</param>
        /// <returns>Hexadecimal equivalent of encrypted byte data.</returns>
        public static string ToHex(byte[] encryptedBytes) {
            StringBuilder retString = new StringBuilder();
            foreach (byte byt in encryptedBytes) {
                // format into hex pairs 00 - FF
                retString.Append(byt.ToString("x2"));
            }
            return retString.ToString();

        }
        /// <summary>
        /// Converts the Hex Values string back into a byte array
        /// </summary>
        /// <param name="encryptedString">Hex string of values.</param>
        /// <returns>Byte array representation of hex string.</returns>

        public static byte[] FromHex(string encryptedString) {
            //encryptedString = encryptedString.ToUpper();        //encryptedString = encryptedString.ToUpper();
            byte[] encryptedBytes = new byte[encryptedString.Length / 2];
            if (encryptedString.Length % 2 == 0) {
                // hex values need to be in paired values 00 - FF
                for (int i = 0; i < encryptedString.Length; i += 2) {
                    string hexChars = encryptedString.Substring(i, 2);
                    encryptedBytes[i / 2] = Byte.Parse(hexChars, System.Globalization.NumberStyles.HexNumber);
                }
            } else {
                // missing values in hex string
            }
            return encryptedBytes;

        }
    }
}