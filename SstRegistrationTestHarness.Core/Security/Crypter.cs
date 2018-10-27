using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SstRegistrationTestHarness.Core.Security
{
    public static class Crypter
    {
        private static readonly byte[] RgbIv = { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9, 0x5, 0x46, 0x9c, 0xea, 0xa8, 0x4b, 0x73, 0xcc };

        public static string HashMd5(string stringToEncrypt)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(stringToEncrypt);
            var hash = md5.ComputeHash(inputBytes);

            var stringBuilder = new StringBuilder();
            foreach (var t in hash)
            {
                stringBuilder.Append(t.ToString("x2"));
            }

            return stringBuilder.ToString();
        }
        
        public static string EncryptString(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var keyBytes = CreateRgbKey();

            using (var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC })
            {
                var encryptor = symmetricKey.CreateEncryptor(keyBytes, RgbIv);
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        var cipherTextBytes = memoryStream.ToArray();

                        return Convert.ToBase64String(cipherTextBytes);
                    }
                }
            }
        }

        public static string DecryptString(string cipherText)
        {
            var cipherTextBytes = Convert.FromBase64String(cipherText);
            var keyBytes = CreateRgbKey();

            using (var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC })
            {
                var decryptor = symmetricKey.CreateDecryptor(keyBytes, RgbIv);
                using (var memoryStream = new MemoryStream(cipherTextBytes))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        var plainTextBytes = new byte[cipherTextBytes.Length];
                        var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                    }
                }
            }
        }

        private static byte[] CreateRgbKey()
        {
            var salt = new byte[] { 0xc, 0x2d, 0x13, 0x2e, 0x15, 0xb6, 0xcd, 0xf9, 0x5, 0x46, 0x9c, 0xef, 0xb3, 0x7c, 0x10, 0xff };
            var password = new Rfc2898DeriveBytes("wqdj~yriu!@*k0_^fa7431%p$#=@hd+&", salt);
            return password.GetBytes(256 / 8);
        }
    }
}
