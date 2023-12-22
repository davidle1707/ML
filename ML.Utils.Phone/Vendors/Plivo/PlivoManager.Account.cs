using ML.Utils.Phone.Vendors.Plivo.Models;
using RestSharp;
using System;
using dict = System.Collections.Generic.Dictionary<string, string>;

namespace ML.Utils.Phone.Vendors.Plivo
{
    public partial class PlivoManager
    {
		public IRestResponse<Account> GetAccount()
		{
			return ExecuteRequest<Account>("GET", "/ ", new dict());
			// had to add an additional space after / as RestSharp consumes it.
		}

		public IRestResponse<BaseResponse> ModifyAccount(dict parameters)
		{
			// had to add an additional space after / as RestSharp consumes it.
			return ExecuteRequest<BaseResponse>("POST", "/ ", parameters);
		}

		public IRestResponse<SubAccountList> GetSubaccounts()
		{
			return ExecuteRequest<SubAccountList>("GET", "/Subaccount/", new dict());
		}

		public IRestResponse<SubAccount> GetSubaccount(dict parameters)
		{
			var subauth_id = GetValueAndRemove(ref parameters, "subauth_id");
			return ExecuteRequest<SubAccount>("GET", $"/Subaccount/{subauth_id}/", parameters);
		}

		public IRestResponse<BaseResponse> CreateSubaccount(dict parameters)
		{
			return ExecuteRequest<BaseResponse>("POST", "/Subaccount/", parameters);
		}

		public IRestResponse<BaseResponse> ModifySubaccount(dict parameters)
		{
			var subauth_id = GetValueAndRemove(ref parameters, "subauth_id");
			return ExecuteRequest<BaseResponse>("POST", $"/Subaccount/{subauth_id}/ ", parameters);
		}

		public IRestResponse<BaseResponse> DeleteSubaccount(dict parameters)
		{
			var subauth_id = GetValueAndRemove(ref parameters, "subauth_id");
			return ExecuteRequest<BaseResponse>("DELETE", $"/Subaccount/{subauth_id}/", parameters);
		}
    }
}
