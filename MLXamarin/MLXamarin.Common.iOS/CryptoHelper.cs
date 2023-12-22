using MLXamarin.Common.iOS;
using MLXamarin.Common.Shared;

[assembly: Xamarin.Forms.Dependency(typeof(CryptoHelper))]
namespace MLXamarin.Common.iOS
{
    public class CryptoHelper : SharedCryptoHelper, ICryptoHelper
    {
    }
}
