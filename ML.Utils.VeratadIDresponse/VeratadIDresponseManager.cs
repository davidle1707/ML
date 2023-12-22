using System;
using log4net;
using ML.Common;
using Newtonsoft.Json;

namespace ML.Utils.VeratadIDresponse
{
	public class VeratadIDresponseManager
	{
		private readonly ILog _log = LogManager.GetLogger(typeof(VeratadIDresponseManager));

		private readonly VeratadIDresponseSetting _setting;

		public VeratadIDresponseManager(VeratadIDresponseSetting setting)
		{
			_setting = setting;

			if (!_setting.IsValid() && _setting.ThrowExceptionIfError)
			{
				throw new VeratadIDresponseException("VeratadIDresponse setting is invalid");
			}
		}

		public AgeVerifyResponse Verify(AgeVerifyRequest request)
		{
			var response = new AgeVerifyResponse();

			try
			{
				if (request == null)
				{
					throw new Exception("Verification Request is invalid.");
				}

				_setting.ServiceName = "AgeMatch5.0";

				var postData = BuildPostDataAsJson(request);

				var apiResponseAsJson = HttpHelper.Post(_setting.ApiUrl, postData);

				response = ProcessResponse<AgeVerifyResponse>(apiResponseAsJson);
				
			}
			catch (Exception ex)
			{
				if (_setting.ThrowExceptionIfError)
				{
					throw new VeratadIDresponseException(ex.Message, ex);
				}

				response.State = ResultState.Error;
			}

			return response;
		}

		private string BuildPostDataAsJson<T>(T request)
		{
			var dynamic = new
			{
				user = _setting.UserName,
				pass = _setting.Password,
				service = _setting.ServiceName,
				reference = CombGuid.New.ToString(),
				target = request
			};

			return JsonConvert.SerializeObject(dynamic);
		}

		private T ProcessResponse<T>(string apiResponseAsJson) where T : BaseResponse
		{
			var response = Activator.CreateInstance<T>();

			if (string.IsNullOrWhiteSpace(apiResponseAsJson))
			{
				throw new Exception("Cannot proccess request at this time.");
			}

			var responseAsJson = apiResponseAsJson.DeserializeJson();

			if (responseAsJson == null)
			{
				throw new Exception("Cannot proccess request at this time.");
			}

			//meta
			var meta = responseAsJson["meta"];
			response.TransactionId = meta["reference"].ToString();
			response.TransactionDate = meta["timestamp"].ToString().ToDateTimeNull() ?? DateTime.Now;

			//result
			var result = responseAsJson["result"];
			response.State = result["action"].ToString().ToResultState();
			response.Description = result["detail"].ToStr();

			//do something in here

			return response;
		}
	}
}
