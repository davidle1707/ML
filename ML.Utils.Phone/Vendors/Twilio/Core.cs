using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using RestSharp.Validation;
using RestSharp.Extensions;

namespace ML.Utils.Phone.Vendors.Twilio
{
    public static class Core
    {
        public static void AddNumberSearchParameters(AvailablePhoneNumberListRequest options, RestRequest request)
        {
            // some check for null. in those cases an empty string is a valid value (to remove a URL assignment)

            if (options.AreaCode.HasValue()) request.AddParameter("AreaCode", options.AreaCode);
            if (options.Contains.HasValue()) request.AddParameter("Contains", options.Contains);
            if (options.Distance.HasValue) request.AddParameter("Distance", options.Distance);
            if (options.InLata.HasValue()) request.AddParameter("InLata", options.InLata);
            if (options.InPostalCode.HasValue()) request.AddParameter("InPostalCode", options.InPostalCode);
            if (options.InRateCenter.HasValue()) request.AddParameter("InRateCenter", options.InRateCenter);
            if (options.InRegion.HasValue()) request.AddParameter("InRegion", options.InRegion);
            if (options.NearLatLong.HasValue()) request.AddParameter("NearLatLong", options.NearLatLong);
            if (options.NearNumber.HasValue()) request.AddParameter("NearNumber", options.NearNumber);
        }

        public static void AddPhoneNumberOptionsToRequest(RestRequest request, PhoneNumberOptions options)
        {
            // some check for null. in those cases an empty string is a valid value (to remove a URL assignment)

            if (options.FriendlyName.HasValue())
            {
                Validate.IsValidLength(options.FriendlyName, 64);
                request.AddParameter("FriendlyName", options.FriendlyName);
            }
            if (options.VoiceApplicationSid != null) request.AddParameter("VoiceApplicationSid", options.VoiceApplicationSid);
            if (options.VoiceUrl != null) request.AddParameter("VoiceUrl", options.VoiceUrl);
            if (options.VoiceMethod.HasValue()) request.AddParameter("VoiceMethod", options.VoiceMethod);
            if (options.VoiceFallbackUrl != null) request.AddParameter("VoiceFallbackUrl", options.VoiceFallbackUrl);
            if (options.VoiceFallbackMethod.HasValue()) request.AddParameter("VoiceFallbackMethod", options.VoiceFallbackMethod);
            if (options.VoiceCallerIdLookup.HasValue) request.AddParameter("VoiceCallerIdLookup", options.VoiceCallerIdLookup.Value);
            if (options.StatusCallback.HasValue()) request.AddParameter("StatusCallback", options.StatusCallback);
            if (options.StatusCallbackMethod.HasValue()) request.AddParameter("StatusCallbackMethod", options.StatusCallbackMethod);
        }

        public static void AddSmsOptionsToRequest(RestRequest request, PhoneNumberOptions options)
        {
            // some check for null. in those cases an empty string is a valid value (to remove a URL assignment)
            if (options.SmsApplicationSid != null) request.AddParameter("SmsApplicationSid", options.SmsApplicationSid);
            if (options.SmsUrl != null) request.AddParameter("SmsUrl", options.SmsUrl);
            if (options.SmsMethod.HasValue()) request.AddParameter("SmsMethod", options.SmsMethod);
            if (options.SmsFallbackUrl != null) request.AddParameter("SmsFallbackUrl", options.SmsFallbackUrl);
            if (options.SmsFallbackMethod.HasValue()) request.AddParameter("SmsFallbackMethod", options.SmsFallbackMethod);
        }

        public static void AddApplicatiionOptionsToRequest(RestRequest request, ApplicationOptions options)
        {
            // some check for null. in those cases an empty string is a valid value (to remove a URL assignment)

            if (options.FriendlyName.HasValue())
            {
                Validate.IsValidLength(options.FriendlyName, 64);
                request.AddParameter("FriendlyName", options.FriendlyName);
            }
            if (options.VoiceUrl != null) request.AddParameter("VoiceUrl", options.VoiceUrl);
            if (options.VoiceMethod.HasValue()) request.AddParameter("VoiceMethod", options.VoiceMethod);
            if (options.VoiceFallbackUrl != null) request.AddParameter("VoiceFallbackUrl", options.VoiceFallbackUrl);
            if (options.VoiceFallbackMethod.HasValue()) request.AddParameter("VoiceFallbackMethod", options.VoiceFallbackMethod);
            if (options.VoiceCallerIdLookup.HasValue) request.AddParameter("VoiceCallerIdLookup", options.VoiceCallerIdLookup.Value);
            if (options.StatusCallback.HasValue()) request.AddParameter("StatusCallback", options.StatusCallback);
            if (options.StatusCallbackMethod.HasValue()) request.AddParameter("StatusCallbackMethod", options.StatusCallbackMethod);
            if (options.SmsUrl != null) request.AddParameter("SmsUrl", options.SmsUrl);
            if (options.SmsMethod.HasValue()) request.AddParameter("SmsMethod", options.SmsMethod);
            if (options.SmsFallbackUrl != null) request.AddParameter("SmsFallbackUrl", options.SmsFallbackUrl);
            if (options.SmsFallbackMethod.HasValue()) request.AddParameter("SmsFallbackMethod", options.SmsFallbackMethod);
        }
    }
}
