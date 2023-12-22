using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MLXamarin.Common.Android;

[assembly: Xamarin.Forms.Dependency(typeof(SoundHelper))]
namespace MLXamarin.Common.Android
{
    public class SoundHelper : ISoundHelper
    {
        private MediaPlayer _player;

        public void Init(string fileName, bool loop = false)
        {
            Release();

            _player = new MediaPlayer { Looping = loop };
            
            var fd = global::Android.App.Application.Context.Assets.OpenFd(fileName);
            _player.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length);
            _player.Prepare();
        }

        public void Play()
        {
            _player?.Start();
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
            _player?.Release();
            _player = null;
        }
    }
}