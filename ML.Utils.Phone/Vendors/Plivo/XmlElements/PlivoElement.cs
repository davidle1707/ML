using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ML.Utils.Phone.Vendors.Plivo.XmlElements
{
	public abstract class PlivoElement
	{
		protected List<string> Nestables { get; set; }
		protected List<string> ValidAttributes { get; set; }
		protected XElement Element { get; set; }
		protected Dictionary<string, string> Attributes { get; set; }

		protected PlivoElement(string body, Dictionary<string, string> attributes)
		{
			Element = new XElement(this.GetType().Name, Util.HtmlConvert(body));
			Attributes = attributes;
		}

		protected PlivoElement(Dictionary<string, string> attributes)
		{
			Element = new XElement(this.GetType().Name);
			Attributes = attributes;
		}

		protected PlivoElement(string body)
		{
			Element = new XElement(this.GetType().Name, Util.HtmlConvert(body));
		}

		protected PlivoElement()
		{
			Element = new XElement(this.GetType().Name);
		}

		protected void addAttributes()
		{
			foreach (var kvp in Attributes)
			{
				var key = kvp.Key;
				var val = kvp.Value;
				var posn = ValidAttributes.FindIndex(k => k == key);
				if (posn >= 0)
					Element.SetAttributeValue(key, _convert_values(val));
				else
					throw new PlivoException($"Invalid attribute {key} for {this.GetType().Name}");
			}
		}

		private string _convert_values(string value)
		{
			var val = "";
			switch (value.ToLower())
			{
				case "true": val = value.ToLower();
					break;
				case "false": val = value.ToLower();
					break;
				case "get": val = value.ToUpper();
					break;
				case "post": val = value.ToUpper();
					break;
				case "man": val = value.ToUpper();
					break;
				case "woman": val = value.ToUpper();
					break;
				default: val = value;
					break;
			}
			return val;
		}

		public PlivoElement Add(PlivoElement element)
		{
			var posn = Nestables.FindIndex(n => n == element.GetType().Name);
			if (posn >= 0)
			{
				Element.Add(element.Element);
				return element;
			}
			else
				throw new PlivoException($"Element {element.GetType().Name} cannot be nested within {this.GetType().Name}");
		}

		public PlivoElement AddSpeak(string body, Dictionary<string, string> parameters)
		{
			return Add(new Speak(body, parameters));
		}

		public PlivoElement AddPlay(string body, Dictionary<string, string> parameters)
		{
			return Add(new Play(body, parameters));
		}

		public PlivoElement AddGetDigits(Dictionary<string, string> parameters)
		{
			return Add(new GetDigits("", parameters));
		}

		public PlivoElement AddRecord(Dictionary<string, string> parameters)
		{
			return Add(new Record(parameters));
		}

		public PlivoElement AddDial(Dictionary<string, string> parameters)
		{
			return Add(new Dial(parameters));
		}

		public PlivoElement AddNumber(string body, Dictionary<string, string> parameters)
		{
			return Add(new Number(body, parameters));
		}

		public PlivoElement AddUser(string body, Dictionary<string, string> parameters)
		{
			return Add(new User(body, parameters));
		}

		public PlivoElement AddRedirect(string body, Dictionary<string, string> parameters)
		{
			return Add(new Redirect(body, parameters));
		}

		public PlivoElement AddWait(Dictionary<string, string> parameters)
		{
			return Add(new Wait(parameters));
		}

		public PlivoElement AddHangup(Dictionary<string, string> parameters)
		{
			return Add(new Hangup(parameters));
		}

		public PlivoElement AddPreAnswer()
		{
			return Add(new PreAnswer());
		}

		public PlivoElement AddConference(string body, Dictionary<string, string> parameters)
		{
			return Add(new Conference(body, parameters));
		}

		public PlivoElement AddMessage(string body, Dictionary<string, string> parameters)
		{
			return Add(new Message(body, parameters));
		}

		public PlivoElement AddDTMF(string body, Dictionary<string, string> attributes)
		{
			return Add(new DTMF(body, attributes));
		}

		public override string ToString()
		{
			return SerializeToXML().ToString().Replace("&amp;", "&");
		}

		protected XDocument SerializeToXML()
		{
			return new XDocument(new XDeclaration("1.0", "utf-8", "yes"), Element);
		}
	}
}