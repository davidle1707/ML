namespace ML.Utils.Payment.FirstData
{
    public class FirstDataSetting
    {
        public FirstDataSetting()
        {
            Debug = false;
        }

        public string ApiUrl { get; set; } //https://api.demo.globalgatewaye4.firstdata.com

        public string GatewayId { get; set; } //AG2846-0

        public string PassWord { get; set; } //6k3e7pfx

        public string KeyId { get; set; } //172781

        public string HmacKey { get; set; } //sA3CnraN1QWMv3Gibr6LL739pHxFJ7dR

        public string BindingConfigurationName { get; set; }

        public bool Debug { get; set; }
    }
}
