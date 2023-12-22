using MLXamarin.Common.Android;
using MLXamarin.Common.Shared;

[assembly: Xamarin.Forms.Dependency(typeof(CryptoHelper))]
namespace MLXamarin.Common.Android
{
    public class CryptoHelper : SharedCryptoHelper, ICryptoHelper
    {
    }
}
