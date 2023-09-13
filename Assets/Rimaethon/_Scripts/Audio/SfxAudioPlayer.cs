using UnityEngine;

namespace Rimaethon._Scripts.Audio
{
    public class SfxAudioPlayer : MonoBehaviour,IAudioPlayer
    {
        
        public AudioClip[] sfxClips;
        private AudioSource audioSource;

        public void Play(int clipIndex)
        {
            audioSource.clip = sfxClips[clipIndex];
            audioSource.Play();
        }

        public void Pause()
        {
            audioSource.Pause();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public void Queue(int clipIndex)
        {
            throw new System.NotImplementedException();
        }
    }
}
