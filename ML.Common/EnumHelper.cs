using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ML.Common
{
    public static class EnumHelper
    {
        public static bool TryGetEnum<TEnum>(this object source, out TEnum result) where TEnum : struct
        {
            var value = GetEnumNull<TEnum>(source);
            result = value ?? default(TEnum);

            return value != null;
        }

        public static bool TryGetEnumObj(this object source, Type enumType, out object enumObj)
        {
            enumObj = null;

            try
            {
                enumObj = source switch
                {
                    int valInt => Enum.IsDefined(enumType, valInt) ? Enum.ToObject(enumType, valInt) : null,
                    short valShort => Enum.IsDefined(enumType, valShort) ? Enum.ToObject(enumType, valShort) : null,
                    string valStr => Enum.Parse(enumType, valStr, true),
                    _ => null
                };
            }
            catch (Exception) { }

            return enumObj != null;
        }

        public static TEnum GetEnum<TEnum>(this object source, TEnum defValue = default(TEnum)) where TEnum : struct
           => GetEnumNull<TEnum>(source) ?? defValue;

        public static TEnum? GetEnumNull<TEnum>(this object source) where TEnum : struct
        {
            if (source == null)
            {
                return null;
            }
            switch (source)
            {
                case int valInt when !Enum.IsDefined(typeof(TEnum), valInt):
                    return null;

                case short valShort when !Enum.IsDefined(typeof(TEnum), (int)valShort):
                    return null;

                default:
                    return Enum.TryParse<TEnum>(source.ToString(), true, out var outValue) ? outValue : (TEnum?)null;
            }
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static List<string> ToNames<TEnum>(Func<string, string> formatName = null) where TEnum : struct, IConvertible
        {
            var enums = ToDictionary<TEnum>(formatName);

            return enums.Select(e => e.Value).ToList();
        }

        public static List<short> ToValues<TEnum>() where TEnum : struct, IConvertible
        {
            return ToValues<TEnum, short>();
        }

        public static List<TValue> ToValues<TEnum, TValue>() where TEnum : struct, IConvertible
        {
            var enums = ToDictionary<TEnum, TValue>();

            return enums.Select(e => e.Key).ToList();
        }

        public static Dictionary<short, string> ToDictionary<TEnum>(Func<string, string> formatName = null, bool descriptionAsName = false) where TEnum : struct, IConvertible
        {
            return ToDictionary<TEnum, short>(formatName, descriptionAsName);
        }

        public static Dictionary<TValue, string> ToDictionary<TEnum, TValue>(Func<string, string> formatName = null, bool descriptionAsName = false) where TEnum : struct, IConvertible
        {
            var enums = new Dictionary<TValue, string>();

            var enumType = typeof(TEnum);

            if (enumType.BaseType == typeof(Enum))
            {
                var enumValArray = Enum.GetValues(enumType);

                foreach (TValue val in enumValArray)
                {
                    var name = descriptionAsName ? ((Enum)Enum.ToObject(enumType, val)).GetDescription() : Enum.ToObject(enumType, val).ToString();
                    if (formatName != null)
                    {
                        name = formatName(name);
                    }

                    enums.Add(val, name);
                }
            }

            return enums;
        }

        public static Dictionary<short, string> ToDictionary2<T>(Func<object, string> formatName = null) where T : struct, IConvertible
        {
            var enums = new Dictionary<short, string>();

            var enumType = typeof(T);

            if (enumType.BaseType == typeof(Enum))
            {
                var enumValArray = Enum.GetValues(enumType);

                foreach (short val in enumValArray)
                {
                    var objEnum = Enum.ToObject(enumType, val);
                    var name = objEnum.ToString();

                    if (formatName != null)
                    {
                        name = formatName(objEnum);
                    }

                    enums.Add(val, name);
                }
            }

            return enums;
        }

        public static Dictionary<string, string> ToDictionary3<T>(Func<object, string> formatName = null) where T : struct, IConvertible
        {
            var enums = new Dictionary<string, string>();

            var enumType = typeof(T);

            if (enumType.BaseType == typeof(Enum))
            {
                var enumValArray = Enum.GetValues(enumType);

                foreach (short val in enumValArray)
                {
                    var objEnum = Enum.ToObject(enumType, val);
                    var name = objEnum.ToString();

                    if (formatName != null)
                    {
                        name = formatName(objEnum);
                    }

                    enums.Add(name, name);
                }
            }

            return enums;
        }

        public static Dictionary<int, string> ToDictionary4<T>(Func<string, string> formatName = null, bool descriptionAsName = false) where T : struct, IConvertible
        {
            var enums = new Dictionary<int, string>();

            var enumType = typeof(T);

            if (enumType.BaseType == typeof(Enum))
            {
                var enumValArray = Enum.GetValues(enumType);

                foreach (int val in enumValArray)
                {
                    var name = descriptionAsName ? ((Enum)Enum.ToObject(enumType, val)).GetDescription() : Enum.ToObject(enumType, val).ToString();
                    if (formatName != null)
                    {
                        name = formatName(name);
                    }

                    enums.Add(val, name);
                }
            }

            return enums;
        }

        public static IEnumerable<TEnum> ToEnumerableWithAttr<TEnum, TAttr>(Func<TAttr, bool> filter)
            where TEnum : struct, IConvertible
            where TAttr : Attribute
        {
            return ToEnumerable<TEnum>(@enum =>
            {
                var attrFilter = GetAttribute<TEnum, TAttr>(@enum);
                return attrFilter != null && filter(attrFilter);
            });
        }

        public class EnumAttr<TEnum, TAttr>
            where TEnum : struct, IConvertible
            where TAttr : Attribute
        {
            public TEnum Enum { get; set; }

            public TAttr Attribute { get; set; }
        }

        public static IEnumerable<EnumAttr<TEnum, TAttr>> ToEnumerableWithAttr<TEnum, TAttr>()
          where TEnum : struct, IConvertible
          where TAttr : Attribute
        {
            return ToEnumerable<TEnum>().Select(@enum => new EnumAttr<TEnum, TAttr>
            {
                Enum = @enum,
                Attribute = GetAttribute<TEnum, TAttr>(@enum)
            });
        }

        public static IEnumerable<TEnum> ToEnumerable<TEnum>(Func<TEnum, bool> filter = null)
            where TEnum : struct, IConvertible
        {
            var enumType = typeof(TEnum);
            foreach (var item in ToEnumerable(enumType, filter))
            {
                yield return item;
            }
        }

        public static IEnumerable<TEnum> ToEnumerable<TEnum>(Type enumType, Func<TEnum, bool> filter = null)
           where TEnum : struct, IConvertible
        {
            if (enumType.BaseType != typeof(Enum))
            {
                yield break;
            }

            foreach (var val in Enum.GetValues(enumType))
            {
                var objEnum = (TEnum)Enum.ToObject(enumType, val);

                if (filter != null)
                {
                    if (filter(objEnum))
                    {
                        yield return objEnum;
                    }
                }
                else
                {
                    yield return objEnum;
                }
            }
        }

        public static string GetDescription(this Enum @enum, bool nameAsDescIfInvalid = true, bool friendlyCase = true)
        {
            var attrDesc = @enum.GetAttribute<DescriptionAttribute>();
            if (attrDesc != null)
            {
                return attrDesc.Description;
            }

            var attrEnumText = @enum.GetAttribute<EnumTextAttribute>();
            if (attrEnumText != null)
            {
                return attrEnumText.Description;
            }

            return nameAsDescIfInvalid ? (friendlyCase ? @enum.ToString().ToFriendlyCase() : @enum.ToString()) : string.Empty;
        }

        public static TAttribute GetAttribute<TAttribute>(this Enum @enum)
            where TAttribute : Attribute
        {
            var fieldInfo = @enum.GetType().GetField(@enum.ToString());

            return fieldInfo?.GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault() as TAttribute;
        }

        public static TAttribute GetAttribute<TEnum, TAttribute>(TEnum @enum)
            where TEnum : struct, IConvertible
            where TAttribute : Attribute
        {
            var enumType = typeof(TEnum);

            if (enumType.BaseType == typeof(Enum))
            {
                var fieldInfo = @enum.GetType().GetField(@enum.ToString());

                return fieldInfo?.GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault() as TAttribute;
            }

            return null;
        }

    }

    [Serializable]
    public class EnumTextAttribute : DescriptionAttribute
    {
        public string Text { get; set; }

        public EnumTextAttribute(string text, string desc = "") : base(desc)
        {
            Text = text;
        }
    }
}