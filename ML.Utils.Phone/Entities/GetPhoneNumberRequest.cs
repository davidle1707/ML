using System;

namespace ML.Utils.Phone
{
    [Serializable]
    public class GetPhoneNumberRequest
    {
        public string CountryCode { get; set; }

        public string StateCode { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsTollFree { get; set; }

        public int Page { get; set; }
    }
}
