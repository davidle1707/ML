using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MLXamarin.Common.Shared
{
    public class SharedCryptoHelper
    {
        public string Encrypt(string source, string key, string saltKey = "")
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            var outStr = source;                       // Encrypted string to return
            RijndaelManaged aesAlg = null;              // RijndaelManaged object used to encrypt the data.

            try
            {
                // generate the key from the shared secret and the salt
                var rfc2898DeriveBytes = new Rfc2898DeriveBytes(key, CreateSalt(saltKey));

                // Create a RijndaelManaged object
                // with the specified key and IV.
                aesAlg = new RijndaelManaged();
                aesAlg.Key = rfc2898DeriveBytes.GetBytes(aesAlg.KeySize / 8);
                aesAlg.IV = rfc2898DeriveBytes.GetBytes(aesAlg.BlockSize / 8);

                // Create a decrytor to perform the stream transform.
                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(source);
                        }
                    }
                    outStr = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                aesAlg?.Clear();
            }

            // Return the encrypted bytes from the memory stream.
            return outStr;
        }

        public string Decrypt(string encrypt, string key, string saltKey = "")
        {
            if (string.IsNullOrEmpty(encrypt))
                throw new ArgumentNullException(nameof(encrypt));
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            // Declare the RijndaelManaged object
            // used to decrypt the data.
            RijndaelManaged aesAlg = null;

            // Declare the string used to hold
            // the decrypted text.
            var plaintext = encrypt;

            try
            {
                // generate the key from the shared secret and the salt
                var rfc2898DeriveBytes = new Rfc2898DeriveBytes(key, CreateSalt(saltKey));

                // Create a RijndaelManaged object
                // with the specified key and IV.
                aesAlg = new RijndaelManaged();
                aesAlg.Key = rfc2898DeriveBytes.GetBytes(aesAlg.KeySize / 8);
                aesAlg.IV = rfc2898DeriveBytes.GetBytes(aesAlg.BlockSize / 8);

                // Create a decrytor to perform the stream transform.
                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                // Create the streams used for decryption.                
                var bytes = Convert.FromBase64String(encrypt);
                using (var msDecrypt = new MemoryStream(bytes))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                aesAlg?.Clear();
            }

            return plaintext;
        }

        private byte[] CreateSalt(string saltKey)
        {
            return Encoding.ASCII.GetBytes(!string.IsNullOrEmpty(saltKey) ? saltKey : "h9>72r^?,9Sl0|41");
        }
    }
}
