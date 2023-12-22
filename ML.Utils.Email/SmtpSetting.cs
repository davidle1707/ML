
namespace ML.Utils.Email
{
	public class SmtpSetting
	{
		public SmtpSetting()
		{
			EnableSsl = true;
		}

        public string From { get; set; }

	    public string Server { get; set; }

		public int Port { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		public bool EnableSsl { get; set; }

		public string FullName { get; set; }

        public string SubAccount { get; set; }

        public string ViaDomain { get; set; }
	}
}
