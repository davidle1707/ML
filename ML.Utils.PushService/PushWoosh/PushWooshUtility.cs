using System;

namespace ML.Utils.PushService.PushWoosh
{
    public static class PushWooshUtility
    {
        public static string SendNow = "now";

        public static string SendDate(DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm");
        }
    }
}
