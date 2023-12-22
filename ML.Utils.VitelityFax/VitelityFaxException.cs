using System;

namespace ML.Utils.VitelityFax
{
    public class VitelityFaxException : Exception
    {
        public bool IsUnauthorized;

        public VitelityFaxException(string message, bool isUnauthorized = false)
            : base(message)
        {
            IsUnauthorized = isUnauthorized;
        }

        public VitelityFaxException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
