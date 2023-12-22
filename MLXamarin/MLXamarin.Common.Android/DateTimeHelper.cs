using MLXamarin.Common.Android;
using MLXamarin.Common.Shared;

[assembly: Xamarin.Forms.Dependency(typeof(DateTimeHelper))]
namespace MLXamarin.Common.Android
{
    public class DateTimeHelper : SharedDateTimeHelper, IDateTimeHelper
    {
    }
}
