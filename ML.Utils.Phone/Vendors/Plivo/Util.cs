using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using ML.Common;
using dict = System.Collections.Generic.Dictionary<string, string>;

namespace ML.Utils.Phone.Vendors.Plivo
{
	class Util
    {
        public static string ToPhoneNumber(string source)
        {
            var sphone = Regex.Replace(source.ToStr(), "[^0-9]", "");

            if (!sphone.StartsWith("1")) 
                sphone = "1" + sphone;

            return sphone.PadLeft(11).Trim();
        }

		public static string HtmlConvert(string inputText)
		{
			var builder = new StringBuilder();
			foreach (var c in inputText)
			{
				if ((int)c > 127)
				{
					builder.Append("&#");
					builder.Append((int)c);
					builder.Append(";");
				}
				else
				{
					builder.Append(c);
				}
			}
			return builder.ToString();
		}

		public static dict GetParams<TRequest>(TRequest request)
		{
			var dict = new dict();

			if (request != null)
			{
				var props = typeof(TRequest).GetProperties();

				foreach (var prop in props)
				{
					var propValue = prop.GetValue(request, null);

					if (propValue != null)
					{
						dict.Add(prop.Name, propValue.ToStr());	
					}
				}
			}

			return dict;
		}

		public static bool VerifyXPlivoSignature(string uri, dict plivoHttpParams, string xPlivoSignature, string authToken)
		{
			var isMatch = false;
			foreach (var kvp in plivoHttpParams.OrderBy(key => key.Key))
				uri += kvp.Key + kvp.Value;

			var enc = Encoding.ASCII;
			var myhmacsha1 = new HMACSHA1(enc.GetBytes(authToken));
			var byteArray = Encoding.ASCII.GetBytes(uri);
			var stream = new MemoryStream(byteArray);
			var hashValue = myhmacsha1.ComputeHash(stream);
			var generatedSignature = Convert.ToBase64String(hashValue);

			if (xPlivoSignature.Equals(generatedSignature))
				isMatch = true;
			return isMatch;
		}
	}
}
