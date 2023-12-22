
namespace ML.Utils.VitelityFax
{
    public class VitelityFaxSetting
    {
        public VitelityFaxSetting()
        {
            ApiUrl = "http://api.vitelity.net/fax.php";
        }

        public string ApiUrl { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
