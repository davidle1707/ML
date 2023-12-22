using System.Collections.Generic;

namespace ML.Utils.Phone.Vendors.Plivo.XmlElements
{
	public class Play : PlivoElement
	{
		public Play(string body, Dictionary<string, string> attributes)
			: base(body, attributes)
		{
			Nestables = new List<string>() { "" };
			ValidAttributes = new List<string>()
			                  {   "loop"
			                  };
			addAttributes();
		}
	}
}