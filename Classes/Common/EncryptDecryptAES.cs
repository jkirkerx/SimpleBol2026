using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace SimpleBol.Classes.Common
{
    public class EncryptDecryptAes
    {
        private const int SaltSize = 8;
        private const int keySize = 64;
        private const int iterations = 350000;
        private static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public static byte[] GetRandomBytes()
        {
            var ba = new byte[SaltSize];
            RandomNumberGenerator.Create().GetBytes(ba);
            return ba;
        }

        public static byte[] CreateSecret()
        {
            var ba = new byte[SaltSize];
            RandomNumberGenerator.Create().GetBytes(ba);
            return ba;
        }

        public static string EncryptText(string input, byte[] passwordBytes)
        {
            // Get the bytes of the string
            var bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            var bytesEncrypted = AesEncrypt(bytesToBeEncrypted, passwordBytes);
            var result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }

        public static string DecryptText(string input, byte[] passwordBytes)
        {
            // Get the bytes of the string
            var bytesToBeDecrypted = Convert.FromBase64String(input);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            var bytesDecrypted = AesDecrypt(bytesToBeDecrypted, passwordBytes);
            var result = Encoding.UTF8.GetString(bytesDecrypted);

            return result;
        }

        private static byte[] AesEncrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            var saltBytes = new byte[] { 115, 251, 109, 44, 2, 153, 246, 76 };

            using var ms = new MemoryStream();
            using (var aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.BlockSize = 128;

                var key = Rfc2898DeriveBytes.Pbkdf2(passwordBytes, saltBytes, 1000, HashAlgorithmName.SHA1, (aes.KeySize + aes.BlockSize) / 8);
                aes.Key = key[..(aes.KeySize / 8)];
                aes.IV = key[(aes.KeySize / 8)..];

                aes.Mode = CipherMode.CBC;

                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                }
                encryptedBytes = ms.ToArray();
            }
            return encryptedBytes;
        }

        private static byte[] AesDecrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            var saltBytes = new byte[] { 115, 251, 109, 44, 2, 153, 246, 76 };

            using var ms = new MemoryStream();
            using (var aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.BlockSize = 128;

                var key = Rfc2898DeriveBytes.Pbkdf2(passwordBytes, saltBytes, 1000, HashAlgorithmName.SHA1, (aes.KeySize + aes.BlockSize) / 8);
                aes.Key = key[..(aes.KeySize / 8)];
                aes.IV = key[(aes.KeySize / 8)..];

                aes.Mode = CipherMode.CBC;

                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                }

                decryptedBytes = ms.ToArray();
            }
            return decryptedBytes;
        }

        public static string HashPasword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }

        public static bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }

        public static string CardNumberLast4(string input, byte[] secret)
        {
            var pValue = DecryptText(input, secret);
            pValue = pValue.Substring(pValue.Length - 4);

            return pValue;
        }

        public static string PrintCardNumberPlaceholder(string p)
        {
            string pValue = String.Empty;
            switch (p)
            {
                case "VISA":
                    pValue = "****_****_****_";
                    break;

                case "MASTERCARD":
                    pValue = "****_****_****_";
                    break;

                case "DISCOVER":
                    pValue = "****_****_****_";
                    break;

                case "AMEX":
                    pValue = "****_******_*";
                    break;

                case "JCB":
                    pValue = "****_****_****_";
                    break;

                case "CUP":
                    pValue = "****_****_****_";
                    break;

                case "MAESTRO":
                    pValue = "****_****_****_";
                    break;

                case "PAYPAL":
                    pValue = "****_****_****_";
                    break;

            }

            return pValue;
        }

        public static string PrintSecurityCodePlaceholder(string p)
        {
            string pValue = String.Empty;
            switch (p)
            {
                case "VISA":
                    pValue = "3 Digits";
                    break;

                case "MASTERCARD":
                    pValue = "3 Digits";
                    break;

                case "DISCOVER":
                    pValue = "3 Digits";
                    break;

                case "AMEX":
                    pValue = "4 Digits";
                    break;

                case "JCB":
                    pValue = "3 Digits";
                    break;

                case "CUP":
                    pValue = "3 Digits";
                    break;

                case "MAESTRO":
                    pValue = "3 Digits";
                    break;

                case "PAYPAL":
                    pValue = "3 Digits";
                    break;

            }

            return pValue;
        }

        public static byte[] ComputeFileChecksum(string _appPath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(_appPath))
                {
                    return md5.ComputeHash(stream);
                }
            }
        }
    }
}
