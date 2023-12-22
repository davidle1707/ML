using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Utils.Phone.Vendors.Plivo
{
	public class PlivoException : Exception
	{
		public PlivoException(string message) : base(message) { }
	}
}
