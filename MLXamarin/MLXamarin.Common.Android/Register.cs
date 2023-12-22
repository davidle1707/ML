using Xamarin.Forms;

namespace MLXamarin.Common.Android
{
    public static class Register
    {
        public static void Init(bool nothing = false, bool crossHelper = true, bool fileHelper = true, bool cryptoHelper = true, bool dateTimeHelper = true, bool soundHelper = false)
        {
            if (crossHelper)
            {
                DependencyService.Register<CrossHelper>();
            }
            if (fileHelper)
            {
                DependencyService.Register<FileHelper>();
            }
            if (cryptoHelper)
            {
                DependencyService.Register<CrossHelper>();
            }
            if (dateTimeHelper)
            {
                DependencyService.Register<DateTimeHelper>();
            }
            if (soundHelper)
            {
                DependencyService.Register<SoundHelper>();
            }
        }
    }
}
