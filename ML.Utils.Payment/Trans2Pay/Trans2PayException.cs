using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML.Utils.Trans2Pay
{
    public class Trans2PayException : Exception
    {
        public Trans2PayException(string message)
            : base(message)
        {
        }

        public Trans2PayException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
