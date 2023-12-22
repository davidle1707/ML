using System;

namespace ML.Utils.VitelityFax
{
    public static class Extensions
    {
        public static bool IsReady(this System.Xml.XmlNode item)
        {
            return (item != null && (item.InnerText.HasValue()));
        }

        public static bool HasValue(this string value)
        {
            if (!String.IsNullOrEmpty(value) && value.Length > 0)
            {
                return true;
            }
            return false;
        }

        public static string GetText(this System.Xml.XmlNode item)
        {
            if (item != null)
            {
                return item.InnerText;
            }
            return string.Empty;
        }

        public static bool GetBool(this System.Xml.XmlNode item)
        {
            if (item.IsReady())
            {
                return item.InnerText == "true";
            }
            return false;
        }

        public static DateTime GetDateTime(this System.Xml.XmlNode item)
        {
            if (item.IsReady())
            {
                return Convert.ToDateTime(item.InnerText);
            }
            return DateTime.MinValue;
        }

        public static int GetInt(this System.Xml.XmlNode item)
        {
            if (item.IsReady())
            {
                return Convert.ToInt32(item.InnerText);

            }
            return 0;
        }

        public static decimal GetDecimal(this System.Xml.XmlNode item)
        {
            if (item.IsReady())
            {
                return Convert.ToDecimal(item.InnerText);
            }
            return 0;
        }

        public static string ToStr(this object item, string defaultString = "")
        {
            if (item == null || item.Equals(DBNull.Value))
                return defaultString;

            return item.ToString().Trim();
        }
    }
}
