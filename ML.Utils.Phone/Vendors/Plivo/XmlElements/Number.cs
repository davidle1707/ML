using System.Collections.Generic;

namespace ML.Utils.Phone.Vendors.Plivo.XmlElements
{
	public class Number : PlivoElement
	{
		public Number(string body, Dictionary<string, string> parameters)
			: base(body, parameters)
		{
			Nestables = new List<string>() { "" };
			ValidAttributes = new List<string>()
			                  {   "sendDigits", "sendOnPreAnswer", "sendDigitsMode"
			                  };
			addAttributes();
		}
	}
}