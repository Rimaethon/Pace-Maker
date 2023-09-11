using System;
using System.Collections.Generic;
using Rimaethon._Scripts.Audio;
using Rimaethon._Scripts.UI;
using Rimaethon.Scripts.UI;
using UI;
using UnityEngine;

namespace Rimaethon._Scripts.Managers
{
    public class GameInitializer : MonoBehaviour
    {
        public SfxAudioPlayer sfxPlayer;
        public MusicAudioPlayer musicPlayer;
        public AudioManager audioManager;
        public List<UIPage> staticUIPages;
        public List<UIPage> dynamicUIPages;

        private void Awake()
        {
            audioManager.Initialize(musicPlayer, sfxPlayer);
            
            
        }
    }
}
