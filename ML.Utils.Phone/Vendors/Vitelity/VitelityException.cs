using System;

namespace ML.Utils.Phone.Vendors.Vitelity
{
    public class VitelityException : Exception
    {
        public bool IsUnauthorized;

        public VitelityException(string message, bool isUnauthorized = false)
            : base(message)
        {
            IsUnauthorized = isUnauthorized;
        }

        public VitelityException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
