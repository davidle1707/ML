using System;

namespace MLXamarin.Common.Shared
{
    public class SharedDateTimeHelper
    {
        public DateTime? ConvertToUtcNull(DateTime? source, string timeZoneId)
        {
            if (source == null)
            {
                return null;
            }

            return ConvertToUtc(source.Value, timeZoneId);
        }

        public DateTime? ConvertToLocalNull(DateTime? source, string timeZoneId)
        {
            if (source == null)
            {
                return null;
            }

            return ConvertToLocal(source.Value, timeZoneId);
        }

        public DateTime ConvertToUtc(DateTime source, string timeZoneId)
        {
            if (source.Kind == DateTimeKind.Utc)
            {
                return source;
            }

            var userDateTime = source.Kind == DateTimeKind.Local ? DateTime.SpecifyKind(source, DateTimeKind.Unspecified) : source;

            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(userDateTime, ConvertTimeZoneId(timeZoneId), TimeZoneInfo.Utc.Id);
        }

        public DateTime ConvertToLocal(DateTime source, string timeZoneId)
        {
            var userDateTime = DateTime.SpecifyKind(source, DateTimeKind.Utc);
            var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(ConvertTimeZoneId(timeZoneId));

            return TimeZoneInfo.ConvertTimeFromUtc(userDateTime, userTimeZone);
        }

        public DateTime ConvertByTimeZone(DateTime source, string fromTimeZoneId, string toTimeZoneId)
        {
            var fromUtc = ConvertToUtc(source, fromTimeZoneId);

            return ConvertToLocal(fromUtc, toTimeZoneId);
        }

        private string ConvertTimeZoneId(string standardTimeZoneId)
        {
            switch (standardTimeZoneId)
            {
                case "SE Asia Standard Time":
                    return "Asia/Bangkok";

                case "Pacific Standard Time":
                    return "US/Pacific";

                case "Eastern Standard Time":
                    return "US/Eastern";

                case "Central Standard Time":
                    return "US/Central";

                case "Mountain Standard Time":
                    return "US/Mountain";

                case "Alaskan Standard Time":
                    return "US/Alaska";

                case "Hawaiian Standard Time":
                    return "US/Hawaii";

                default:
                    return standardTimeZoneId;
            }
        }
    }
}
