using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML.Utils.Phone.Vendors.Twilio
{
    public class IncomingPhoneNumberResult : TwilioListBase
    {
        /// <summary>
        /// List of IncomingPhoneNumber instances returned by API
        /// </summary>
        public List<IncomingPhoneNumber> IncomingPhoneNumbers { get; set; }
    }
}
