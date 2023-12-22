using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ML.Common
{
	public static class HttpHelper
	{
		#region Post & Get

		public static string Post(string url, string data, string contentType = "application/json; charset=UTF-8", bool throwExceptionIfError = false)
		{
			var postData = Encoding.UTF8.GetBytes(data);

			return Post(url, postData, contentType, throwExceptionIfError);
		}

		public static string Post(string url, Dictionary<string, string> @params, bool urlEncode, string contentType = "application/x-www-form-urlencoded", bool throwExceptionIfError = false)
		{
			var query = BuildQueryString(@params, urlEncode);

			var postData = Encoding.UTF8.GetBytes(query);

			return Post(url, postData, contentType, throwExceptionIfError);
		}

		public static string Post(string url, byte[] data, string contentType = "application/x-www-form-urlencoded", bool throwExceptionIfError = false)
		{
			try
			{
				var request = (HttpWebRequest)WebRequest.Create(url);
				request.Method = "POST";
				request.ContentType = contentType;
				request.ContentLength = data.Length;

				var requestStream = request.GetRequestStream();
				requestStream.Write(data, 0, data.Length);
				requestStream.Close();

				var response = (HttpWebResponse)request.GetResponse();

				return ReadResponse(response);
			}
			catch (Exception)
			{
				if (throwExceptionIfError)
				{
					throw;
				}
			}

			return null;
		}

		public static string Get(string url, Dictionary<string, string> @params = null, bool urlEncode = true, bool throwExceptionIfError = false)
		{
			try
			{
				var uri = new UriBuilder(url);

				if (@params != null)
				{
					uri.Query = BuildQueryString(@params, urlEncode);
				}

				var request = (HttpWebRequest)WebRequest.Create(uri.ToString());
				var response = (HttpWebResponse)request.GetResponse();

				return ReadResponse(response);
			}
			catch (Exception)
			{
				if (throwExceptionIfError)
				{
					throw;
				}
			}

			return null;
		}

        public static async Task<string> GetAsync(string url, Dictionary<string, string> @params = null, bool urlEncode = true, bool throwExceptionIfError = false)
        {
            try
            {
                var uri = new UriBuilder(url);

                if (@params != null)
                {
                    uri.Query = BuildQueryString(@params, urlEncode);
                }

                using (var client = new WebClient())
                {
                    return await client.DownloadStringTaskAsync(uri.ToString());
                }
            }
            catch (Exception)
            {
                if (throwExceptionIfError)
                {
                    throw;
                }
            }

            return null;
        }

		private static string ReadResponse(HttpWebResponse response)
		{
			if (response.StatusCode != HttpStatusCode.OK)
			{
				return string.Empty;
			}

			var receiveStream = response.GetResponseStream();

			if (receiveStream == null)
			{
				return string.Empty;
			}

			var readStream = !string.IsNullOrEmpty(response.CharacterSet)
				? new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet))
				: new StreamReader(receiveStream);

			var data = readStream.ReadToEnd();

			response.Close();
			readStream.Close();

			return data;
		}

		private static string BuildQueryString(Dictionary<string, string> @params, bool urlEncode = true)
		{
			return string.Join("&", @params.Select(param => string.Format("{0}={1}", param.Key, urlEncode ? HttpUtility.UrlEncode(param.Value) : param.Value)));
		}

		#endregion

		#region Post Files

		// Implements multipart/form-data POST in C# http://www.ietf.org/rfc/rfc2388.txt
		// http://www.briangrinstead.com/blog/multipart-form-post-in-c

		public static string PostMultiPartForm(string url, Dictionary<string, string> postValues, Dictionary<string, List<PostFileInfo>> postFiles = null, bool throwExceptionIfError = false)
		{
			try
			{
				var formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());
				var contentType = "multipart/form-data; boundary=" + formDataBoundary;

				var request = WebRequest.Create(url) as HttpWebRequest;

				if (request == null)
				{
					throw new NullReferenceException("request is not a http request");
				}

				var formData = GetMultipartFormData(formDataBoundary, postValues, postFiles);

				request.Method = "POST";
				request.ContentType = contentType;
				request.ContentLength = formData.Length;
				request.KeepAlive = true;
				request.Credentials = CredentialCache.DefaultCredentials;
				//request.UserAgent = userAgent;
				//request.CookieContainer = new CookieContainer();

				// You could add authentication here as well if needed:
				// request.PreAuthenticate = true;
				// request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
				// request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes("username" + ":" + "password")));

				// Send the form data to the request.
				using (var requestStream = request.GetRequestStream())
				{
					requestStream.Write(formData, 0, formData.Length);
					requestStream.Close();
				}

				var response = request.GetResponse() as HttpWebResponse;

				return ReadResponse(response);
			}
			catch (Exception)
			{
				if (throwExceptionIfError)
				{
					throw;
				}
			}

			return null;
		}

		private static byte[] GetMultipartFormData(string boundary, Dictionary<string, string> postValues, Dictionary<string, List<PostFileInfo>> postFiles = null)
		{
			var formDataStream = new MemoryStream();
			var needsClrf = false;

			var encoding = Encoding.UTF8;

			foreach (var param in postValues)
			{
				// Thanks to feedback from commenters, add a CRLF to allow multiple parameters to be added.
				// Skip it on the first parameter, add it to subsequent parameters.
				if (needsClrf)
				{
					formDataStream.Write(encoding.GetBytes("\r\n"), 0, encoding.GetByteCount("\r\n"));
				}

				needsClrf = true;

				var postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}", boundary, param.Key, HttpUtility.UrlEncode(param.Value));

				formDataStream.Write(encoding.GetBytes(postData), 0, encoding.GetByteCount(postData));
			}

			if (postFiles != null)
			{
				needsClrf = postValues.Count > 0;

				foreach (var postFile in postFiles.Where(pf => pf.Value != null && pf.Value.Count > 0))
				{
					// Add just the first part of this param, since we will write the file data directly to the Stream
					var formatPostFileHeader = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\";", boundary, postFile.Key) + " filename=\"{0}\";\r\nContent-Type: {1}\r\n\r\n";

					foreach (var file in postFile.Value)
					{
						// Skip it on the first parameter, add it to subsequent parameters.
						if (needsClrf)
						{
							formDataStream.Write(encoding.GetBytes("\r\n"), 0, encoding.GetByteCount("\r\n"));
						}

						needsClrf = true;

						var postHeader = string.Format(formatPostFileHeader, file.FileName, file.ContentType);
						formDataStream.Write(encoding.GetBytes(postHeader), 0, encoding.GetByteCount(postHeader));

						// Write the file data directly to the Stream, rather than serializing it to a string.
						formDataStream.Write(file.Contents, 0, file.Contents.Length);
					}
				}
			}

			// Add the end of the request.  Start with a newline
			var footer = "\r\n--" + boundary + "--\r\n";
			formDataStream.Write(encoding.GetBytes(footer), 0, encoding.GetByteCount(footer));

			// Dump the Stream into a byte[]
			formDataStream.Position = 0;
			var formData = new byte[formDataStream.Length];
			formDataStream.Read(formData, 0, formData.Length);
			formDataStream.Close();

			return formData;
		}
		
		[Serializable]
		public class PostFileInfo
		{
			public byte[] Contents { get; private set; }
			public string FileName { get; private set; }
			public string ContentType { get; private set; }

			public PostFileInfo(byte[] contents) : this(contents, null) { }
			public PostFileInfo(byte[] contents, string fileName) : this(contents, fileName, null) { }
			public PostFileInfo(byte[] contents, string fileName, string contentType)
			{
				Contents = contents;
				FileName = !string.IsNullOrWhiteSpace(fileName) ? fileName : Path.GetRandomFileName();
				ContentType = contentType ?? "application/octet-stream";
			}
		}

		#endregion
	}
}
