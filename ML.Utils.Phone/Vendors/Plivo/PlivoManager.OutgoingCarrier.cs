using ML.Utils.Phone.Vendors.Plivo.Models;
using RestSharp;
using System;
using dict = System.Collections.Generic.Dictionary<string, string>;

namespace ML.Utils.Phone.Vendors.Plivo
{
    public partial class PlivoManager
    {
		public IRestResponse<OutgoingCarrierList> GetOutgoingCarriers()
		{
			return ExecuteRequest<OutgoingCarrierList>("GET", "/OutgoingCarrier/", new dict());
		}

		public IRestResponse<OutgoingCarrier> GetOutgoingCarrier(dict parameters)
		{
			var carrierId = GetValueAndRemove(ref parameters, "carrier_id");
			return ExecuteRequest<OutgoingCarrier>("GET", $"/OutgoingCarrier/{carrierId}/", parameters);
		}

		public IRestResponse<BaseResponse> CreateOutgoingCarrier(dict parameters)
		{
			return ExecuteRequest<BaseResponse>("POST", "/OutgoingCarrier/", parameters);
		}

		public IRestResponse<BaseResponse> ModifyOutgoingCarrier(dict parameters)
		{
			var carrierId = GetValueAndRemove(ref parameters, "carrier_id");
			return ExecuteRequest<BaseResponse>("POST", $"/OutgoingCarrier/{carrierId}/", parameters);
		}

		public IRestResponse<BaseResponse> DeleteOutgoingCarrier(dict parameters)
		{
			var carrierId = GetValueAndRemove(ref parameters, "carrier_id");
			return ExecuteRequest<BaseResponse>("DELETE", $"/OutgoingCarrier/{carrierId}/", parameters);
		}
    }
}
