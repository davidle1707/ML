using ML.Common.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using ML.Utils.IpLookup.Providers;

namespace ML.Utils.IpLookup
{
	public class IpLookupManager
	{
		private List<IpLookupProvider> _lookupProviders;

		public IpLookupManager(IpLookupProvider lookupProvider)
			: this()
		{
			_lookupProviders.Clear();
			_lookupProviders.Add(lookupProvider);
		}

		/// <summary>
        /// Freegeoip is default
		/// </summary>
		public IpLookupManager()
		{
			AddAllProviders();
		}

		public void AddProvider(IpLookupProvider lookupProvider)
		{
			if (_lookupProviders.All(p => p.GetType() != lookupProvider.GetType()))
			{
				_lookupProviders.Add(lookupProvider);
			}
		}

		public void AddAllProviders()
		{
			_lookupProviders = new List<IpLookupProvider>
			                   {
				                   new DbIpProvider(),
				                   new FreegeoIpProvider()
			                   };
		}

		public IpInfo Lookup(string ip)
		{
			return ExecuteLookup(ip, provider => provider.Lookup(ip));
		}

		private IpInfo ExecuteLookup(string ip, Func<IpLookupProvider, IpInfo> execute)
		{
			if (_lookupProviders.Count == 0)
			{
				AddAllProviders();
			}

			foreach (var provider in _lookupProviders)
			{
				try
				{
					var ipInfo = execute(provider);

					if (ipInfo != null)
					{
						return ipInfo;
					}
				}
				catch (Exception ex)
				{
					LogManager.GetLogger(typeof(IpLookupManager)).Error(ex);
				}
			}

			return new IpInfo { Ip = ip };
		}
	}
}
