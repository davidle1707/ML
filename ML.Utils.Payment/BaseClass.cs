using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ML.Utils.Payment
{
    public class BaseClass
    {
        public string getStrError(object responseCode, object responseText)
        {
            return "\n <b>Response Reason Code:</b> " + getValue(responseCode) + ". \n <b>Response Reason Text:</b> " + getValue(responseText) + ".";
            ;
        }

        public string getValue(object str)
        {
            try
            {
                if (str != null)
                    return str.ToString();
                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public string GetUniqueKey(int maxSize = 18)
        {
            char[] chars = "1234567890".ToCharArray();
            var data = new byte[1];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(maxSize);
            foreach (var b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
