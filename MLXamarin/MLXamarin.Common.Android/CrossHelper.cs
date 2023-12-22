using Android.Provider;
using MLXamarin.Common.Android;

[assembly: Xamarin.Forms.Dependency(typeof(CrossHelper))]
namespace MLXamarin.Common.Android
{
    public class CrossHelper : ICrossHelper
    {
        public string GetDeviceId()
        {
            return Settings.Secure.GetString(Common.CurrentActivity.ContentResolver, Settings.Secure.AndroidId);
            //return Settings.Secure.GetString(Application.Context.ContentResolver, Settings.Secure.AndroidId);
        }
    }
}