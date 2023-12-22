using System;

namespace ML.Utils.VeratadIDresponse
{
	public class VeratadIDresponseException : Exception
	{
		public VeratadIDresponseException(string message)
            : base(message)
        {
        }

		public VeratadIDresponseException(string message, Exception exception)
            : base(message, exception)
        {
        }
	}
}
