namespace Rimaethon._Scripts.Audio
{
    public interface IAudioPlayer
    {
        void Play(int clipIndex);
        void Pause();
        void Queue(int clipIndex);
    }
}