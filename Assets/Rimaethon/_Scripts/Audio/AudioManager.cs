using System;
using Rimaethon._Scripts.Audio;
using Rimaethon._Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;
using UnityEngine;

namespace Rimaethon._Scripts.UI
{
    public class AudioManager : MonoBehaviour
    {
        #region Fields
        
        private AudioSource _audioSource;
        private IAudioPlayer _musicPlayer;
        private IAudioPlayer _sfxPlayer;
        private bool isInitialized;
        
        #endregion

        #region Unity Functions
        
        private void OnEnable()
        {
            EventManager.Instance.AddHandler<float>(GameEvents.OnVolumeChange, UpdateVolume);
            EventManager.Instance.AddHandler(GameEvents.OnButtonHover, () => _sfxPlayer.Play(0));

        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveHandler<float>(GameEvents.OnVolumeChange, UpdateVolume);
            EventManager.Instance.RemoveHandler(GameEvents.OnButtonHover, () => _sfxPlayer.Play(0));
        }
        private void Awake()
        {
            
        }

        private void OnApplicationQuit()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Custom Functions
        
        public void Initialize(IAudioPlayer musicPlayer, IAudioPlayer sfxPlayer)
        {
            if (isInitialized) return;

            _musicPlayer = musicPlayer;
            _sfxPlayer = sfxPlayer;

            isInitialized = true;
        }
   
      


        private void UpdateVolume(float volume)
        {
            _audioSource.volume = volume;
        }

        #endregion
    
    }
}