using System;

namespace ML.Utils.MarkLogic
{
	[Serializable]
	public class XException : Exception
	{
		public XException(string message)
			: base(message)
		{
		}

		public XException(string message, Exception exception)
			: base(message, exception)
		{
		}
	}
}
