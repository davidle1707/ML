using ML.Utils.Phone.Vendors.Plivo.Models;
using RestSharp;
using System;
using dict = System.Collections.Generic.Dictionary<string, string>;

namespace ML.Utils.Phone.Vendors.Plivo
{
    public partial class PlivoManager
    {
		public IRestResponse<IncomingCarrierList> GetIncomingCarriers(dict parameters)
		{
			return ExecuteRequest<IncomingCarrierList>("GET", "/IncomingCarrier/", parameters);
		}

		public IRestResponse<IncomingCarrier> GetIncomingCarrier(dict parameters)
		{
			var carrierId = GetValueAndRemove(ref parameters, "carrier_id");
			return ExecuteRequest<IncomingCarrier>("GET", $"/IncomingCarrier/{carrierId}/", parameters);
		}

		public IRestResponse<BaseResponse> CreateIncomingCarrier(dict parameters)
		{
			return ExecuteRequest<BaseResponse>("POST", "/IncomingCarrier/", parameters);
		}

		public IRestResponse<IncomingCarrier> ModifyIncomingCarrier(dict parameters)
		{
			var carrierId = GetValueAndRemove(ref parameters, "carrier_id");
			return ExecuteRequest<IncomingCarrier>("POST", $"/IncomingCarrier/{carrierId}/", parameters);
		}

		public IRestResponse<BaseResponse> DeleteIncomingCarrier(dict parameters)
		{
			var carrierId = GetValueAndRemove(ref parameters, "carrier_id");
			return ExecuteRequest<BaseResponse>("DELETE", $"/IncomingCarrier/{carrierId}/", parameters);
		}

		public IRestResponse<PlivoPricing> Pricing(dict parameters)
		{
			return ExecuteRequest<PlivoPricing>("GET", "/Pricing/", parameters);
		}
    }
}
