using MLXamarin.Common.iOS;
using MLXamarin.Common.Shared;

[assembly: Xamarin.Forms.Dependency(typeof(DateTimeHelper))]
namespace MLXamarin.Common.iOS
{
    public class DateTimeHelper : SharedDateTimeHelper, IDateTimeHelper
    {
    }
}
