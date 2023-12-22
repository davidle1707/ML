using System.Collections.Generic;

namespace ML.Utils.Phone.Vendors.Plivo.XmlElements
{
	public class GetDigits : PlivoElement
	{
		public GetDigits(string body, Dictionary<string, string> parameters)
			: base(body, parameters)
		{
			Nestables = new List<string>()
			            {   "Speak", "Play", "Wait"
			            };
			ValidAttributes = new List<string>()
			                  {   "action", "method", "timeout", "digitTimeout","finishOnKey", "numDigits",
			                      "retries", "invalidDigitsSound", "validDigits", "playBeep", "redirect", "log"
			                  };
			addAttributes();
		}
	}
}