using Rimaethon._Scripts.Core.Enums;
using Rimaethon._Scripts.Utility;
using UnityEngine;

namespace UI
{
    public class AudioManager : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            EventManager.Instance.AddHandler<float>(GameEvents.OnVolumeChange, UpdateVolume);
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveHandler<float>(GameEvents.OnVolumeChange, UpdateVolume);
        }


        private void UpdateVolume(float volume)
        {
            _audioSource.volume = volume;
        }
    }
}