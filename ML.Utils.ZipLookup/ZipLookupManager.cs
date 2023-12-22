using ML.Common.Log;
using ML.Utils.ZipLookup.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ML.Utils.ZipLookup
{
	public class ZipLookupManager
	{
		private readonly List<ZipLookupProvider> _lookupProviders;

		public ZipLookupManager(ZipLookupProvider lookupProvider)
			: this()
		{
			_lookupProviders.Clear();
			AddProvider(lookupProvider);
		}

		/// <summary>
		/// Google provider is default
		/// </summary>
		public ZipLookupManager()
		{
			_lookupProviders = new List<ZipLookupProvider> { new GoogleZipLookupProvider() };
		}

		public void AddProvider(ZipLookupProvider lookupProvider)
		{
			if (_lookupProviders.All(p => p.GetType() != lookupProvider.GetType()))
			{
				_lookupProviders.Add(lookupProvider);
			}
		}

		public void AddAllProviders()
		{
			_lookupProviders.Clear();
			_lookupProviders.Add(new GoogleZipLookupProvider());
		}

		public ZipInfo Lookup(string zip, string country = "")
		{
			return ExecuteLookup(zip, provider => provider.Lookup(zip));
		}

        public async Task<ZipInfo> LookupAsync(string zip, string country = "")
        {
            if (_lookupProviders.Count == 0)
            {
                AddAllProviders();
            }

            foreach (var provider in _lookupProviders)
            {
                try
                {
                    var zipInfo = await provider.LookupAsync(zip, country);

                    if (zipInfo != null)
                    {
                        return zipInfo;
                    }
                }
                catch (Exception ex)
                {
                    LogManager.GetLogger(typeof(ZipLookupManager)).Error(ex);
                }
            }

            return new ZipInfo { Zip = zip };
        }

		public ZipInfo LookupUS(string zip)
		{
			return ExecuteLookup(zip, provider => provider.LookupUS(zip));
		}

		private ZipInfo ExecuteLookup(string zip, Func<ZipLookupProvider, ZipInfo> execute)
		{
			if (_lookupProviders.Count == 0)
			{
				AddAllProviders();
			}

			foreach (var provider in _lookupProviders)
			{
				try
				{
					var zipInfo = execute(provider);

					if (zipInfo != null)
					{
						return zipInfo;
					}
				}
				catch (Exception ex)
				{
					LogManager.GetLogger(typeof(ZipLookupManager)).Error(ex);
				}
			}

			return new ZipInfo { Zip = zip };
		}
	}
}
