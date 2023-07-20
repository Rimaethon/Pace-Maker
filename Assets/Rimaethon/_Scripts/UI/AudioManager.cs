using System;
using Rimaethon._Scripts.Core.Enums;
using Rimaethon._Scripts.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AudioManager : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void OnEnable()
        {
            EventManager.Instance.AddHandler<float>(GameEvents.OnVolumeChange, UpdateVolume);

        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveHandler<float>(GameEvents.OnVolumeChange, UpdateVolume);
        }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        
        
        void UpdateVolume(float volume)
        {
            _audioSource.volume = volume;
        }
        
    }
}
