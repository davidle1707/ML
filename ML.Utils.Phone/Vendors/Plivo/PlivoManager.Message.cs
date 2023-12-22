using ML.Utils.Phone.Vendors.Plivo.Models;
using RestSharp;
using System;
using System.Threading.Tasks;
using dict = System.Collections.Generic.Dictionary<string, string>;

namespace ML.Utils.Phone.Vendors.Plivo
{
    public partial class PlivoManager
    {
		/// <summary>
		/// View <see cref="https://www.plivo.com/docs/api/message/#send-a-message"/>
		/// </summary>
		public IRestResponse<SendMessageResponse> SendMessage(SendMessageRequest request)
		{
			var dict = Util.GetParams(request);

			return SendMessage(dict);
		}

		/// <summary>
		/// View <see cref="https://www.plivo.com/docs/api/message/#send-a-message"/>
		/// </summary>
		public IRestResponse<SendMessageResponse> SendMessage(dict parameters)
		{
			return ExecuteRequest<SendMessageResponse>("POST", "/Message/", parameters);
		}

        /// <summary>
        /// View <see cref="https://www.plivo.com/docs/api/message/#send-a-message"/>
        /// </summary>
        public Task<IRestResponse<SendMessageResponse>> SendMessageAsync(SendMessageRequest request)
        {
            var dict = Util.GetParams(request);

            return SendMessageAsync(dict);
        }

        /// <summary>
        /// View <see cref="https://www.plivo.com/docs/api/message/#send-a-message"/>
        /// </summary>
        public Task<IRestResponse<SendMessageResponse>> SendMessageAsync(dict parameters)
        {
            return ExecuteRequestAsync<SendMessageResponse>("POST", "/Message/", parameters);
        }

		/// <summary>
		/// View <see cref="https://www.plivo.com/docs/api/message/#get-details-of-a-single-message"/>
		/// </summary>
		public IRestResponse<Message> GetMessage(string messageId)
		{
			var dict = new dict { { "record_id", messageId } };

			return GetMessage(dict);
		}

		/// <summary>
		/// View <see cref="https://www.plivo.com/docs/api/message/#get-details-of-a-single-message"/>
		/// </summary>
		public IRestResponse<Message> GetMessage(dict parameters)
		{
			var record_id = GetValueAndRemove(ref parameters, "record_id");
			return ExecuteRequest<Message>("GET", $"/Message/{record_id}/", parameters);
		}

		/// <summary>
		/// View <see cref="https://www.plivo.com/docs/api/message/#get-details-of-all-messages"/>
		/// </summary>
		public IRestResponse<GetMessagesResponse> GetMessages(GetMessagesRequest request)
		{
			var dict = Util.GetParams(request);

			return GetMessages(dict);
		}

		/// <summary>
		/// View <see cref="https://www.plivo.com/docs/api/message/#get-details-of-all-messages"/>
		/// </summary>
		public IRestResponse<GetMessagesResponse> GetMessages()
		{
			return GetMessages(new dict());
		}

		/// <summary>
		/// View <see cref="https://www.plivo.com/docs/api/message/#get-details-of-all-messages"/>
		/// </summary>
		public IRestResponse<GetMessagesResponse> GetMessages(dict parameters)
		{
			return ExecuteRequest<GetMessagesResponse>("GET", "/Message/", parameters);
		}
    }
}
