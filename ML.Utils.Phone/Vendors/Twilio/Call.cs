using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML.Utils.Phone.Vendors.Twilio
{
    public class Call : TwilioBase
    {
        /// <summary>
        /// A 34 character string that uniquely identifies this resource.
        /// </summary>
        public string Sid { get; set; }
        /// <summary>
        /// A 34 character string that uniquely identifies the call that created this leg.
        /// </summary>
        public string ParentCallSid { get; set; }
        /// <summary>
        /// The date that this resource was created, given as GMT
        /// </summary>
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// The date that this resource was last updated, given as GMT 
        /// </summary>
        public DateTime DateUpdated { get; set; }
        /// <summary>
        /// The unique Sid of the Account responsible for creating this call.
        /// </summary>
        public string AccountSid { get; set; }
        /// <summary>
        /// The phone number that received this call. e.g., +16175551212 (E.164 format)
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// The phone number that made this call. e.g., +16175551212 (E.164 format)
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// If the call was inbound, this is the Sid of the IncomingPhoneNumber that received the call. If the call was outbound, it is the Sid of the OutgoingCallerId from which the call was placed.
        /// </summary>
        public string PhoneNumberSid { get; set; }
        /// <summary>
        /// A string representing the status of the call. May be queued, ringing, in-progress, completed, failed, busy or no-answer.
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// The start time of the call, given as GMT in RFC 2822 format. Empty if the call has not yet been dialed.
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// The end time of the call, given as GMT in RFC 2822 format. Empty if the call did not complete successfully.
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// The length of the call in seconds. This value is empty for busy, failed, unanswered or ongoing calls.
        /// </summary>
        public int? Duration { get; set; }
        /// <summary>
        /// The charge for this call in USD. Populated after the call is completed. May not be immediately available.
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// A string describing the direction of the call. inbound for inbound calls, outbound-api for calls initiated via the REST API or outbound-dial for calls initiated by a Dial verb.
        /// </summary>
        public string Direction { get; set; }
        /// <summary>
        /// If this call was initiated with answering machine detection, either human or machine. Empty otherwise.
        /// </summary>
        public string AnsweredBy { get; set; }
        /// <summary>
        /// If this call was an incoming call forwarded from another number, the forwarding phone number (depends on carrier supporting forwarding). Empty otherwise.
        /// </summary>
        public string ForwardedFrom { get; set; }
        /// <summary>
        /// If this call was an incoming call from a phone number with Caller ID Lookup enabled, the caller's name. Empty otherwise.
        /// </summary>
        public string CallerName { get; set; }
    }

    /// <summary>
    /// Twilio API call result with paging information
    /// </summary>
    public class CallResult : TwilioListBase
    {
        /// <summary>
        /// List of Calls returned from API request
        /// </summary>
        public List<Call> Calls { get; set; }
    }

    /// <summary>
    /// Search filter options for Call list request
    /// </summary>
    public class CallListRequest
    {
        /// <summary>
        /// Only show calls from this phone number.
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// Only show calls to this phone number.
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// Only show calls currently in this status. May be queued, ringing, in-progress, completed, failed, busy, or no-answer.
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Only show calls that started on this date
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// Comparison type for start time
        /// </summary>
        public ComparisonType StartTimeComparison { get; set; }
        /// <summary>
        /// Only show calls that ended on this date
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// Comparison type for end time
        /// </summary>
        public ComparisonType EndTimeComparison { get; set; }
        /// <summary>
        /// What page number to start retrieving results from
        /// </summary>
        public int? PageNumber { get; set; }
        /// <summary>
        /// How many results to retrieve
        /// </summary>
        public int? Count { get; set; }
        /// <summary>
        /// Only show calls that belong to this parent call (e.g. Dial legs)
        /// </summary>
        public string ParentCallSid { get; set; }
    }

    /// <summary>
    /// Available types of range selections
    /// </summary>
    public enum ComparisonType
    {
        /// <summary>
        /// Selects items equal to value
        /// </summary>
        EqualTo,
        /// <summary>
        /// Selects items greater than or equal to value
        /// </summary>
        GreaterThanOrEqualTo,
        /// <summary>
        /// Selects items less than or equal to value
        /// </summary>
        LessThanOrEqualTo
    }

    public class CallOptions
    {
        /// <summary>
        /// The phone number to use as the caller id. Format with a '+' and country code e.g., +16175551212 (E.164 format). Must be a Twilio number or a valid outgoing caller id for your account.
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// The number to call formatted with a '+' and country code e.g., +16175551212 (E.164 format). Twilio will also accept unformatted US numbers e.g., (415) 555-1212, 415-555-1212.
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// The fully qualified URL that should be consulted when the call connects. Just like when you set a URL for your inbound calls.
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// The 34 character sid of the application Twilio should use to handle this phone call. If this parameter is present, Twilio will ignore all of the voice URLs passed and use the URLs set on the application.
        /// </summary>
        public string ApplicationSid { get; set; }
        /// <summary>
        /// A URL that Twilio will request when the call ends to notify your app.
        /// </summary>
        public string StatusCallback { get; set; }
        /// <summary>
        /// The HTTP method Twilio should use when requesting the above URL. Defaults to POST.
        /// </summary>
        public string StatusCallbackMethod { get; set; }
        /// <summary>
        /// The HTTP method Twilio should use when requesting the required Url parameter's value above. Defaults to POST.
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// A string of keys to dial after connecting to the number. Valid digits in the string include: any digit (0-9), '#' and '*'. For example, if you connected to a company phone number, and wanted to dial extension 1234 and then the pound key, use SendDigits=1234#. Remember to URL-encode this string, since the '#' character has special meaning in a URL.
        /// </summary>
        public string SendDigits { get; set; }
        /// <summary>
        /// Tell Twilio to try and determine if a machine (like voicemail) or a human has answered the call. Possible values are Continue and Hangup.
        /// </summary>
        public string IfMachine { get; set; }
        /// <summary>
        /// The integer number of seconds that Twilio should allow the phone to ring before assuming there is no answer. Default is 60 seconds, the maximum is 999 seconds. Note, you could set this to a low value, such as 15, to hangup before reaching an answering machine or voicemail.
        /// </summary>
        public int? Timeout { get; set; }
        /// <summary>
        /// A URL that Twilio will request if an error occurs requesting or executing the TwiML at Url.
        /// </summary>
        public string FallbackUrl { get; set; }
        /// <summary>
        /// The HTTP method that Twilio should use to request the FallbackUrl. Must be either GET or POST. Defaults to POST.
        /// </summary>
        public string FallbackMethod { get; set; }
        /// <summary>
        /// Set this parameter to 'true' to record the entirety of a phone call. The RecordingUrl will be sent to the StatusCallback URL. Defaults to 'false'.
        /// </summary>
        public bool Record { get; set; }
    }

    /// <summary>
    /// Options for handling call hangups
    /// </summary>
    public enum HangupStyle
    {
        /// <summary>
        ///  Specifying canceled will attempt to hangup calls that are queued or ringing but not affect calls already in progress.
        /// </summary>
        Canceled,
        /// <summary>
        /// Specifying completed will attempt to hang up a call even if it's already in progress.
        /// </summary>
        Completed
    }

}
