
namespace ML.Utils.Payment.Trans2Pay
{
    public class Trans2PaySetting
    {
        public string UserName { get; set; }

        public string PassWord { get; set; }

        public string PolicyId { get; set; }
        /// <summary>
        /// default value: APIServiceSoap
        /// </summary>
        public string BindingConfigurationName { get; set; }

        public string ApiUrl { get; set; }
    }
}
