using System;
using ML.Common;

namespace ML.Utils.IntegrityDirect
{
	[AttributeUsage(AttributeTargets.Property)]
	internal class QueryParamAttribute : Attribute
	{
		public string Name { get; private set; }

		public QueryParamAttribute(string name)
		{
			Name = name;
		}
	}

	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	internal class MatchDescriptionAttribute : Attribute
	{
		public readonly MatchDescription Description;

		public MatchDescriptionAttribute(string name, FieldFormat format = FieldFormat.String, string formatDescription = "")
		{
			Description = new MatchDescription
						  {
							  FieldName = name ?? string.Empty,
							  FieldFormat = format,
							  FieldFormatDescription = formatDescription
						  };
		}

		public bool IsMatch(string fieldName)
		{
			return Description.FieldName.Replace(" ", "").Equals((fieldName ?? string.Empty).Replace(" ", ""), StringComparison.OrdinalIgnoreCase);
		}

		public MatchDescription GetDescription(object value)
		{
			var response = new MatchDescription { FieldName = Description.FieldName };

			if (value != null)
			{
				response.FieldValue = value.ToStr();

				switch (Description.FieldFormat)
				{
					case FieldFormat.StringRight:
						response.FieldValue = response.FieldValue.Right(Description.FieldFormatDescription.ToInt());
						break;

					case FieldFormat.DateTime:
						response.FieldValue = response.FieldValue.ToDateTime().ToString(Description.FieldFormatDescription);
						break;
				}
			}

			return response;
		}
	}

}
