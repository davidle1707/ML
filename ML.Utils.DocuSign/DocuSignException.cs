using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML.Utils.DocuSign
{
    public class DocuSignException : Exception
    {
        public DocuSignException(string message)
            : base(message)
        {
        }

        public DocuSignException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
