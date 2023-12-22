namespace MLXamarin.Common
{
    public interface ICryptoHelper
    {
        string Encrypt(string source, string key, string saltKey = "");

        string Decrypt(string encrypt, string key, string saltKey = "");
    }
}
