using System;
using System.Linq;
using System.Text.RegularExpressions;
using log4net;
using ML.Common;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web;

namespace ML.Utils.IntegrityDirect
{
	public class IntegrityDirectManager
	{
		private readonly ILog _log = LogManager.GetLogger(typeof(IntegrityDirectManager));

		private readonly IntegrityDirectSetting _setting;

		public IntegrityDirectManager(IntegrityDirectSetting setting)
		{
			_setting = setting;

			if (!_setting.IsValid() && _setting.ThrowExceptionIfError)
			{
				throw new IntegrityDirectException("IntegrityDirect setting is invalid");
			}
		}

		#region Verify Age

		public AgeVerifyResponse Verify(AgeVerifyRequest request)
		{
			return new AgeVerifyResponse();
		}

		#endregion

		#region Identity Match

		public IdentityMatchResponse Match(IdentityMatchRequest request)
		{
			try
			{
				if (request == null)
				{
					throw new Exception("VerificationRequest is invalid.");
				}

				var postData = BuildPostData(request);

				var responseAsString = HttpHelper.Post(_setting.ApiUrl, postData);

				if (string.IsNullOrWhiteSpace(responseAsString))
				{
					throw new Exception("Cannot proccess request at this time.");
				}

				var response = HttpUtility.ParseQueryString(responseAsString);

				var verifyResponse = new IdentityMatchResponse
				{
					TransactionId = response["tid"],
					MatchCode = response["mc"],
					MatchDescription = IntegrityDirectCodes.GetMatchDescription(response["mc"]),
					ErrorCode = response["err_code"],
					ErrorDescription = response["err_desc"],
				};

				verifyResponse.MatchDescriptions = GetMatchDescriptions(request, verifyResponse.MatchDescription);

				return verifyResponse;
			}
			catch (Exception ex)
			{
				if (_setting.ThrowExceptionIfError)
				{
					throw new IntegrityDirectException(ex.Message, ex);
				}

				return null;
			}
		}
		
		#endregion
		
		private byte[] BuildPostData<TRequest>(TRequest request)
		{
			var queries = new List<string>
			              {
				              string.Format("sid={0}", _setting.SiteId)
			              };

			var props = typeof(TRequest).GetProperties();

			foreach (var prop in props)
			{
				var propValue = prop.GetValue(request, null).ToStr();
				var propAttr = prop.GetCustomAttribute<QueryParamAttribute>();

				queries.Add(string.Format("{0}={1}", propAttr.Name, HttpUtility.UrlEncode(propValue)));
			}

			var queryAsString = string.Join("&", queries);

			_log.Debug("IntegrityDirect Query: " + queryAsString);

			return Encoding.UTF8.GetBytes(queryAsString);
		}

		private List<MatchDescription> GetMatchDescriptions<TRequest>(TRequest request, string verifyResponseMatchDesciption)
		{
			var matchDesciptions = new List<MatchDescription>();

			if (string.IsNullOrWhiteSpace(verifyResponseMatchDesciption))
			{
				return matchDesciptions;
			}

			var verifyResponseMatchDesciptions = SplitVerifyResponseMatchDesciption(verifyResponseMatchDesciption);

			var props = typeof(TRequest).GetProperties().Where(p => p.GetCustomAttributes<MatchDescriptionAttribute>().Any());

			foreach (var prop in props)
			{
				var aatchDescriptionAttributes = prop.GetCustomAttributes<MatchDescriptionAttribute>();

				matchDesciptions.AddRange(aatchDescriptionAttributes
					.Where(d => verifyResponseMatchDesciptions.Any(d.IsMatch))
					.Select(d => d.GetDescription(prop.GetValue(request, null)))
				);
			}

			return matchDesciptions;
		}

		private IEnumerable<string> SplitVerifyResponseMatchDesciption(string verifyResponseMatchDesciption)
		{
			var matches = Regex.Matches(verifyResponseMatchDesciption.Replace(" ", ""), @"[A-Z]\w*");

			return from Match match in matches
				   select match.Value == "DOB" ? Regex.Match(verifyResponseMatchDesciption, @"DOB\([a-zA-Z|\/]*\)").Value : match.Value;
		}
	}
}
