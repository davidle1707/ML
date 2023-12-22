using RestSharp;
using RestSharp.Validation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ML.Utils.Phone.Vendors.Twilio
{
    /*
        Possible POST or PUT Response Status Codes
            200 OK: The request was successful, we updated the resource and the response body contains the representation.
            201 CREATED: The request was successful, we created a new resource and the response body contains the representation.
            400 BAD REQUEST: The data given in the POST or PUT failed validation. Inspect the response body for details.
            401 UNAUTHORIZED: The supplied credentials, if any, are not sufficient to create or update the resource.
            404 NOT FOUND: You know this one.
            405 METHOD NOT ALLOWED: You can't POST or PUT to the resource.
            500 SERVER ERROR: We couldn't create or update the resource. Please try again.
    */
    public partial class TwilioManager
    {

        /// <summary>
        /// Send a new SMS message to the specified recipients.
        /// Makes a POST request to the SMSMessages List resource.
        /// </summary>
        public SMSMessage SendSmsMessage(string to, string body)
        {
            return SendSmsMessageAsync(Setting.From, to, body, string.Empty).Result;
        }

        /// <summary>
        /// Send a new SMS message to the specified recipients.
        /// Makes a POST request to the SMSMessages List resource.
        /// </summary>
        public Task<SMSMessage> SendSmsMessageAsync(string to, string body)
        {
            return SendSmsMessageAsync(Setting.From, to, body, string.Empty);
        }

        /// <summary>
        /// Send a new SMS message to the specified recipients
        /// Makes a POST request to the SMSMessages List resource.
        /// </summary>
        public SMSMessage SendSmsMessage(string from, string to, string body)
        {
            return SendSmsMessageAsync(from, to, body, string.Empty).Result;
        }

        /// <summary>
        /// Send a new SMS message to the specified recipients
        /// Makes a POST request to the SMSMessages List resource.
        /// </summary>
        public Task<SMSMessage> SendSmsMessageAsync(string from, string to, string body)
        {
            return SendSmsMessageAsync(from, to, body, string.Empty);
        }

        public SMSMessage SendSmsMessage(string from, string to, string body, string statusCallback)
        {
            return SendSmsMessageAsync(from, to, body, statusCallback).Result;
        }

        public async Task<SMSMessage> SendSmsMessageAsync(string from, string to, string body, string statusCallback)
        {
            Validate.IsValidLength(body, 160);
            Require.Argument("from", from);
            Require.Argument("to", to);
            Require.Argument("body", body);

            var request = new RestRequest(Method.POST) { Resource = "Accounts/{AccountSid}/SMS/Messages" };
            //request.RootElement = "TwilioResponse";
            request.AddParameter("From", from);
            request.AddParameter("To", to);
            request.AddParameter("Body", body);

            if (!string.IsNullOrEmpty(statusCallback))
            {
                request.AddParameter("StatusCallback", statusCallback);
            }

            var response = await _client.ExecuteTaskAsync<SMSMessage>(request);

            return response.Data;
        }

        public SMSMessage GetMessage(string messageId)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "Accounts/{AccountSid}/Messages/{MessageSid}";
            request.AddParameter("MessageSid", messageId, ParameterType.UrlSegment);

            var response = _client.Execute<SMSMessage>(request);

            return response.Data;
        }

        public List<Message> GetListMessage(string from, string to, DateTime? dateSent)
        {
            //Require.Argument("from", from);
            //Require.Argument("to", to);
            //Require.Argument("dateSent", dateSent);

            var request = new RestRequest(Method.GET);
            request.Resource = "Accounts/{AccountSid}/Messages";

            if (!string.IsNullOrEmpty(to))
                request.AddParameter("To", to);

            if (!string.IsNullOrEmpty(from))
                request.AddParameter("From", from);

            if (dateSent.HasValue)
                request.AddParameter("DateSent", dateSent);

            var response = _client.Execute<List<Message>>(request);

            return response.Data;
        }

    }
}
