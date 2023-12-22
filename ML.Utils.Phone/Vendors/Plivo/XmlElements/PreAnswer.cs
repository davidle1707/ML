using System.Collections.Generic;

namespace ML.Utils.Phone.Vendors.Plivo.XmlElements
{
	public class PreAnswer : PlivoElement
	{
		public PreAnswer()
			: base()
		{
			Nestables = new List<string>()
			            {   "Play", "Speak", "GetDigits", "Wait", "Redirect", "Message", "DTMF"
			            };
			ValidAttributes = new List<string>() { "" };
		}
	}
}