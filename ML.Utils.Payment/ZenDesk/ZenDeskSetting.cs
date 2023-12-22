using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML.Utils.Payment.ZenDesk
{
    public class ZenDeskSetting
    {
        public string ApiUrl { get; set; } //https://api.demo.globalgatewaye4.firstdata.com

        public string GatewayId { get; set; } //AG2846-0

        public string PassWord { get; set; } //6k3e7pfx

        public string KeyId { get; set; } //172781

        public string HmacKey { get; set; } //sA3CnraN1QWMv3Gibr6LL739pHxFJ7dR

        public string BindingConfigurationName { get; set; }
    }
}
