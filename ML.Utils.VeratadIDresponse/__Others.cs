using ML.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ML.Utils.VeratadIDresponse
{
	public enum ResultState : short
	{
		Error = 0,

		Pass,

		Fail,

		Review
	}

	internal static class Std
	{
		public static ResultState ToResultState(this string source)
		{
			switch (source.ToStr().ToUpper())
			{
				case "PASS":
					return ResultState.Pass;

				case "FAIL":
					return ResultState.Fail;

				case "REVIEW":
					return ResultState.Review;
			}

			return ResultState.Error;
		}

		public static JObject DeserializeJson(this string json)
		{
			return (JObject)JsonConvert.DeserializeObject(json, new JsonSerializerSettings
			{
				Error = (sender, args) => args.ErrorContext.Handled = true //ignore error
			});
		}
	}
}
