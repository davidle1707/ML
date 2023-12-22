using System;
using System.Collections.Generic;

namespace ML.Utils.Phone.Vendors.Twilio
{
    /// <summary>
    /// Twilio API call result with paging information
    /// </summary>
    [Serializable]
    public class ApplicationResult : TwilioListBase
    {
        /// <summary>
        /// List of Application instances returned by API
        /// </summary>
        public List<Application> Applications { get; set; }
    }
}
