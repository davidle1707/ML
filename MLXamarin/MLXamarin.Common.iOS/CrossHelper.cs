using MLXamarin.Common.iOS;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(CrossHelper))]
namespace MLXamarin.Common.iOS
{
    public class CrossHelper : ICrossHelper
    {
        public string GetDeviceId()
        {
            return UIDevice.CurrentDevice.IdentifierForVendor.AsString();
        }
    }
}
