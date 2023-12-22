namespace MLXamarin.Common
{
    public interface ISoundHelper
    {
        void Init(string fileName, bool loop = false);

        void Play();

        void Pause();

        void Stop();

        void Release();
    }
}
