using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ML.Common
{
    public static partial class std
    {
        #region Variables

        private static readonly string[] BoolValues = { "yes", "y", "true", "true,false", "on" };

        public static NameValueCollection AppSettings => ConfigurationManager.AppSettings;

        public static string GetAppSetting(string key)
        {
            return AppSettings[key].ToStr();
        }

        public static ConnectionStringSettingsCollection ConnectionStrings => ConfigurationManager.ConnectionStrings;

        #endregion

        #region Converter

        private static Dictionary<string, Func<object, object>> _valueConverters;

        /// <summary>
        /// string, bool, boolean, short, int16, int, int32, long, int64, float, single, double, decimal, datetime, guid, objectid
        /// </summary>
        public static Dictionary<string, Func<object, object>> ValueConverters => _valueConverters ??= new()
        {
            ["string"] = val => val.ToStr(),

            ["bool"] = val => val.ToBool(),
            ["bool?"] = val => val.ToBoolNull(),

            ["boolean"] = val => val.ToBool(),
            ["boolean?"] = val => val.ToBoolNull(),

            ["short"] = val => val.ToShort(),
            ["short?"] = val => val.ToShortNull(),

            ["int16"] = val => val.ToShort(),
            ["int16?"] = val => val.ToShortNull(),

            ["int"] = val => val.ToInt(),
            ["int?"] = val => val.ToIntNull(),

            ["int32"] = val => val.ToInt(),
            ["int32?"] = val => val.ToIntNull(),

            ["long"] = val => val.ToLong(),
            ["long?"] = val => val.ToLongNull(),

            ["int64"] = val => val.ToLong(),
            ["int64?"] = val => val.ToLongNull(),

            ["float"] = val => val.ToFloat(),
            ["float?"] = val => val.ToFloatNull(),

            ["single"] = val => val.ToFloat(),
            ["single?"] = val => val.ToFloatNull(),

            ["double"] = val => val.ToDouble(),
            ["double?"] = val => val.ToDoubleNull(),

            ["decimal"] = val => val.ToDecimal(),
            ["decimal?"] = val => val.ToDecimalNull(),

            ["datetime"] = val => val.ToDateTime(),
            ["datetime?"] = val => val.ToDateTimeNull(),

            ["guid"] = val => val.ToGuid(),
            ["guid?"] = val => val.ToGuidNull(),

            ["objectid"] = val => val.ToObjectId(),
            ["objectid?"] = val => val.ToObjectIdNull(),
        };

        public static Func<object, object> ValueConverter(this Type valueType)
            => ValueConverter(valueType.GetTypeName());

        /// <summary>
        /// string, bool, short, int16, int, int32, long, int64, float, single, double, decimal, datetime, guid, objectid
        /// </summary>
        public static Func<object, object> ValueConverter(this string valueTypeName)
            => ValueConverters.TryGetValue(valueTypeName.ToLower(), out var converter) ? converter : null;

        #endregion

        #region Convert

        public static string ToStrNumber(this object source, string symbolGroup = ",") => source?.ToStr().Replace(symbolGroup, "") ?? "";

        public static bool TryToShort(this object source, out short val)
        {
            var toVal = source.ToShortNull();
            val = toVal ?? 0;

            return toVal != null;
        }

        public static short ToShort(this object source, short defVal = default) => source.ToShortNull() ?? defVal;

        public static short? ToShortNull(this object source) => ProcessToNumberNull<short>(source, short.TryParse);

        public static bool TryToInit(this object source, out int val)
        {
            var toVal = source.ToIntNull();
            val = toVal ?? 0;

            return toVal != null;
        }

        public static int ToInt(this object source, int defVal = default) => source.ToIntNull() ?? defVal;

        public static int? ToIntNull(this object source) => ProcessToNumberNull<int>(source, int.TryParse);

        public static bool TryToLong(this object source, out long val)
        {
            var toVal = source.ToLongNull();
            val = toVal ?? 0;

            return toVal != null;
        }

        public static long ToLong(this object source, long defVal = default) => source.ToLongNull() ?? defVal;

        public static long? ToLongNull(this object source) => ProcessToNumberNull<long>(source, long.TryParse);

        public static bool TryToFloat(this object source, out float val)
        {
            var toVal = source.ToFloatNull();
            val = toVal ?? 0;

            return toVal != null;
        }

        public static float ToFloat(this object source, float defVal = default) => source.ToFloatNull() ?? defVal;

        public static float? ToFloatNull(this object source) => ProcessToNumberNull<float>(source, float.TryParse);

        public static bool TryToDouble(this object source, out double val)
        {
            var toVal = source.ToDoubleNull();
            val = toVal ?? 0;

            return toVal != null;
        }

        public static double ToDouble(this object source, double defVal = default) => source.ToDoubleNull() ?? defVal;

        public static double? ToDoubleNull(this object source) => ProcessToNumberNull<double>(source, double.TryParse);

        public static bool TryToDecimal(this object source, out decimal val, int? decimalPoints = null, bool noRound = false)
        {
            var toVal = source.ToDecimalNull(decimalPoints, noRound);
            val = toVal ?? 0;

            return toVal != null;
        }

        public static decimal ToDecimal(this object source, decimal defVal = default, int? decimalPoints = null, bool noRound = false) => source.ToDecimalNull(decimalPoints, noRound) ?? defVal;

        /// <summary>
        /// noRound only apply if decimalPoints is not null
        /// </summary>
        public static decimal? ToDecimalNull(this object source, int? decimalPoints = null, bool noRound = false)
        {
            var decNum = ProcessToNumberNull<decimal>(source, decimal.TryParse);

            return decNum != null && decimalPoints > 0
                ? noRound ? ToDecimalNoRound(decNum.Value, decimalPoints.Value) : decimal.Round(decNum.Value, decimalPoints.Value)
                : decNum;
        }

        private delegate bool NumberTryParse<T>(string s, out T result) where T : struct;

        private static T? ProcessToNumberNull<T>(this object source, NumberTryParse<T> tryParse) where T : struct
        {
            if (source == null) return null;
            if (source is T okVal) return okVal;

            var strNumber = source.ToStrNumber();
            if (string.IsNullOrWhiteSpace(strNumber)) return null;

            return !tryParse(strNumber, out var result) ? (T?)null : result;
        }

        /// <summary>
        /// ex: 1.2357 with points 3 => 1.235
        /// </summary>
        public static decimal ToDecimalNoRound(this decimal source, int decimalPoints = 2)
        {
            var pow10 = (decimal)Math.Pow(10, decimalPoints);
            return decimal.Truncate(source * pow10) / pow10; // ex: 1.2357 with points 3 => 1.235
        }

        public static string ToMoneyString(this decimal source, int decimalPlace = 2)
        {
            if (source % 1 > 0)
                return source.ToString(string.Format("C{0}", decimalPlace));
            return source.ToString(string.Format("C0"));
        }

        public static string ToMoneyNullString(this decimal? source, int decimalPlace = 2)
        {
            if (source != null)
            {
                return ToMoneyString(source.Value, decimalPlace);
            }
            return string.Empty;
        }

        public static string ToDecimalString(this decimal source, int decimalPlace = 2, bool noRound = false)
        {
            if (noRound)
            {
                source = source.ToDecimalNoRound(decimalPlace);
            }

            return source.ToString(string.Format("N{0}", decimalPlace));
        }

        public static string ToDecimalNullString(this decimal? source, int decimalPlace = 2, bool noRound = false)
        {
            if (noRound && source != null)
            {
                source = source.Value.ToDecimalNoRound(decimalPlace);
            }

            return source != null ? source.Value.ToString(string.Format("N{0}", decimalPlace)) : string.Empty;
        }

        public static string ToPointString(this decimal source, int decimalPlace = 1)
        {
            var dp = "0.#";
            if (decimalPlace > 1)
            {
                for (var i = 0; i < decimalPlace - 1; i++)
                {
                    dp = dp + "#";
                }
            }
            return source.ToString(dp);
        }

        public static string ToPointNullString(this decimal? source, int maxDecimalPlace = 1)
        {
            return source != null ? source.Value.ToPointString(maxDecimalPlace) : string.Empty;
        }

        public static string ToOrdinalString(this int ordinal)
        {
            var val = ordinal.ToString();

            ordinal %= 100;

            if ((ordinal >= 11) && (ordinal <= 13))
            {
                return string.Format("{0}th", val);
            }

            return (ordinal % 10) switch
            {
                1 => string.Format("{0}st", val),
                2 => string.Format("{0}nd", val),
                3 => string.Format("{0}rd", val),
                _ => string.Format("{0}th", val),
            };
        }

        public static bool TryToStr(this object source, out string val)
        {
            val = null;
            if (source == null || source.Equals(DBNull.Value))
            {
                return false;
            }

            val = source.ToString().Trim();
            return true;
        }

        public static string ToStr(this object source, string defVal = "")
        {
            if (source == null || source.Equals(DBNull.Value))
            {
                return defVal;
            }

            return source.ToString().Trim();
        }

        public static bool TryToObjectId(this object source, out ObjectId outValue)
        {
            var val = source.ToObjectIdNull();
            outValue = val ?? ObjectId.Empty;

            return val != null;
        }

        public static ObjectId ToObjectId(this object source) => source.ToObjectIdNull() ?? ObjectId.Empty;

        public static ObjectId? ToObjectIdNull(this object source)
        {
            if (source == null) return null;

            var strVal = source.ToString();
            if (string.IsNullOrWhiteSpace(strVal)) return null;

            try
            {
                return new ObjectId(strVal);
            }
            catch { return null; }
        }

        public static List<ObjectId> ToListOfObjectId(this string source, char seperator = ';', Func<ObjectId, bool> funcFilter = null)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return new List<ObjectId>();
            }

            var objectIds = source.Split(new[] { seperator }, StringSplitOptions.RemoveEmptyEntries).Select(id => id.ToObjectId());

            if (funcFilter != null)
            {
                objectIds = objectIds.Where(funcFilter);
            }

            return objectIds.ToList();
        }

        public static bool TryToBool(this object source, out bool outVal)
        {
            var val = source.ToBoolNull();
            outVal = val ?? false;

            return val != null;
        }

        public static bool ToBool(this object source, bool defVal = default) => source.ToBoolNull() ?? defVal;

        public static bool? ToBoolNull(this object source)
        {
            if (source == null) return null;

            var strVal = source.ToStr().ToLower();
            if (string.IsNullOrWhiteSpace(strVal)) return null;

            return BoolValues.Contains(strVal);
        }

        public static bool TryToGuid(this object source, out Guid outVal)
        {
            var val = source.ToGuidNull();
            outVal = val ?? Guid.Empty;

            return val != null;
        }

        public static Guid ToGuid(this object source) => source.ToGuidNull() ?? Guid.Empty;

        public static Guid? ToGuidNull(this object source)
        {
            if (source == null) return null;

            var strVal = source.ToStr();
            if (string.IsNullOrWhiteSpace(strVal)) return null;

            try
            {
                return new Guid(strVal);
            }
            catch { return null; }
        }

        public static bool EqualsString(this object source, object toCompare)
        {
            return source.ToStr().ToLower() == toCompare.ToStr().ToLower();
        }

        public static bool StartWithString(this object source, object toCompare)
        {
            return source.ToStr().ToLower().StartsWith(toCompare.ToStr().ToLower());
        }

        public static bool EndWithString(this object source, object toCompare)
        {
            return source.ToStr().ToLower().EndsWith(toCompare.ToStr().ToLower());
        }

        public static bool ContainsString(this object source, object toCompare)
        {
            return source.ToStr().ToLower().Contains(toCompare.ToStr().ToLower());
        }

        public static bool InRange(this decimal source, decimal? from, decimal? to)
        {
            if (from != null && to != null)
            {
                return from <= source && source <= to;
            }
            if (from != null)
            {
                return from <= source;
            }
            if (to != null)
            {
                return source <= to;
            }

            return true;
        }

        public static byte[] ToBytes(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return null;

            return Convert.FromBase64String(s);
        }

        public static string ToBase64String(this byte[] source)
        {
            if (source == null)
                return null;

            return Convert.ToBase64String(source);
        }

        public static string ToBase64String(this Stream source)
        {
            if (source == null)
                return null;

            using var memStream = new MemoryStream();
            source.CopyTo(memStream);
            return Convert.ToBase64String(memStream.ToArray());
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

        public static string RemovePrefix(this object source, string prefix, bool ignoreCase = true)
        {
            var tmp = source.ToStr();

            if (ignoreCase)
            {
                tmp = tmp.ToLower();
                prefix = prefix.ToLower();
            }

            if (tmp.StartsWith(prefix))
            {
                tmp = tmp.Substring(prefix.Length);
            }

            return tmp;
        }

        public static long ToUnixTimestamp(this DateTime target)
        {
            var date = new DateTime(1970, 1, 1, 0, 0, 0, target.Kind);
            var unixTimestamp = Convert.ToInt64((target - date).TotalSeconds);

            return unixTimestamp;
        }

        /// <summary>
        /// https://coderwall.com/p/vshjwq/how-to-generate-clean-url-slug-in-c
        /// </summary>
        public static string ToUrlSlug(this string text)
        {
            //First to lower case 
            var value = text.ToLowerInvariant();

            //Remove all accents
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);

            value = Encoding.ASCII.GetString(bytes);

            //Replace spaces 
            value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);

            //Remove invalid chars 
            value = Regex.Replace(value, @"[^\w\s\p{Pd}]", "", RegexOptions.Compiled);

            //Trim dashes from end 
            value = value.Trim('-', '_');

            //Replace double occurences of - or \_ 
            value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            return value;
        }

        public static decimal ConvertMileToMeter(decimal miles)
        {
            return miles * 1609.344m;
        }

        public static decimal ConvertMeterToMile(decimal meters)
        {
            return meters / 1609.344m;
        }

        #endregion

        #region Convert DateTime

        public static DateTime ToDateTime(this object item, DateTime defaultDateTime = default)
        {
            if (string.IsNullOrWhiteSpace(item?.ToString()))
            {
                return defaultDateTime;
            }


            return DateTime.TryParse(item.ToString(), out DateTime result) ? result : defaultDateTime;
        }

        public static DateTime? ToDateTimeNull(this object item)
        {
            if (string.IsNullOrWhiteSpace(item?.ToString()))
            {
                return null;
            }


            return DateTime.TryParse(item.ToString(), out DateTime result) ? result : (DateTime?)null;
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

        public static (DateTime? From, DateTime? To) RangeDateTimeUtc(this (DateTime? From, DateTime? To) source, string timeZoneId)
        {
            var from = source.From.StartDateTimeUtc(timeZoneId);
            var to = source.To.EndDateTimeUtc(timeZoneId);

            return (from, to);
        }

        public static (DateTime From, DateTime To) RangeDateTimeUtc(this (DateTime From, DateTime To) source, string timeZoneId)
        {
            var from = source.From.StartDateTimeUtc(timeZoneId);
            var to = source.To.EndDateTimeUtc(timeZoneId);

            return (from, to);
        }

        public static DateTime StartDateTimeUtc(this DateTime item, string timeZone)
        {
            return item.StartDateTime().ConvertToUtc(timeZone);
        }

        public static DateTime? StartDateTimeUtc(this DateTime? item, string timeZone)
        {
            return item.StartDateTime().ConvertToUtcNull(timeZone);
        }

        public static DateTime StartDateTimeLocal(this DateTime item, string timeZone)
        {
            return item.StartDateTime().ConvertToLocal(timeZone);
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
            return item.EndDateTime().ConvertToUtc(timeZone);
        }

        public static DateTime? EndDateTimeUtc(this DateTime? item, string timeZone)
        {
            return item.EndDateTime().ConvertToUtcNull(timeZone);
        }

        public static DateTime EndDateTimeLocal(this DateTime item, string timeZone)
        {
            return item.EndDateTime().ConvertToLocal(timeZone);
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
            return DateTime.UtcNow.ConvertToLocal(timeZone);
        }

        public static DateTime TodayStartDateUtc(string timeZone)
        {
            return TodayStartDateLocal(timeZone).ConvertToUtc(timeZone);
        }

        public static DateTime TodayStartDateLocal(string timeZone)
        {
            return NowLocal(timeZone).StartDateTime();
        }

        public static DateTime TodayEndDateUtc(string timeZone)
        {
            return TodayEndDateLocal(timeZone).ConvertToUtc(timeZone);
        }

        public static DateTime TodayEndDateLocal(string timeZone)
        {
            return NowLocal(timeZone).EndDateTime();
        }

        public static DateTime ThisWeekStartDateUtc(string timeZone)
        {
            return ThisWeekStartDateLocal(timeZone).ConvertToUtc(timeZone);
        }

        public static DateTime ThisWeekStartDateLocal(string timeZone)
        {
            var localStart = TodayStartDateLocal(timeZone);
            return localStart.AddDays(-(int)localStart.DayOfWeek);
        }

        public static DateTime ThisWeekEndDateUtc(string timeZone, bool fullWeek = false)
        {
            return ThisWeekEndDateLocal(timeZone).ConvertToUtc(timeZone);
        }

        public static DateTime ThisWeekEndDateLocal(string timeZone, bool fullWeek = false)
        {
            var localEnd = TodayEndDateLocal(timeZone);
            return localEnd.AddDays(6 - (int)localEnd.DayOfWeek);
        }

        public static DateTime ThisMonthStartDateUtc(string timeZone)
        {
            return ThisMonthStartDateLocal(timeZone).ConvertToUtc(timeZone);
        }

        public static DateTime ThisMonthStartDateLocal(string timeZone)
        {
            var local = NowLocal(timeZone);
            return new DateTime(local.Year, local.Month, 1, 0, 0, 0);
        }

        public static DateTime ThisMonthEndDateUtc(string timeZone, bool fullMonth = false)
        {
            return ThisMonthEndDateLocal(timeZone, fullMonth).ConvertToUtc(timeZone);
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
            return LastMonthStartDateLocal(timeZone).ConvertToUtc(timeZone);
        }

        public static DateTime LastMonthStartDateLocal(string timeZone)
        {
            return ThisMonthStartDateLocal(timeZone).AddMonths(-1);
        }

        public static DateTime LastMonthEndDateUtc(string timeZone)
        {
            return LastMonthEndDateLocal(timeZone).ConvertToUtc(timeZone);
        }

        public static DateTime LastMonthEndDateLocal(string timeZone)
        {
            return ThisMonthStartDateLocal(timeZone).AddSeconds(-1);
        }

        public static DateTime Last90DaysStartDateUtc(string timeZone)
        {
            return Last90DaysStartDateLocal(timeZone).ConvertToUtc(timeZone);
        }

        public static DateTime Last90DaysStartDateLocal(string timeZone, bool includeNow = false)
        {
            return TodayStartDateLocal(timeZone).AddDays(-90);
        }

        public static DateTime Last90DaysEndDateUtc(string timeZone)
        {
            return Last90DaysEndDateLocal(timeZone).ConvertToUtc(timeZone);
        }

        public static DateTime Last90DaysEndDateLocal(string timeZone)
        {
            return TodayEndDateLocal(timeZone);
        }
        public static DateTime SubWorkdays(this DateTime originalDate, int workDays)
        {
            DateTime tmpDate = originalDate;
            while (workDays > 0)
            {
                tmpDate = tmpDate.AddDays(-1);
                if (tmpDate.DayOfWeek < DayOfWeek.Saturday &&
                    tmpDate.DayOfWeek > DayOfWeek.Sunday &&
                    !tmpDate.IsHoliday())
                    workDays--;
            }
            return tmpDate;
        }
        public static DateTime AddWorkdays(this DateTime originalDate, int workDays)
        {
            DateTime tmpDate = originalDate;
            while (workDays > 0)
            {
                tmpDate = tmpDate.AddDays(1);
                if (tmpDate.DayOfWeek < DayOfWeek.Saturday &&
                    tmpDate.DayOfWeek > DayOfWeek.Sunday &&
                    !tmpDate.IsHoliday())
                    workDays--;
            }
            return tmpDate;
        }

        public static bool IsHoliday(this DateTime date)
        {
            // to ease typing
            int nthWeekDay = (int)(Math.Ceiling((double)date.Day / 7.0d));
            DayOfWeek dayName = date.DayOfWeek;
            bool isThursday = dayName == DayOfWeek.Thursday;
            bool isFriday = dayName == DayOfWeek.Friday;
            bool isMonday = dayName == DayOfWeek.Monday;
            bool isWeekend = dayName == DayOfWeek.Saturday || dayName == DayOfWeek.Sunday;

            // New Years Day (Jan 1, or preceding Friday/following Monday if weekend)
            if ((date.Month == 12 && date.Day == 31 && isFriday) ||
                (date.Month == 1 && date.Day == 1 && !isWeekend) ||
                (date.Month == 1 && date.Day == 2 && isMonday)) return true;

            // MLK day (3rd monday in January)
            if (date.Month == 1 && isMonday && nthWeekDay == 3) return true;

            // President’s Day (3rd Monday in February)
            if (date.Month == 2 && isMonday && nthWeekDay == 3) return true;

            // Memorial Day (Last Monday in May)
            if (date.Month == 5 && isMonday && date.AddDays(7).Month == 6) return true;

            // Independence Day (July 4, or preceding Friday/following Monday if weekend)
            if ((date.Month == 7 && date.Day == 3 && isFriday) ||
                (date.Month == 7 && date.Day == 4 && !isWeekend) ||
                (date.Month == 7 && date.Day == 5 && isMonday)) return true;

            // Labor Day (1st Monday in September)
            if (date.Month == 9 && isMonday && nthWeekDay == 1) return true;

            // Columbus Day (2nd Monday in October)
            if (date.Month == 10 && isMonday && nthWeekDay == 2) return true;

            // Veteran’s Day (November 11, or preceding Friday/following Monday if weekend))
            if ((date.Month == 11 && date.Day == 10 && isFriday) ||
                (date.Month == 11 && date.Day == 11 && !isWeekend) ||
                (date.Month == 11 && date.Day == 12 && isMonday)) return true;

            // Thanksgiving Day (4th Thursday in November)
            if (date.Month == 11 && isThursday && nthWeekDay == 4) return true;

            // Christmas Day (December 25, or preceding Friday/following Monday if weekend))
            if ((date.Month == 12 && date.Day == 24 && isFriday) ||
                (date.Month == 12 && date.Day == 25 && !isWeekend) ||
                (date.Month == 12 && date.Day == 26 && isMonday)) return true;

            return false;
        }

        #endregion

        #region Format Data

        public static string FormatPhoneNumber(this string psUnformattedPhone)
        {
            return Regex.Replace(ToPhoneNumber(psUnformattedPhone),
                                              @"(\d{3})(\d{3})(\d{4})",
                                              @"$1-$2-$3");
        }

        public static string FormatSsn(this string psUnformattedPhone)
        {
            return Regex.Replace(ToSsnNumber(psUnformattedPhone),
                                              @"(\d{3})(\d{2})(\d{4})",
                                              @"$1-$2-$3");
        }

        public static bool IsColorValid(this string source)
        {
            if (!string.IsNullOrWhiteSpace(source))
            {
                if (!source.StartsWith("#"))
                {
                    source = "#" + source;
                }

                return Regex.IsMatch(source, "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$");
            }

            return false;
        }

        public static string FormatFullAddress(string houseNumber, string address, string city, string state, string zipCode)
            => $"{houseNumber} {address}, {city}, {state} {zipCode}".Trim(new[] { ' ', ',' });

        #endregion

        #region Others

        public static string SeparateByComma<T, U>(this IEnumerable<T> source, Func<T, U> func)
        {
            return string.Join(",", source.Select(s => func(s).ToString()).ToArray());
        }

        public static string StripHTML(string psText)
        {
            return Regex.Replace(Regex.Replace(psText, "<[^>]*>", ""), "[\\s\\r\\n]+", " ").Replace("&nbsp;", " ").Trim();
        }

        public static string MainHostName(this string url)
        {
            string[] pattern = DnsPatterns(url);
            if (pattern.Length > 2)
                return string.Join(".", pattern.Skip(pattern.Length - 2).Take(2).ToArray());

            return string.Join(".", pattern);
        }

        public static string[] DnsPatterns(this string url)
        {
            var uriObj = new Uri(url);
            return uriObj.DnsSafeHost.Split('.');
        }

        public static string MainHostNameByHost(this string host)
        {
            var patterns = host.Split('.');
            if (patterns.Length > 2)
                return string.Join(".", patterns.Skip(1).Take(patterns.Length - 1).ToArray());

            return string.Join(".", patterns);
        }

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

        public static string ToFriendlyCase(this string value, string removeText = "")
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "";
            }

            var friendlyCase = Regex.Replace(value, "(?!^)([A-Z])", " $1");

            return string.IsNullOrWhiteSpace(removeText) ? friendlyCase : friendlyCase.Replace(removeText, "");
        }

        public static string ShortString(this string source, int length)
        {
            source = source.ToStr();
            if (source.Length > length)
                return source.Substring(0, SafeWhiteSpaceIndex(source, length - 3)) + "...";

            return source;
        }

        public static int SafeWhiteSpaceIndex(string source, int length, int padding = 5)
        {
            int wsIndex = 0;
            for (int times = 1; times * padding < length; times++)
            {
                wsIndex = source.IndexOf(' ', length - 1 - (times * padding), times * padding);
                if (wsIndex > 0) return wsIndex;
            }

            return length;
        }

        public static bool IsEmail(this string email, string pattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
            => !string.IsNullOrWhiteSpace(email) && Regex.IsMatch(email, pattern);

        public static void AddRange(this MailAddressCollection collection, string emails, char separator = ';')
        {
            collection.AddRange(emails.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries));
        }

        public static void AddRange(this MailAddressCollection collection, IEnumerable<string> emails)
        {
            foreach (var otherEmail in emails.Where(a => a.IsEmail()).Distinct())
            {
                collection.Add(otherEmail);
            }
        }

        public static string ToDomainName(this string text)
        {
            var domain = text.ToStr().ToLower();

            if (domain.StartsWith("http://")) domain = domain.Substring(7);
            if (domain.StartsWith("https://")) domain = domain.Substring(8);
            if (domain.StartsWith("www.")) domain = domain.Substring(4);

            var indexSlash = domain.IndexOf("/");

            if (indexSlash >= 0)
            {
                domain = domain.Substring(0, indexSlash);
            }

            return domain;
        }

        public static string ToDomainName(this Uri url)
        {
            return url.Host;
        }

        public static string ToFullDomainName(this Uri url)
        {
            return url.GetLeftPart(UriPartial.Authority);
        }

        public static string ClientIP(this HttpRequestBase request)
        {
            string ip = null;
            if (request != null)
            {
                if (request.Headers?.Count > 0)
                {
                    ip = request.Headers["X-Forwarded-For"]; // load-balancing
                }
                if (string.IsNullOrWhiteSpace(ip) && request.ServerVariables?.Count > 0)
                {
                    ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
                if (string.IsNullOrWhiteSpace(ip))
                {
                    ip = request.UserHostAddress;
                }
            }

            return ip;
        }

        public static string ClientIP(this HttpRequest request)
        {
            string ip = null;
            if (request != null)
            {
                if (request.Headers?.Count > 0)
                {
                    ip = request.Headers["X-Forwarded-For"]; // load-balancing
                }
                if (string.IsNullOrWhiteSpace(ip) && request.ServerVariables?.Count > 0)
                {
                    ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
                if (string.IsNullOrWhiteSpace(ip))
                {
                    ip = request.UserHostAddress;
                }
            }

            return ip;
        }

        public static string MachineName() => Environment.MachineName;

        public static string MachineIP()
        {
            try
            {
                // from HttpContext
                if (HttpContext.Current?.Request != null)
                {
                    var localIp = HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];
                    if (!string.IsNullOrWhiteSpace(localIp))
                    {
                        return localIp;
                    }
                }

                // from Dns
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
            }
            catch { }

            return null;
        }

        #endregion

        #region Prop

        public static string PropStringValue(this object source, string propName)
        {
            return source.PropValue(propName).ToStr();
        }

        public static object PropValue(this object source, string propName)
        {
            var prop = source.GetType().GetProperty(propName);
            return prop != null ? prop.GetValue(source, null) : null;
        }

        public static bool HasProp(this object source, string propName)
        {
            return source.GetType().GetProperty(propName) != null;
        }

        public static string PropName<T>(Expression<Func<T>> e)
        {
            return MemberInfo(e).Name;
        }

        public static string PropName2<T>(Expression<Func<T, object>> e)
        {
            return PropNameByGeneric(e);
        }

        public static string PropNameByGeneric<T, TMapping>(Expression<Func<T, TMapping>> e)
        {
            return MemberInfo(e).Name;
        }

        public static string PropFullName<T>(Expression<Func<T, object>> expression, string separator = ".")
        {
            var props = PropFullNames(expression);

            return string.Join(separator, props);
        }

        public static List<string> PropFullNames<T>(Expression<Func<T, object>> expression)
        {
            return PropFullNamesByGeneric(expression);
        }

        public static string PropFullNameByGeneric<T, TMapping>(Expression<Func<T, TMapping>> expression, string separator = ".")
        {
            var props = PropFullNamesByGeneric(expression);

            return string.Join(separator, props);
        }

        public static List<string> PropFullNamesByGeneric<T, TMapping>(Expression<Func<T, TMapping>> expression)
        {
            var props = new List<string>();

            MemberExpression me;

            switch (expression.Body.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    var ue = expression.Body as UnaryExpression;
                    me = ((ue != null) ? ue.Operand : null) as MemberExpression;
                    break;

                default:
                    me = expression.Body as MemberExpression;
                    break;
            }

            while (me != null)
            {
                props.Add(me.Member.Name);

                me = me.Expression as MemberExpression;
            }

            props.Reverse();

            return props;
        }

        //public static List<string> PropMemeersByGeneric<T, TMapping>(Expression<Func<T, TMapping>> expression)
        //{
        //    var props = new List<string>();

        //    MemberExpression me;

        //    switch (expression.Body.NodeType)
        //    {
        //        case ExpressionType.Convert:
        //        case ExpressionType.ConvertChecked:
        //            var ue = expression.Body as UnaryExpression;
        //            me = ((ue != null) ? ue.Operand : null) as MemberExpression;
        //            break;

        //        default:
        //            me = expression.Body as MemberExpression;
        //            break;
        //    }

        //    while (me != null)
        //    {
        //        props.Add(me.Member.Name);

        //        me = me.Expression as MemberExpression;
        //    }

        //    props.Reverse();

        //    return props;
        //}

        public static PropertyInfo PropertyInfo(Expression method)
        {
            return (PropertyInfo)MemberInfo(method);
        }

        public static MemberInfo MemberInfo(Expression method)
        {
            var lambda = method as LambdaExpression;

            if (lambda == null)
            {
                throw new ArgumentNullException("method");
            }

            MemberExpression memberExpr = null;

            switch (lambda.Body.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    memberExpr = ((UnaryExpression)lambda.Body).Operand as MemberExpression;
                    break;

                case ExpressionType.MemberAccess:
                    memberExpr = lambda.Body as MemberExpression;
                    break;
            }

            if (memberExpr == null)
            {
                throw new ArgumentException("method");
            }

            return memberExpr.Member;
        }

        public static bool TryGetAttribute<TAttribute>(this MemberInfo member, out TAttribute attribute) where TAttribute : Attribute
        {
            attribute = member.GetCustomAttribute<TAttribute>();
            return attribute != null;
        }

        #endregion

        #region Common methods

        public static object IIf(this bool expression, object @true, object @false)
        {
            return expression ? @true : @false;
        }

        public static async Task<T> IIfAsync<T>(this bool expression, Func<Task<T>> @true, Func<Task<T>> @false = null)
        {
            if (expression)
            {
                if (@true != null)
                {
                    return await @true();
                }

                return default;
            }

            if (@false != null)
            {
                return await @false();
            }

            return default;
        }

        public static T IIf<T>(this bool expression, Func<T> @true, Func<T> @false = null)
        {
            if (expression)
            {
                return @true != null ? @true() : default;
            }

            return @false != null ? @false() : default;
        }

        public static void IIf(this bool expression, Action @true, Action @false = null)
        {
            if (expression)
            {
                @true?.Invoke();
            }
            else
            {
                @false?.Invoke();
            }
        }

        #endregion

        #region Sql

        public static DateTime? ToSqlDateTime(this DateTime? source)
        {
            if (source == null) return null;
            if (source < SqlMinDate) return null;
            if (source > SqlMaxDate) return null;

            return source;
        }

        public static DateTime ToSqlDateTime(this DateTime source)
        {
            if (source < SqlMinDate) return SqlMinDate;
            if (source > SqlMaxDate) return SqlMaxDate;

            return source;
        }

        public static DateTime SqlMinDate = new DateTime(1753, 1, 1);

        public static DateTime SqlMaxDate = new DateTime(9999, 12, 31, 23, 59, 59);

        #endregion

        #region XQuery

        public static string ToXQueryDateTime(this DateTime? source)
        {
            if (source == null)
            {
                return string.Empty;
            }

            var t = source.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");

            //http://msdn.microsoft.com/en-us/library/8kb3ddd4.aspx#KSpecifier
            return source.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
        }

        public static string ToXQueryBool(this bool? source)
        {
            if (source == null)
            {
                return string.Empty;
            }

            return source.Value.ToString().ToLower();
        }

        #endregion

        #region Fantasy Data

        public static string ToShortMonthName(this DateTime date)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(date.Month);
        }

        public static string ToMonthName(this DateTime date)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month);
        }

        public static string ToApiDate(this DateTime date)
        {
            return string.Format("{0}-{1}-{2:D2}", date.Year, date.ToShortMonthName().ToUpper(), date.Day);
        }

        public static DateTime? ConvertToUtcNull(this DateTime? source, string timeZoneId)
            => DateTimeHelper.ToUtcNull(source, timeZoneId);

        public static DateTime? ConvertToLocalNull(this DateTime? source, string timeZoneId)
            => DateTimeHelper.ToLocalNull(source, timeZoneId);

        public static DateTime ConvertToUtc(this DateTime source, string timeZoneId)
            => DateTimeHelper.ToUtc(source, timeZoneId);

        public static DateTime ConvertToLocal(this DateTime source, string timeZoneId)
            => DateTimeHelper.ToLocal(source, timeZoneId);

        public static DateTime ConvertByTimeZone(this DateTime source, string fromTimeZoneId, string toTimeZoneId)
            => DateTimeHelper.ToDateByTimeZone(source, fromTimeZoneId, toTimeZoneId);

        public static DateTime JumpToDayOfWeek(this DateTime source, DayOfWeek day)
            => DateTimeHelper.JumpToDayOfWeek(source, day);

        #endregion

        #region Json

        public static JObject DeserializeJson(string json, bool throwExceptionIfError = false)
        {
            if (throwExceptionIfError)
            {
                return (JObject)JsonConvert.DeserializeObject(json);
            }

            return (JObject)JsonConvert.DeserializeObject(json, new JsonSerializerSettings
            {
                Error = (sender, args) => args.ErrorContext.Handled = true
            });
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

            double deg2rad(double deg) => (deg * Math.PI / 180.0);

            double rad2deg(double rad) => (rad / Math.PI * 180.0);
        }

        #endregion

        #region PluralizationService

        private static PluralizationService _pluralizationService;

        public static PluralizationService PluralizationService => _pluralizationService ?? (_pluralizationService = PluralizationService.CreateService(new CultureInfo("en-US")));

        public static string GetPluzalName(string name) => PluralizationService.Pluralize(name);

        #endregion

    }
}
