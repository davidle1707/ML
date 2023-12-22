using System.Threading.Tasks;

namespace ML.Utils.ZipLookup
{
	public abstract class ZipLookupProvider
	{
		internal abstract ZipInfo Lookup(string zip, string country = "");

        internal abstract Task<ZipInfo> LookupAsync(string zip, string country = "");

		internal abstract ZipInfo LookupUS(string zip);
	}
}
