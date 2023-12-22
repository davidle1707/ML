using System;

namespace ML.Utils.Phone
{
    [Serializable]
    public class OrderPhoneResponse
    {
        public string ExternalId { get; set; }

        public bool Success { get; set; }

        public string Description { get; set; }
    }
}
