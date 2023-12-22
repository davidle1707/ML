using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using ML.Common;

namespace ML.Utils.MarkLogic
{
	[XmlRoot("Response")]
	public class ExecuteResponse<T> where T : class
	{
		public ExecuteResponse()
		{
			Items = new List<T>();
		}

        public int Total { get; set; }

        public List<T> Items { get; set; }

		public T FirstOrDefault()
		{
			return Items.FirstOrDefault();
		}
    }

    [XmlRoot("Response")]
	public class ExecuteScalarResponse
    {
        public string Value { get; set; }
    }
}
