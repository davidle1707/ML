using ML.Utils.Phone.Vendors.Plivo.Models;
using RestSharp;
using System;
using System.Threading.Tasks;
using dict = System.Collections.Generic.Dictionary<string, string>;

namespace ML.Utils.Phone.Vendors.Plivo
{
    public partial class PlivoManager
    {
		public IRestResponse<NumberList> GetNumbers()
		{
			return ExecuteRequest<NumberList>("GET", "/Number/", new dict());
		}

        ///<summary>
        ///View <see cref="https://www.plivo.com/docs/api/number/phonenumber/#search-for-new-numbers"/>
        ///</summary>
        public IRestResponse<SearchPhoneNumberResponse> SearchPhoneNumbers(SearchPhoneNumberRequest request)
        {
            return SearchPhoneNumbersAsync(request).Result;
        }

		///<summary>
		///View <see cref="https://www.plivo.com/docs/api/number/phonenumber/#search-for-new-numbers"/>
		///</summary>
		public Task<IRestResponse<SearchPhoneNumberResponse>> SearchPhoneNumbersAsync(SearchPhoneNumberRequest request)
		{
			var dict = Util.GetParams(request);

            return SearchPhoneNumbersAsync(dict);
		}

        ///<summary>
        ///View <see cref="https://www.plivo.com/docs/api/number/phonenumber/#search-for-new-numbers"/>
        ///</summary>
        public IRestResponse<SearchPhoneNumberResponse> SearchPhoneNumbers(dict parameters)
        {
            return SearchPhoneNumbersAsync(parameters).Result;
        }
		
		///<summary>
		///View <see cref="https://www.plivo.com/docs/api/number/phonenumber/#search-for-new-numbers"/>
		///</summary>
		public Task<IRestResponse<SearchPhoneNumberResponse>> SearchPhoneNumbersAsync(dict parameters)
		{
			return ExecuteRequestAsync<SearchPhoneNumberResponse>("GET", "/PhoneNumber/", parameters);
		}

		///<summary>
		///View <see cref="https://www.plivo.com/docs/api/number/phonenumber/#buy-number"/>
		///</summary>
		public IRestResponse<BuyPhoneNumberResponse> BuyPhoneNumber(BuyPhoneNumberRequest request)
		{
            return BuyPhoneNumberAsync(request).Result;
		}

        ///<summary>
        ///View <see cref="https://www.plivo.com/docs/api/number/phonenumber/#buy-number"/>
        ///</summary>
        public Task<IRestResponse<BuyPhoneNumberResponse>> BuyPhoneNumberAsync(BuyPhoneNumberRequest request)
        {
            var dict = Util.GetParams(request);

            return BuyPhoneNumberAsync(dict);
        }

		///<summary>
		///View <see cref="https://www.plivo.com/docs/api/number/phonenumber/#buy-number"/>
		///</summary>
		public IRestResponse<BuyPhoneNumberResponse> BuyPhoneNumber(dict parameters)
		{
            return BuyPhoneNumberAsync(parameters).Result;
		}

        ///<summary>
        ///View <see cref="https://www.plivo.com/docs/api/number/phonenumber/#buy-number"/>
        ///</summary>
        public Task<IRestResponse<BuyPhoneNumberResponse>> BuyPhoneNumberAsync(dict parameters)
        {
            var number = GetValueAndRemove(ref parameters, "number");

            return ExecuteRequestAsync<BuyPhoneNumberResponse>("POST", $"/PhoneNumber/{number}/", parameters);
        }

		[Obsolete("Use SearchPhoneNumbers instead")]
		public IRestResponse<NumberList> SearchNumbers(dict parameters)
		{
			return ExecuteRequest<NumberList>("GET", "/AvailableNumber/", parameters);
		}

		[Obsolete("Use SearchPhoneNumbers instead")]
		public IRestResponse<NumberList> SearchNumberGroup(dict parameters)
		{
			return ExecuteRequest<NumberList>("GET", "/AvailableNumberGroup/", parameters);
		}

		public IRestResponse<Number> GetNumber(dict parameters)
		{
			var number = GetValueAndRemove(ref parameters, "number");
			return ExecuteRequest<Number>("GET", $"/Number/{number}/", parameters);
		}

		[Obsolete("Use BuyPhoneNumber instead")]
		public IRestResponse<BaseResponse> RentNumber(dict parameters)
		{
			var number = GetValueAndRemove(ref parameters, "number");
			return ExecuteRequest<BaseResponse>("POST", $"/AvailableNumber/{number}/", parameters);
		}

		[Obsolete("Use BuyPhoneNumber instead")]
		public IRestResponse<NumberResponse> RentFromNumberGroup(dict parameters)
		{
			var group_id = GetValueAndRemove(ref parameters, "group_id");
			return ExecuteRequest<NumberResponse>("POST", $"/AvailableNumberGroup/{group_id}/", parameters);
		}

        public IRestResponse<BaseResponse> UnrentNumber(BuyPhoneNumberRequest request)
        {
            return UnrentNumberAsync(request).Result;
		}

        public Task<IRestResponse<BaseResponse>> UnrentNumberAsync(BuyPhoneNumberRequest request)
        {
            var dict = Util.GetParams(request);

            return UnrentNumberAsync(dict);
        }

        public IRestResponse<BaseResponse> UnrentNumber(dict parameters)
        {
            return UnrentNumberAsync(parameters).Result;
        }

        public async Task<IRestResponse<BaseResponse>> UnrentNumberAsync(dict parameters)
        {
            var number = GetValueAndRemove(ref parameters, "number");
            var response = await ExecuteRequestAsync<BaseResponse>("DELETE", $"/Number/{number}/", parameters);

            return response;
        }

		public IRestResponse<BaseResponse> LinkApplicationNumber(dict parameters)
		{
			var number = GetValueAndRemove(ref parameters, "number");
			return ExecuteRequest<BaseResponse>("POST", $"/Number/{number}/", parameters);
		}

		public IRestResponse<BaseResponse> UnlinkApplicationNumber(dict parameters)
		{
			var number = GetValueAndRemove(ref parameters, "number");
			parameters.Add("app_id", "");
			return ExecuteRequest<BaseResponse>("POST", $"/Number/{number}/", parameters);
		}
    }
}
