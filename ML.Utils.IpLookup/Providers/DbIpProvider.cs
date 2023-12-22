using System;
using System.Collections.Generic;
using System.Linq;
using ML.Common;
using Newtonsoft.Json.Linq;

namespace ML.Utils.IpLookup.Providers
{
	/*
	 * 
	 * Site: https://db-ip.com/api/#documentation
	 * 
	 * API Lookup: http://api.db-ip.com/addrinfo?addr={ip}&api_key={key}
	 * 
	 * API Key Info: http://api.db-ip.com/keyinfo?api_key={key}
	 * 
	 */

	public class DbIpProvider : IpLookupProvider
	{
		public static List<ApiKey> ApiKeys = new List<ApiKey>
		{
			new ApiKey("f14e50d1bed6f062f670ccc8c79be40f27a21b3b"), //huyngo@gocentel.com
			new ApiKey("3373b96fd3afb30813c669fb0a5b87b58c19dee0"), //huyngo1983@gmail.com
			new ApiKey("81a6b45965f8929524ba1ea9b53627a357585dcd"), //huykhtn2002@yahoo.com
			new ApiKey("f4f647539ea9cdf4bf51525d38ce3cff018a47c9"), //trinh.huflit@gmail.com
			new ApiKey("385756a7ce0769d9c53a9ac91226b7757443b76b"), //thuongtranv@gmail.com
			new ApiKey("02872dcae610f5d318646bb8053fa7c07b6abbe6"), //quynhnguyentk2@gmail.com
			new ApiKey("13bdee8e60ab19a2cb7b626c90cf87c2d3f077e2"), //trungnguyen@interactivecontactcenter.com
			new ApiKey("3ea6cb3e23ec1503f9887cd6f9eb6015b608619a"), //pthevinh@gmail.com
			new ApiKey("d3b0504d0bb37e99a13dc6269a0cbdcfe3db52ab"), //kiettran88@gmail.com
			new ApiKey("904a871fbd177a54d373def7b12a023a66cc3f3e"), //tqt2285@gmail.com
			new ApiKey("610060de8f9c0f9ba3fa6f9d816de0174b4255cb"), //vanthuong420@yahoo.com
		};

		public DbIpProvider(params string[] apiKeys)
		{
			if (apiKeys != null && apiKeys.Length > 0)
			{
				ApiKeys.Clear();
				ApiKeys.AddRange(apiKeys.Distinct().Select(k => new ApiKey(k)));
			}
		}

		internal override IpInfo Lookup(string ip)
		{
			foreach (var apiKey in ApiKeys.OrderBy(k => k.InvaidDate))
			{
				if (!apiKey.IsAlive())
				{
					continue;	
				}

				var responseAsJson = HttpHelper.Get(string.Format("http://api.db-ip.com/addrinfo?addr={0}&api_key={1}", ip, apiKey.Key));

				if (string.IsNullOrWhiteSpace(responseAsJson))
				{
					return null;
				}

				JObject response;

				if ((response = std.DeserializeJson(responseAsJson)) == null || response.Count == 0)
				{
					return null;
				}

				if (string.IsNullOrEmpty(response.Value<string>("error")))
				{
					return new IpInfo
					       {
						       Ip = ip,
						       Country = response.Value<string>("country"),
						       CountryCode = response.Value<string>("country"),
						       Region = response.Value<string>("stateprov"),
						       RegionCode = response.Value<string>("stateprov"),
						       City = response.Value<string>("city"),
							   ByProvider = typeof(DbIpProvider).Name
					       };
				}

				apiKey.InvaidDate = DateTime.UtcNow;
			}

			return null;
		}

		[Serializable]
		public class ApiKey
		{
			public string Key { get; set; }

			public DateTime? InvaidDate { get; set; }

			public ApiKey(string key)
			{
				Key = key;
			}

			//Maximum 2000 request per day
			public bool IsAlive()
			{
				if (InvaidDate != null && (DateTime.UtcNow - InvaidDate.Value).TotalDays >= 1 )
				{
					InvaidDate = null;
				}

				return InvaidDate == null;
			}
		}
	}
}
