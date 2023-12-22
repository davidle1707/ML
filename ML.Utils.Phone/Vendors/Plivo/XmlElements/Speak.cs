using System.Collections.Generic;

namespace ML.Utils.Phone.Vendors.Plivo.XmlElements
{
	public class Speak : PlivoElement
	{
		public Speak(string body, Dictionary<string, string> attributes)
			: base(body, attributes)
		{
			Nestables = new List<string>() { "" };
			ValidAttributes = new List<string>()
			                  {   "loop", "language", "voice"
			                  };
			addAttributes();
		}
	}
}