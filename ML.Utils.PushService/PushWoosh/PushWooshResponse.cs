using Newtonsoft.Json;

namespace ML.Utils.PushService.PushWoosh
{
    public class PushWooshResponse
    {
        [JsonProperty("status_code")]
        public string StatusCode { get; set; }

        [JsonProperty("status_message")]
        public string StatusMessage { get; set; }

        [JsonProperty("response")]
        public dynamic Response { get; set; }

        public bool Success { get { return StatusCode == "200"; } }
    }
}
