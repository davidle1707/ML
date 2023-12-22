using System.Collections.Generic;

namespace ML.Utils.Phone.Vendors.Plivo.XmlElements
{
	public class User : PlivoElement
	{
		public User(string body, Dictionary<string, string> parameters)
			: base(body, parameters)
		{
			Nestables = new List<string>() { "" };
			ValidAttributes = new List<string>()
			                  {   "sendDigits","sendOnPreAnswer", "sipHeaders"
			                  };
			addAttributes();
		}
	}
}