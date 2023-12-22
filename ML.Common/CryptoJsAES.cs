using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ML.Common
{
    // https://cryptojs.gitbook.io/docs/
    public static class CryptoJsAES
    {
        public static string Decrypt(string sourceAsBase64, string key, string iv, Setting setting = null, bool throwIfError = true)
        {
            var keys = Encoding.UTF8.GetBytes(key);
            var ivs = Encoding.UTF8.GetBytes(iv);
            return Decrypt(Convert.FromBase64String(sourceAsBase64), keys, ivs, setting, throwIfError);
        }

        public static string Decrypt(byte[] source, byte[] key, byte[] iv, Setting setting = null, bool throwIfError = true)
        {
            // Check arguments.  
            if (source == null || source.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            // Declare the string used to hold  
            // the decrypted text.  
            string plaintext = null;

            // Create an RijndaelManaged object  
            // with the specified key and IV.  
            using (var rijAlg = new RijndaelManaged())
            {
                setting ??= new Setting();
                rijAlg.Mode = setting.Mode;
                rijAlg.Padding = setting.Padding;
                rijAlg.FeedbackSize = setting.FeedbackSize;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.  
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                try
                {
                    // Create the streams used for decryption.  
                    using var msDecrypt = new MemoryStream(source);
                    using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                    using var srDecrypt = new StreamReader(csDecrypt);
                    // Read the decrypted bytes from the decrypting stream  
                    // and place them in a string.  
                    plaintext = srDecrypt.ReadToEnd();
                }
                catch (Exception ex)
                {
                    if (throwIfError) throw ex;
                }
            }

            return plaintext;
        }

        #region Encrypt

        public static string EncryptAsString(string source, string key, string iv, Setting setting = null, bool throwIfError = true)
        {
            var encrypted = Encrypt(source, key, iv, setting, throwIfError);
            return Convert.ToBase64String(encrypted);
        }

        public static byte[] Encrypt(string source, string key, string iv, Setting setting = null, bool throwIfError = true)
        {
            var keys = Encoding.UTF8.GetBytes(key);
            var ivs = Encoding.UTF8.GetBytes(iv);
            return Encrypt(source, keys, ivs, setting, throwIfError);
        }

        public static string EncryptAsString(string source, byte[] key, byte[] iv, Setting setting = null, bool throwIfError = true)
        {
            var encrypted = Encrypt(source, key, iv, setting, throwIfError);
            return Convert.ToBase64String(encrypted);
        }

        public static byte[] Encrypt(string source, byte[] key, byte[] iv, Setting setting = null, bool throwIfError = true)
        {
            // Check arguments.  
            if (source == null || source.Length <= 0)
            {
                throw new ArgumentNullException("plainText");
            }
            if (key == null || key.Length == 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length == 0)
            {
                throw new ArgumentNullException("key");
            }

            byte[] encrypted = null;
            // Create a RijndaelManaged object  
            // with the specified key and IV.  
            using (var rijAlg = new RijndaelManaged())
            {
                setting ??= new Setting();
                rijAlg.Mode = setting.Mode;
                rijAlg.Padding = setting.Padding;
                rijAlg.FeedbackSize = setting.FeedbackSize;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.  
                var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                try
                {
                    // Create the streams used for encryption.  
                    using var msEncrypt = new MemoryStream();
                    using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.  
                        swEncrypt.Write(source);
                    }
                    encrypted = msEncrypt.ToArray();
                }
                catch (Exception ex)
                {
                    if (throwIfError) throw ex;
                }
            }

            // Return the encrypted bytes from the memory stream.  
            return encrypted;
        }

        #endregion

        public class Setting
        {
            /// <summary>
            /// default: CBC
            /// </summary>
            public CipherMode Mode { get; set; } = CipherMode.CBC;

            /// <summary>
            /// default: PKCS7
            /// </summary>
            public PaddingMode Padding { get; set; } = PaddingMode.PKCS7;

            /// <summary>
            /// default: 128
            /// </summary>
            public int FeedbackSize { get; set; } = 128;
        }
    }
}
