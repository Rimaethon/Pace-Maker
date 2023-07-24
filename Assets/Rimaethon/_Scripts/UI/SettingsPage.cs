using System;
using System.Collections.Generic;
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
        [SerializeField] private TMP_Text volumeValueText;
        private Resolution[] _resolutions;
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        List<String> _options = new List<string>();
            
            
       
        private void OnEnable()
        {
            volumeSlider.onValueChanged.AddListener(HandleVolume);

        }

        private void OnDisable()
        {
            volumeSlider.onValueChanged.RemoveListener(HandleVolume);

        }
        
        private void OnApplicationQuit()
        {
            PlayerPrefs.Save();
        }

        private void Awake()
        {
            _resolutions = Screen.resolutions;
            PopulateResolutionDropdown();
        }

        private void Start()
        {
            HandleVolume(volumeSlider.value);
        }
        
        
        
        void HandleVolume(float volume)
        {
            volumeValueText.text = volume.ToString("0.00");
            PlayerPrefs.SetFloat("Volume", volume);
            EventManager.Instance.Broadcast(GameEvents.OnVolumeChange, volume);
            
        }
        
       public void SetQuality(int qualityIndex)
        {
            PlayerPrefs.SetInt("Quality", qualityIndex);
            QualitySettings.SetQualityLevel(qualityIndex);
        }
       
       public void SetFullscreen(bool isFullscreen)
        {
            PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
            Screen.fullScreen = isFullscreen;
        }


       private void PopulateResolutionDropdown()
       {
           _resolutions = Screen.resolutions;

           List<string> resolutionOptions = new List<string>();
           for (int i = 0; i < _resolutions.Length; i++)
           {
               string option = $"{_resolutions[i].width} x {_resolutions[i].height}";
               resolutionOptions.Add(option);
           }

           resolutionDropdown.ClearOptions();
           resolutionDropdown.AddOptions(resolutionOptions);

           SetDefaultResolution();
       }

       private void SetDefaultResolution()
       {
           int currentResolutionIndex = FindResolutionIndex(Screen.currentResolution);
           if (currentResolutionIndex != -1)
           {
               resolutionDropdown.value = currentResolutionIndex;
               resolutionDropdown.RefreshShownValue();
           }
       }

       public void ApplySelectedResolution()
       {
           int selectedResolutionIndex = resolutionDropdown.value;
           if (selectedResolutionIndex >= 0 && selectedResolutionIndex < _resolutions.Length)
           {
               Resolution selectedResolution = _resolutions[selectedResolutionIndex];
               Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
           }
       }

       private int FindResolutionIndex(Resolution resolutionToFind)
       {
           for (int i = 0; i < _resolutions.Length; i++)
           {
               if (_resolutions[i].width == resolutionToFind.width && _resolutions[i].height == resolutionToFind.height)
               {
                   return i;
               }
           }
           return -1;
       }


    }
}
