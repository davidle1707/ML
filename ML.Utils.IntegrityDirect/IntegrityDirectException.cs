using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Utils.IntegrityDirect
{
	public class IntegrityDirectException : Exception
	{
		public IntegrityDirectException(string message)
            : base(message)
        {
        }

		public IntegrityDirectException(string message, Exception exception)
            : base(message, exception)
        {
        }
	}
}
