using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Utils.Phone.Vendors.Twilio
{
	public class TwilioException : Exception
	{
		public TwilioException(string message) : base(message)
		{
			
		}
	}
}
