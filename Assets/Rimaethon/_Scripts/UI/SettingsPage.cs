using Rimaethon._Scripts.Core.Enums;
using Rimaethon._Scripts.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rimaethon._Scripts.UI
{
    public class SettingsPage : MonoBehaviour
    {
    
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private TextMeshProUGUI volumeText;

        private void OnEnable()
        {
            volumeSlider.onValueChanged.AddListener(HandleVolume);

        }

        private void OnDisable()
        {
            volumeSlider.onValueChanged.RemoveListener(HandleVolume);

        }
        

        private void Start()
        {
            HandleVolume(volumeSlider.value);
        }
        

        
        
        
        void HandleVolume(float volume)
        {
            volumeText.text = volume.ToString("0.0");
            PlayerPrefs.SetFloat("Volume", volume);
            EventManager.Instance.Broadcast(GameEvents.OnVolumeChange, volume);
            
        }
    
    
    }
}
