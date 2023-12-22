using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Utils.Phone.Vendors.Tropo
{
	public class TropoException : Exception
	{
		public TropoException(string message)
			: base(message)
		{

		}
	}
}
