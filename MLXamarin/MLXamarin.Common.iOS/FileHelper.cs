using MLXamarin.Common.iOS;
using MLXamarin.Common.Shared;

[assembly: Xamarin.Forms.Dependency(typeof(FileHelper))]
namespace MLXamarin.Common.iOS
{
    public class FileHelper : SharedFileHelper, IFileHelper
    {
         }
}
