using System;
using System.Collections.Generic;

namespace ML.Utils.Phone
{
    [Serializable]
    public class GetPhoneNumberResponse
    {
        public GetPhoneNumberResponse()
        {
            PhoneNumbers = new List<AvailablePhoneNumber>();
        }

        public List<AvailablePhoneNumber> PhoneNumbers { get; set; }

        public int TotalPages { get; set; }

        public bool Success { get; set; }

        public string Description { get; set; }
    }
}
