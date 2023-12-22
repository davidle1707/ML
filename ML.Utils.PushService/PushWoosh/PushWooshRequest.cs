namespace ML.Utils.PushService.PushWoosh
{
    public class CreateMessageRequest
    {
        public string SendDate{ get; set; }

        public bool IgnoreUserTimezone { get; set; }

        public string Content { get; set; }

        public string TimeZone { get; set; } // http://php.net/manual/en/timezones.php
    }

    public class DeleteMessageRequest
    {
        public string MessageCode { get; set; }
    }
}
