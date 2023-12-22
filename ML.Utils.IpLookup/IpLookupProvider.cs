using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Utils.IpLookup
{
	public abstract class IpLookupProvider
	{
		internal abstract IpInfo Lookup(string ip);
	}
}
