using System;

namespace ML.Common.Attributes
{
	public class NameResolverAttribute : Attribute
	{
		public NameResolverAttribute(string nameResolverKey)
    	{
			NameResolverKey = nameResolverKey;
    	}

		public string NameResolverKey { get; private set; }
	}
}
