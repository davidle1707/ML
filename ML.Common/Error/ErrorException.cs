using System;

namespace ML.Common.Error
{
    public class ErrorException : Exception
    {
        public ErrorException(string message, int errorId)
			:base(message)
		{
			ErrorId = errorId;
		}

		public int ErrorId { get; private set; }
    }
}
