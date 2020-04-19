namespace Library.Helper
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class AesHandler
    {
        private const string passPhrase = ",os4GtqW4\"bPuBOb#J:-Lh,5!6j0iru6FUEQe9OU(KkHRCotW%";
        private const string saltValue = "Nl§S3(d7j6Wa)nFrKDun\"9r(ztyhIzm7iuNl.n&fi;5i#Se(Yl";
        private const string hashAlgorithm = "SHA1";
        private const string initVector = "olTA11OL-PnwOZA,";
        private const int passwordIterations = 6;
        private const int keySize = 256;

        public static string Decrypt(string cypherText, string salt)
        {
            try
            {
                string plainText = string.Empty;

                byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.UTF8.GetBytes(saltValue);
                byte[] cipherTextBytes = Convert.FromBase64String(cypherText);

                PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

                byte[] keyBytes = password.GetBytes(keySize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged
                {
                    Mode = CipherMode.CBC
                };
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

                using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                    int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                    plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                }

                return plainText.Substring(0, plainText.Length - salt.Length);
            }
            catch (Exception ex) { throw new ApplicationException($"Decrypt of value {cypherText} does not work.", ex); }
        }
        public static string Encrypt(string plainText, string salt)
        {
            try
            {
                string cypherText = string.Empty;

                byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.UTF8.GetBytes(saltValue);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText + salt);

                PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
                byte[] keyBytes = password.GetBytes(keySize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged
                {
                    Mode = CipherMode.CBC
                };
                ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
                using (MemoryStream memoryStream = new MemoryStream())
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cypherText = Convert.ToBase64String(memoryStream.ToArray());
                }

                return cypherText;
            }
            catch (Exception ex) { throw new ApplicationException($"Encrypt of plain text does not work.", ex); }
        }
    }
}