using log4net;
using ML.Utils.Phone.Vendors;
using ML.Utils.Phone.Vendors.Plivo;
using ML.Utils.Phone.Vendors.Tropo;
using ML.Utils.Phone.Vendors.Twilio;
using ML.Utils.Phone.Vendors.Vitelity;

namespace ML.Utils.Phone
{
    public partial class PhoneManager
	{
		protected ILog log = LogManager.GetLogger(typeof(PhoneManager));

		private TwilioManager _twilio;
		private VitelityManager _vitelity;
		private PlivoManager _plivo;
		private TropoManager _tropo;

		public TwilioManager Twilio => _twilio ?? (_twilio = new TwilioManager());

	    public VitelityManager Vitelity => _vitelity ?? (_vitelity = new VitelityManager());

	    public PlivoManager Plivo => _plivo ?? (_plivo = new PlivoManager());

	    public TropoManager Tropo => _tropo ?? (_tropo = new TropoManager());

	    public PhoneManager(bool initVendorSettingsFromAppSetting = false)
		{
			if (initVendorSettingsFromAppSetting)
			{
				InitVendorSettingsFromAppSetting();
			}
		}

		public PhoneManager(params VendorSetting[] settings)
		{
			foreach (var setting in settings)
			{
				if (setting  is TwilioSetting)
				{
					_twilio = new TwilioManager((TwilioSetting)setting);
				}
				else if (setting is PlivoSetting)
				{
					_plivo = new PlivoManager((PlivoSetting)setting);
				}
				else if (setting is VitelitySetting)
				{
					_vitelity = new VitelityManager((VitelitySetting)setting);
				}
				else if (setting is TropoSetting)
				{
					_tropo = new TropoManager((TropoSetting)setting);
				}
			}
		}

		public void InitVendorSettingsFromAppSetting()
		{
			_twilio = new TwilioManager(true);
			_vitelity = new VitelityManager(true);
			_plivo = new PlivoManager(true);
			_tropo = new TropoManager(true);
		}
	}
}
