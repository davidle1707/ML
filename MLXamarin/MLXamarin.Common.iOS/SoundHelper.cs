using AVFoundation;
using Foundation;
using MLXamarin.Common.iOS;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(SoundHelper))]
namespace MLXamarin.Common.iOS
{
    public class SoundHelper : ISoundHelper
    {
        private AVAudioPlayer _player;

        public void Init(string fileName, bool loop = false)
        {
            Release();

            var filePath = NSBundle.MainBundle.PathForResource(Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName));

            _player = AVAudioPlayer.FromUrl(NSUrl.FromString(filePath));
            
            if (loop)
            {
                _player.NumberOfLoops = -1;
            }
        }

        public void Play()
        {
            _player?.Play();
        }

        public void Pause()
        {
            _player?.Pause();
        }

        public void Stop()
        {
            _player?.Stop();
        }

        public void Release()
        {
            _player = null;
        }
    }
}
