using ML.Utils.Phone.Vendors.Plivo.Models;
using RestSharp;
using System;
using dict = System.Collections.Generic.Dictionary<string, string>;

namespace ML.Utils.Phone.Vendors.Plivo
{
    public partial class PlivoManager
    {
		public IRestResponse<OutgoingCarrierRoutingList> GetOutgoingCarrierRoutings()
		{
			return ExecuteRequest<OutgoingCarrierRoutingList>("GET", "/OutgoingCarrierRouting/", new dict());
		}

		public IRestResponse<OutgoingCarrierRouting> GetOutgoingCarrierRouting(dict parameters)
		{
			var carrierId = GetValueAndRemove(ref parameters, "routing_id");
			return ExecuteRequest<OutgoingCarrierRouting>("GET", $"/OutgoingCarrierRouting/{carrierId}/", parameters);
		}

		public IRestResponse<BaseResponse> CreateOutgoingCarrierRouting(dict parameters)
		{
			return ExecuteRequest<BaseResponse>("POST", "/OutgoingCarrierRouting/", parameters);
		}

		public IRestResponse<BaseResponse> ModifyOutgoingCarrierRouting(dict parameters)
		{
			var carrierId = GetValueAndRemove(ref parameters, "routing_id");
			return ExecuteRequest<BaseResponse>("POST", $"/OutgoingCarrierRouting/{carrierId}/", parameters);
		}

		public IRestResponse<BaseResponse> DeleteOutgoingCarrierRouting(dict parameters)
		{
			var carrierId = GetValueAndRemove(ref parameters, "routing_id");
			return ExecuteRequest<BaseResponse>("DELETE", $"/OutgoingCarrierRouting/{carrierId}/", parameters);
		}

    }
}
