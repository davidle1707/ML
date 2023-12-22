using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLXamarin.Common
{
    public interface IDateTimeHelper
    {
        DateTime? ConvertToUtcNull(DateTime? source, string timeZoneId);

        DateTime? ConvertToLocalNull(DateTime? source, string timeZoneId);

        DateTime ConvertToUtc(DateTime source, string timeZoneId);

        DateTime ConvertToLocal(DateTime source, string timeZoneId);

        DateTime ConvertByTimeZone(DateTime source, string fromTimeZoneId, string toTimeZoneId);
    }
}
