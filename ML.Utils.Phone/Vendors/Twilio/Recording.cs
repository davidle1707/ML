﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML.Utils.Phone.Vendors.Twilio
{
    public class Recording : TwilioBase
    {
        /// <summary>
        /// A 34 character string that uniquely identifies this resource.
        /// </summary>
        public string Sid { get; set; }
        /// <summary>
        /// The date that this resource was created
        /// </summary>
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// The date that this resource was last updated
        /// </summary>
        public DateTime DateUpdated { get; set; }
        /// <summary>
        /// The unique id of the Account responsible for this recording.
        /// </summary>
        public string AccountSid { get; set; }
        /// <summary>
        /// The call during which the recording was made.
        /// </summary>
        public string CallSid { get; set; }
        /// <summary>
        /// The length of the recording, in seconds.
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// The version of the API in use during the recording.
        /// </summary>
        public string ApiVersion { get; set; }
    }

    public class RecordingResult : TwilioListBase
    {
        /// <summary>
        /// List of Recording instances returned by API
        /// </summary>
        public List<Recording> Recordings { get; set; }
    }
}
