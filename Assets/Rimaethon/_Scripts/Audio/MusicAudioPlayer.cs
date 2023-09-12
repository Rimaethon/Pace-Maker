using UnityEngine;

namespace Rimaethon._Scripts.Audio
{
    public class MusicAudioPlayer : MonoBehaviour,IAudioPlayer
    {
        public AudioClip[] musicClips;
        private AudioSource audioSource;

        public void Play(int clipIndex)
        {
            audioSource.clip = musicClips[clipIndex];
            audioSource.Play();
        }
        public void Pause()
        {
            audioSource.Pause();
        }

        public void Queue(int clipIndex)
        {
            throw new System.NotImplementedException();
        }
    }
}
