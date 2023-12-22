using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ML.Common
{
    public static class DateTimeHelper
    {
        

        // refer WindowsZones.xml to convert to Olson timezones

        // time zone windows
        public static List<TimeZoneInfo> TimeZones()
        {
            var timezones = new List<TimeZoneInfo>(TimeZoneInfo.GetSystemTimeZones());
            timezones.Sort(delegate(TimeZoneInfo left, TimeZoneInfo right)
            {
                //Fix the crappy default sort order
                int comparison = left.BaseUtcOffset.CompareTo(right.BaseUtcOffset);
                return comparison == 0
                    ? string.CompareOrdinal(left.DisplayName, right.DisplayName)
                    : comparison;
            });

            return timezones;
        }

        // time zone windows
        public static string GetTimeZoneId(int timeZone)
        {
            switch (timeZone)
            {
                case -10:
                    return "Hawaiian Standard Time";
                case -9:
                    return "Alaskan Standard Time";
                case -8:
                    return "Pacific Standard Time";
                case -7:
                    return "Mountain Standard Time";
                case -6:
                    return "Central Standard Time";
                case -5:
                    return "Eastern Standard Time";
                case 7:
                    return "SE Asia Standard Time";
            }

            var timeZoneInfo = TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(c => c.BaseUtcOffset.Hours == timeZone);

            return timeZoneInfo != null ? timeZoneInfo.Id : TimeZone.CurrentTimeZone.StandardName;
        }

        public static bool TryToUtc(this DateTime source, string timeZoneId, out DateTime utc)
        {
            utc = DateTime.MinValue;
            var tmpUtc = ToUtcNull(source, timeZoneId);

            if (tmpUtc != null)
            {
                utc = tmpUtc.Value;
                return true;
            }

            return false;
        }

        public static DateTime? ToUtcNull(this DateTime? source, string timeZoneId)
        {
            return source != null ? ToUtc(source.Value, timeZoneId) : (DateTime?)null;
        }

        public static DateTime ToUtc(DateTime source, string timeZoneId)
        {
            if (source.Kind == DateTimeKind.Utc)
            {
                return source;
            }

            var userDateTime = source.Kind == DateTimeKind.Local ? DateTime.SpecifyKind(source, DateTimeKind.Unspecified) : source;

            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(userDateTime, timeZoneId, TimeZoneInfo.Utc.Id);
        }

        public static bool TryToLocal(this DateTime? source, string timeZoneId, out DateTime local)
        {
            local = DateTime.MinValue;
            var tmpLocal = ToLocalNull(source, timeZoneId);

            if (tmpLocal != null)
            {
                local = tmpLocal.Value;
                return true;
            }

            return false;
        }

        public static DateTime? ToLocalNull(this DateTime? source, string timeZoneId) 
            => source != null ? ToLocal(source.Value, timeZoneId) : (DateTime?)null;

        public static DateTime ToLocal(this DateTime source, string timeZoneId)
        {
            var userDateTime = DateTime.SpecifyKind(source, DateTimeKind.Utc);
            var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            return TimeZoneInfo.ConvertTimeFromUtc(userDateTime, userTimeZone);
        }

        public static DateTime ToDateByTimeZone(this DateTime source, string fromTimeZoneId, string toTimeZoneId)
        {
            var fromUtc = ToUtc(source, fromTimeZoneId);

            return ToLocal(fromUtc, toTimeZoneId);
        }

        public static TimeSpan TimeZoneOffset(string timeZoneId)
        {
            TimeZoneInfo timeZone;

            return !string.IsNullOrEmpty(timeZoneId) && (timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId)) != null
                ? timeZone.BaseUtcOffset
                : new TimeSpan(0);
        }

        public static DateTime StartDate(this DateTime date) 
            => new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

        public static DateTime EndDate(this DateTime date) 
            => new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);

        public static DateTime[] WeekDays(int year, int week, string cultureName = "en-US")
        {
            var start = FirstDateOfWeek(year, week, cultureName);
            return Enumerable.Range(0, 7).Select(num => start.AddDays(num)).ToArray();
        }

        public static DateTime[] WeekDays(this DateTime time, string cultureName = "en-US")
        {
            var start = FirstDateOfWeek(time, cultureName);
            return Enumerable.Range(0, 7).Select(num => start.AddDays(num)).ToArray();
        }

        public static DateTime FirstDateOfWeek(int year, int week, string cultureName = "en-US")
        {
            var dt = new DateTime(year, 1, 1);
            if (WeekOfYear(dt, cultureName) == 1) dt = dt.AddDays(-7);

            return FirstDateOfWeek(dt.AddDays(week * 7), cultureName);
        }

        public static DateTime FirstDateOfWeek(this DateTime date, string cultureName = "en-US")
        {
			var culture = CultureInfo(cultureName);

            return date.Date.AddDays(-((7 + date.Date.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek) % 7));
        }

        public static int WeekOfYear(this DateTime time, string cultureName = "en-US")
        {
			var culture = CultureInfo(cultureName);
            var format = culture.DateTimeFormat;

            return culture.Calendar.GetWeekOfYear(time, format.CalendarWeekRule, format.FirstDayOfWeek);
        }

        public static DateTime JumpToDayOfWeek(this DateTime source, DayOfWeek day)
        {
            if (source.DayOfWeek == day)
            {
                source = source.AddDays(2);
            }

            int daysToAdd = ((int)day - (int)source.DayOfWeek + 7) % 7;

            return source.AddDays(daysToAdd);
        }

        public static CultureInfo CultureInfo(string cultureName = "en-US")
	    {
            var name = cultureName.ToLower();

            if (!_cacheCultureInfos.ContainsKey(name))
            {
                return (_cacheCultureInfos[name] = System.Globalization.CultureInfo.CreateSpecificCulture(cultureName));
            }

			return _cacheCultureInfos[name];
	    }

        public static DateTime GetStartDateExcludeSpecialDays(DateTime endDate, int rangeHours)
        {
            var startHours = GetStartHoursExcludeSpecialDays(endDate, rangeHours);
            return endDate.AddHours(-startHours);
        }

        public static int GetStartHoursExcludeSpecialDays(DateTime endDate, int rangeHours)
        {
            var startDate = endDate.AddHours(-rangeHours);
            while (IsSpecialDay(startDate))
            {
                startDate = startDate.AddDays(-1);
            }

            var countSpecialDays = CountSpecialDays(startDate, endDate);

            return rangeHours + countSpecialDays * 24;
        }

        public static DateTime GetEndDateExcludeSpecialDays(DateTime startDate, int rangeHours)
        {
            var endHours = GetEndHoursExcludeSpecialDays(startDate, rangeHours);
            return startDate.AddHours(endHours);
        }

        public static int GetEndHoursExcludeSpecialDays(DateTime startDate, int rangeHours)
        {
            var endDate = startDate.AddHours(rangeHours);
            while (IsSpecialDay(endDate))
            {
                endDate = endDate.AddDays(1);
            }

            var countSpecialDays = CountSpecialDays(startDate, endDate);
            
            return rangeHours + countSpecialDays * 24;
        }

        public static readonly string[] SpecialDays = new[] { "Saturday", "Sunday", "01/01", "01/16", "01/20", "02/17", "05/25", "05/29", "11/25", "12/24", "12/25" };

        public static int CountSpecialDays(DateTime startDate, DateTime endDate)
        {
            var count = 0;
            var totalDays = (int)Math.Ceiling((endDate - startDate).TotalDays);

            for (var day = 0; day < totalDays; day++)
            {
                var tmp = startDate.AddDays(day);
                if (IsSpecialDay(tmp))
                {
                    count++;
                }
            }

            return count;
        }

        public static bool IsSpecialDay(DateTime date)
        {
            var dayOfWeek = date.DayOfWeek.ToString();
            var shortDate = date.ToString("MM/dd");

            return SpecialDays.Contains(dayOfWeek) || SpecialDays.Contains(shortDate);
        }

        private static Dictionary<string, CultureInfo> _cacheCultureInfos = new Dictionary<string, CultureInfo>();
    }
}
