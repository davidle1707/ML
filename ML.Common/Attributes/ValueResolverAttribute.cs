using System;

namespace ML.Common.Attributes
{
	public class ValueResolverAttribute : Attribute
	{
		public ValueResolverAttribute(string valueResolverKey)
    	{
			ValueResolverKey = valueResolverKey;
    	}

		public string ValueResolverKey { get; private set; }
	}
}
