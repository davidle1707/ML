using System.Collections.Generic;

namespace ML.Utils.Phone.Vendors.Plivo.XmlElements
{
	public class Dial : PlivoElement
	{
		public Dial(Dictionary<string, string> parameters)
			: base(parameters)
		{
			Nestables = new List<string>()
			            {   "Number", "User"
			            };
			ValidAttributes = new List<string>()
			                  {   "action", "method", "hangupOnStar", "timeLimit", "timeout", "callerId",
			                      "callerName", "confirmSound", "confirmKey", "dialMusic", "callbackUrl",
			                      "callbackMethod", "redirect", "digitsMatch", "sipHeaders"
			                  };
			addAttributes();
		}
	}
}