using System.Security.Cryptography;

namespace WarStreamer.Commons.Tools
{
    public class AesEncryption(string key)
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly string _key = key;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public string Decrypt(string cipherText, string initializationVector)
        {
            using Aes aes = Aes.Create();

            aes.Key = Convert.FromBase64String(_key);
            aes.IV = Convert.FromBase64String(initializationVector);

            ICryptoTransform decryptor = aes.CreateDecryptor();

            byte[] cipher = Convert.FromBase64String(cipherText);

            using MemoryStream msDecrypt = new(cipher);
            using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new(csDecrypt);

            return srDecrypt.ReadToEnd();
        }

        public string Encrypt(string plainText, out string initializationVector)
        {
            using Aes aes = Aes.Create();

            aes.Key = Convert.FromBase64String(_key);
            aes.GenerateIV();

            ICryptoTransform encryptor = aes.CreateEncryptor();

            byte[] encryptedData = [];

            using (MemoryStream msEncrypt = new())
            {
                using CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write);
                using (StreamWriter swEncrypt = new(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }

                encryptedData = msEncrypt.ToArray();
            }

            initializationVector = Convert.ToBase64String(aes.IV);
            return Convert.ToBase64String(encryptedData);
        }
    }
}
