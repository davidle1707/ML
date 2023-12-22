using System.Collections.Generic;

namespace ML.Utils.Phone.Vendors.Plivo.XmlElements
{
	public class Wait : PlivoElement
	{
		public Wait(Dictionary<string, string> attributes)
			: base(attributes)
		{
			Nestables = new List<string>() { "" };
			ValidAttributes = new List<string>()
			                  {   "length", "silence","minSilence"
			                  };
			addAttributes();
		}
	}
}