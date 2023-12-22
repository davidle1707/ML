using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MLXamarin.Common
{
    public static class std
    {
        #region String

        public static string Left(this string source, int left)
        {
            if (left <= 0)
            {
                return string.Empty;
            }

            source = source ?? string.Empty;

            return source.Length <= left ? source : source.Substring(0, left);
        }

        public static string Right(this string source, int right)
        {
            if (right <= 0)
            {
                return string.Empty;
            }

            source = source ?? string.Empty;

            return source.Length <= right ? source : source.Substring(source.Length - right, right);
        }

        public static string Middle(this string source, int startIndex, int length)
        {
            source = source ?? string.Empty;

            return startIndex >= 0 && source.Length > startIndex
                ? source.Substring(startIndex).Left(length)
                : string.Empty;
        }

        public static string FriendlyCase(this string value, string removeText = "")
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }

            var friendlyCase = Regex.Replace(value, "(?!^)([A-Z])", " $1");

            return string.IsNullOrEmpty(removeText) ? friendlyCase : friendlyCase.Replace(removeText, "");
        }

        #endregion

        #region Convert

        private static string CustomStringToNumber(this object item)
        {
            return item.ToStr().Replace(",", "");
        }

        public static short ToShort(this object item, short defaultInt = default(short))
        {
            if (item == null || string.IsNullOrEmpty(item.ToString()))
                return defaultInt;

            short result;
            return !short.TryParse(item.CustomStringToNumber(), out result) ? defaultInt : result;
        }

        public static short? ToShortNull(this object item)
        {
            if (item == null || string.IsNullOrEmpty(item.ToString()))
                return null;

            short result;
            return !short.TryParse(item.CustomStringToNumber(), out result) ? (short?)null : result;
        }

        public static int ToInt(this object item, int defaultInt = default(int))
        {
            if (item == null || string.IsNullOrEmpty(item.ToString()))
                return defaultInt;

            int result;
            return !int.TryParse(item.CustomStringToNumber(), out result) ? defaultInt : result;
        }

        public static int? ToIntNull(this object item)
        {
            if (item == null || string.IsNullOrEmpty(item.ToString()))
                return null;

            int result;
            return !int.TryParse(item.CustomStringToNumber(), out result) ? (int?)null : result;
        }

        public static long ToLong(this object item, long defaultLong = default(long))
        {
            if (item == null || string.IsNullOrEmpty(item.ToString()))
                return defaultLong;

            long result;
            return !long.TryParse(item.CustomStringToNumber(), out result) ? defaultLong : result;
        }

        public static long? ToLongNull(this object item)
        {
            if (item == null || string.IsNullOrEmpty(item.ToString()))
                return null;

            long result;
            return !long.TryParse(item.CustomStringToNumber(), out result) ? (long?)null : result;
        }

        public static double ToDouble(this object item, double defaultDouble = default(double))
        {
            if (item == null || string.IsNullOrEmpty(item.ToString()))
                return defaultDouble;

            double result;
            return !double.TryParse(item.CustomStringToNumber(), out result) ? defaultDouble : result;
        }

        public static double? ToDoubleNull(this object item)
        {
            if (item == null || item.ToStr() == string.Empty)
                return null;

            double result;
            return !double.TryParse(item.CustomStringToNumber(), out result) ? (double?)null : result;
        }

        public static decimal ToDecimal(this object item, decimal defaultDecimal = default(decimal))
        {
            if (item == null || string.IsNullOrEmpty(item.ToString()))
                return defaultDecimal;

            decimal result;
            return !decimal.TryParse(item.CustomStringToNumber(), out result) ? defaultDecimal : result;
        }

        public static decimal? ToDecimalNull(this object item)
        {
            if (item == null || string.IsNullOrEmpty(item.ToString()))
                return null;

            decimal result;
            return !decimal.TryParse(item.CustomStringToNumber(), out result) ? (decimal?)null : result;
        }

        public static string ToMoneyString(this decimal item, int decimalPlace = 2)
        {
            if (item % 1 > 0)
                return item.ToString(string.Format("C{0}", decimalPlace));
            return item.ToString(string.Format("C0"));
        }

        public static string ToMoneyNullString(this decimal? item, int decimalPlace = 2)
        {
            if (item != null)
            {
                return ToMoneyString(item.Value, decimalPlace);
            }
            return string.Empty;
        }

        public static string ToDecimalString(this decimal item, int decimalPlace = 2)
        {
            return item.ToString(string.Format("N{0}", decimalPlace));
        }

        public static string ToDecimalNullString(this decimal? item, int decimalPlace = 2)
        {
            return item != null ? item.Value.ToString(string.Format("N{0}", decimalPlace)) : string.Empty;
        }

        public static string ToOrdinalString(this int item)
        {
            var val = item.ToString();

            item %= 100;

            if ((item >= 11) && (item <= 13))
            {
                return string.Format("{0}th", val);
            }

            switch (item % 10)
            {
                case 1:
                    return string.Format("{0}st", val);
                case 2:
                    return string.Format("{0}nd", val);
                case 3:
                    return string.Format("{0}rd", val);
                default:
                    return string.Format("{0}th", val);
            }
        }

        public static string ToStr(this object item, string defaultString = "")
        {
            return item == null ? defaultString : item.ToString().Trim();
        }

        public static bool EqualsString(this object item, object toCompare)
        {
            return item.ToStr().ToLower() == toCompare.ToStr().ToLower();
        }

        public static bool StartWithString(this object item, object toCompare)
        {
            return item.ToStr().ToLower().StartsWith(toCompare.ToStr().ToLower());
        }

        public static bool EndWithString(this object item, object toCompare)
        {
            return item.ToStr().ToLower().EndsWith(toCompare.ToStr().ToLower());
        }

        public static bool ContainsString(this object item, object toCompare)
        {
            return item.ToStr().ToLower().Contains(toCompare.ToStr().ToLower());
        }

        private static readonly string[] BoolValues = { "yes", "y", "true", "true,false", "on" };

        public static bool ToBool(this object item, bool defaultBool = default(bool))
        {
            if (item == null)
                return defaultBool;

            return BoolValues.Contains(item.ToStr().ToLower());
        }

        public static bool? ToBoolNull(this object item)
        {
            if (item == null || string.IsNullOrEmpty(item.ToString()))
                return null;

            return BoolValues.Contains(item.ToStr().ToLower());
        }

        public static byte[] ToBytes(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            return Convert.FromBase64String(s);
        }

        public static string ToBase64String(this byte[] item)
        {
            if (item == null)
                return null;

            return Convert.ToBase64String(item);
        }

        public static string ToBase64String(this Stream item)
        {
            if (item == null)
                return null;

            using (var memStream = new MemoryStream())
            {
                item.CopyTo(memStream);
                return Convert.ToBase64String(memStream.ToArray());
            }
        }

        public static Guid ToGuid(this object item)
        {
            var source = item.ToStr();
            if (source == "") return Guid.Empty;

            try { return new Guid(source); }
            catch { return Guid.Empty; }
        }

        public static Guid? ToGuidNull(this object item)
        {
            if (item == null || string.IsNullOrEmpty(item.ToString()))
                return null;

            try { return new Guid(item.ToString()); }
            catch { return null; }
        }

        public static string ToPhoneNumber(this object source)
        {
            var sphone = Regex.Replace(source.ToStr(), "[^0-9]", "");

            if (sphone.Length > 10 && sphone.StartsWith("1"))
            {
                sphone = sphone.Substring(1);
            }

            return sphone.PadLeft(10).Trim();
        }

        public static string ToSsnNumber(this object source)
        {
            var snumber = Regex.Replace(source.ToStr(), "[^0-9]", "");
            return snumber.PadLeft(9).Trim();
        }

        public static string ToZipCodeNumber(this object source)
        {
            var snumber = Regex.Replace(source.ToStr(), "[^0-9]", "");
            return snumber.PadLeft(5).Trim();
        }

        public static string ToStringNumber(this object source)
        {
            var number = Regex.Replace(source.ToStr(), "[^0-9]", "");
            return number.Trim();
        }

        public static string ToStringNumberEx(this object source, bool hasDecimaPlaces = false, bool hasNegative = false)
        {
            var pattern = "0-9";
            if (hasDecimaPlaces) pattern += @"\.";
            if (hasNegative) pattern += @"\-";

            var number = Regex.Replace(source.ToStr(), "[^" + pattern + "]", "");
            return number.Trim();
        }

        public static List<Guid> ToListOfGuid(this string source, char separator = ';', Func<Guid, bool> funcFilter = null)
        {
            var guids = source.ToStr().Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries).Select(id => id.ToGuid());

            if (funcFilter != null)
            {
                guids = guids.Where(funcFilter);
            }

            return guids.ToList();
        }

        public static string ReplaceAll(this object source, string replacement = "")
        {
            return Regex.Replace(source.ToStr(), @"\s+", replacement);
        }

        #endregion

        #region Convert DateTime
        public static DateTime ToDateTime(this object item, DateTime defaultDateTime = default(DateTime))
        {
            if (string.IsNullOrEmpty(item?.ToString()))
            {
                return defaultDateTime;
            }

            DateTime result;

            return DateTime.TryParse(item.ToString(), out result) ? result : defaultDateTime;
        }

        public static DateTime? ToDateTimeNull(this object item)
        {
            if (string.IsNullOrEmpty(item?.ToString()))
            {
                return null;
            }

            DateTime result;

            return DateTime.TryParse(item.ToString(), out result) ? result : (DateTime?)null;
        }

        public static DateTime ToStartDateTime(this object item)
        {
            return item.ToDateTime().StartDateTime();
        }

        public static DateTime? ToStartDateTimeNull(this object item)
        {
            return item.ToDateTimeNull().StartDateTime();
        }

        public static DateTime ToEndDateTime(this object item)
        {
            return item.ToDateTime().EndDateTime();
        }

        public static DateTime? ToEndDateTimeNull(this object item)
        {
            return item.ToDateTimeNull().EndDateTime();
        }

        public static DateTime StartDateTimeUtc(this DateTime item, string timeZone)
        {
            return item.StartDateTime().ToUtcDate(timeZone);
        }

        public static DateTime? StartDateTimeUtc(this DateTime? item, string timeZone)
        {
            return item.StartDateTime().ConvertToUtcNull(timeZone);
        }

        public static DateTime StartDateTimeLocal(this DateTime item, string timeZone)
        {
            return item.StartDateTime().ToLocalDate(timeZone);
        }

        public static DateTime? StartDateTimeLocal(this DateTime? item, string timeZone)
        {
            return item.StartDateTime().ConvertToLocalNull(timeZone);
        }

        public static DateTime StartDateTime(this DateTime item)
        {
            return new DateTime(item.Year, item.Month, item.Day);
        }

        public static DateTime? StartDateTime(this DateTime? item)
        {
            return item != null ? new DateTime(item.Value.Year, item.Value.Month, item.Value.Day) : (DateTime?)null;
        }

        public static DateTime EndDateTimeUtc(this DateTime item, string timeZone)
        {
            return item.EndDateTime().ToUtcDate(timeZone);
        }

        public static DateTime? EndDateTimeUtc(this DateTime? item, string timeZone)
        {
            return item.EndDateTime().ConvertToUtcNull(timeZone);
        }

        public static DateTime EndDateTimeLocal(this DateTime item, string timeZone)
        {
            return item.EndDateTime().ToLocalDate(timeZone);
        }

        public static DateTime? EndDateTimeLocal(this DateTime? item, string timeZone)
        {
            return item.StartDateTime().ConvertToLocalNull(timeZone);
        }

        public static DateTime EndDateTime(this DateTime item)
        {
            return new DateTime(item.Year, item.Month, item.Day, 23, 59, 59);
        }

        public static DateTime? EndDateTime(this DateTime? item)
        {
            return item != null ? new DateTime(item.Value.Year, item.Value.Month, item.Value.Day, 23, 59, 59) : (DateTime?)null;
        }

        public static DateTime NowLocal(string timeZone)
        {
            return DateTime.UtcNow.ToLocalDate(timeZone);
        }

        public static DateTime TodayStartDateUtc(string timeZone)
        {
            return TodayStartDateLocal(timeZone).ToUtcDate(timeZone);
        }

        public static DateTime TodayStartDateLocal(string timeZone)
        {
            return NowLocal(timeZone).StartDateTime();
        }

        public static DateTime TodayEndDateUtc(string timeZone)
        {
            return TodayEndDateLocal(timeZone).ToUtcDate(timeZone);
        }

        public static DateTime TodayEndDateLocal(string timeZone)
        {
            return NowLocal(timeZone).EndDateTime();
        }

        public static DateTime ThisWeekStartDateUtc(string timeZone)
        {
            return ThisWeekStartDateLocal(timeZone).ToUtcDate(timeZone);
        }

        public static DateTime ThisWeekStartDateLocal(string timeZone)
        {
            var localStart = TodayStartDateLocal(timeZone);
            return localStart.AddDays(-(int)localStart.DayOfWeek);
        }

        public static DateTime ThisWeekEndDateUtc(string timeZone, bool fullWeek = false)
        {
            return ThisWeekEndDateLocal(timeZone).ToUtcDate(timeZone);
        }

        public static DateTime ThisWeekEndDateLocal(string timeZone, bool fullWeek = false)
        {
            var localEnd = TodayEndDateLocal(timeZone);
            return localEnd.AddDays(6 - (int)localEnd.DayOfWeek);
        }

        public static DateTime ThisMonthStartDateUtc(string timeZone)
        {
            return ThisMonthStartDateLocal(timeZone).ToUtcDate(timeZone);
        }

        public static DateTime ThisMonthStartDateLocal(string timeZone)
        {
            var local = NowLocal(timeZone);
            return new DateTime(local.Year, local.Month, 1, 0, 0, 0);
        }

        public static DateTime ThisMonthEndDateUtc(string timeZone, bool fullMonth = false)
        {
            return ThisMonthEndDateLocal(timeZone, fullMonth).ToUtcDate(timeZone);
        }

        public static DateTime ThisMonthEndDateLocal(string timeZone, bool fullMonth = false)
        {
            var local = NowLocal(timeZone);

            return fullMonth
                 ? new DateTime(local.Year, local.Month, DateTime.DaysInMonth(local.Year, local.Month), 23, 59, 59)
                 : local.EndDateTime();
        }

        public static DateTime LastMonthStartDateUtc(string timeZone)
        {
            return LastMonthStartDateLocal(timeZone).ToUtcDate(timeZone);
        }

        public static DateTime LastMonthStartDateLocal(string timeZone)
        {
            return ThisMonthStartDateLocal(timeZone).AddMonths(-1);
        }

        public static DateTime LastMonthEndDateUtc(string timeZone)
        {
            return LastMonthEndDateLocal(timeZone).ToUtcDate(timeZone);
        }

        public static DateTime LastMonthEndDateLocal(string timeZone)
        {
            return ThisMonthStartDateLocal(timeZone).AddSeconds(-1);
        }

        public static DateTime Last90DaysStartDateUtc(string timeZone)
        {
            return Last90DaysStartDateLocal(timeZone).ToUtcDate(timeZone);
        }

        public static DateTime Last90DaysStartDateLocal(string timeZone)
        {
            return TodayStartDateLocal(timeZone).AddDays(-90);
        }

        public static DateTime Last90DaysEndDateUtc(string timeZone)
        {
            return Last90DaysEndDateLocal(timeZone).ToUtcDate(timeZone);
        }

        public static DateTime Last90DaysEndDateLocal(string timeZone)
        {
            return TodayEndDateLocal(timeZone);
        }

        public static DateTime? ConvertToUtcNull(this DateTime? source, string timeZoneId)
        {
            if (source == null)
            {
                return null;
            }

            return ToUtcDate(source.Value, timeZoneId);
        }

        public static DateTime? ConvertToLocalNull(this DateTime? source, string timeZoneId)
        {
            if (source == null)
            {
                return null;
            }

            return ToLocalDate(source.Value, timeZoneId);
        }

        public static DateTime ToUtcDate(this DateTime source, string timeZoneId)
        {
            return source;
            //if (source.Kind == DateTimeKind.Utc)
            //{
            //    return source;
            //}

            //var userDateTime = source.Kind == DateTimeKind.Local ? DateTime.SpecifyKind(source, DateTimeKind.Unspecified) : source;

            //return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(userDateTime, timeZoneId, TimeZoneInfo.Utc.Id);
        }

        public static DateTime ToLocalDate(this DateTime source, string timeZoneId)
        {
            return source;
            //var userDateTime = DateTime.SpecifyKind(source, DateTimeKind.Utc);
            //var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            //return TimeZoneInfo.ConvertTimeFromUtc(userDateTime, userTimeZone);
        }

        #endregion

        #region Other

        public static TAttribute GetAttribute<TAttribute>(this object obj) where TAttribute : Attribute
        {
            var fieldInfo = obj.GetType().GetRuntimeField(obj.ToString());

            return fieldInfo?.GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault() as TAttribute;
        }

        #endregion

        #region Distance

        /// <summary>
        /// convertToUnit: if null return Mile else 'K' -> kimometre, 'N' -> Nautical mile
        /// </summary>
        public static double Distance(double lat1, double lng1, double lat2, double lng2, char? convertToUnit = null)
        {
            var theta = lng1 - lng2;
            var dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;

            if (convertToUnit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (convertToUnit == 'N')
            {
                dist = dist * 0.8684;
            }
            return (dist);
        }

        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

        #endregion
    }
}
