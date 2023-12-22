using ML.Utils.Phone.Vendors.Plivo.Models;
using RestSharp;
using System;
using dict = System.Collections.Generic.Dictionary<string, string>;

namespace ML.Utils.Phone.Vendors.Plivo
{
    public partial class PlivoManager
    {
		public IRestResponse<EndpointList> GetEndpoints()
		{
			return ExecuteRequest<EndpointList>("GET", "/Endpoint/", new dict());
		}

		public IRestResponse<EndpointList> GetEndpoints(dict parameters)
		{
			return ExecuteRequest<EndpointList>("GET", "/Endpoint/", parameters);
		}

		public IRestResponse<Endpoint> CreateEndpoint(dict parameters)
		{
			return ExecuteRequest<Endpoint>("POST", "/Endpoint/", parameters);
		}

		public IRestResponse<Endpoint> GetEndpoint(dict parameters)
		{
			var endpoint_id = GetValueAndRemove(ref parameters, "endpoint_id");
			return ExecuteRequest<Endpoint>("GET", $"/Endpoint/{endpoint_id}/", parameters);
		}

		public IRestResponse<BaseResponse> ModifyEndpoint(dict parameters)
		{
			var endpoint_id = GetValueAndRemove(ref parameters, "endpoint_id");
			return ExecuteRequest<BaseResponse>("POST", $"/Endpoint/{endpoint_id}/", parameters);
		}

		public IRestResponse<BaseResponse> DeleteEndpoint(dict parameters)
		{
			var endpoint_id = GetValueAndRemove(ref parameters, "endpoint_id");
			return ExecuteRequest<BaseResponse>("DELETE", $"/Endpoint/{endpoint_id}/", parameters);
		}
    }
}
