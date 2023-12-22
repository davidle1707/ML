using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Utils.IntegrityDirect
{
	[Serializable]
	public class MatchDescription
	{
		public string FieldName { get; set; }

		public string FieldValue { get; set; }

		internal FieldFormat FieldFormat { get; set; }

		internal string FieldFormatDescription { get; set; }
	}

	internal enum FieldFormat
	{
		String,

		StringRight,

		DateTime,
	}
}
