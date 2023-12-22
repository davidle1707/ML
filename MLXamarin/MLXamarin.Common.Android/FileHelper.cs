using MLXamarin.Common.Android;
using MLXamarin.Common.Shared;

[assembly: Xamarin.Forms.Dependency(typeof(FileHelper))]
namespace MLXamarin.Common.Android
{
    public class FileHelper : SharedFileHelper, IFileHelper
    {
    }
}
