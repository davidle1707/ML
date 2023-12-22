using System;
using System.Collections.Generic;

namespace MLXamarin.Common
{
    public class EnumHelper
    {
        public static Dictionary<int, string> ToDictionary<TEnum>(Func<string, string> formatName = null) where TEnum : struct
        {
            var enums = new Dictionary<int, string>();

            var enumType = typeof(TEnum);
            var enumValues = Enum.GetValues(enumType);

            foreach (var enumValue in enumValues)
            {
                var @enum = Enum.ToObject(enumType, enumValue);

                var name = @enum.ToString();

                if (formatName != null)
                {
                    name = formatName(name);
                }

                enums.Add((int)enumValue, name);
            }

            return enums;
        }
    }
}
