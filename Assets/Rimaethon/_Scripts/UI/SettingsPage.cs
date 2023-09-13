using System.Collections.Generic;
using Rimaethon._Scripts.Core.Enums;
using Rimaethon._Scripts.Utility;
using Rimaethon.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rimaethon._Scripts.UI
{
    public class SettingsPage : MonoBehaviour
    {
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private TMP_Text volumeValueText;
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        private List<string> _options = new();
        private Resolution[] _resolutions;

        private void Awake()
        {
            _resolutions = Screen.resolutions;
            PopulateResolutionDropdown();
        }

        private void Start()
        {
            HandleVolume(volumeSlider.value);
        }


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


        private void HandleVolume(float volume)
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

            var resolutionOptions = new List<string>();
            for (var i = 0; i < _resolutions.Length; i++)
            {
                var option = $"{_resolutions[i].width} x {_resolutions[i].height}";
                resolutionOptions.Add(option);
            }

            resolutionDropdown.ClearOptions();
            resolutionDropdown.AddOptions(resolutionOptions);

            SetDefaultResolution();
        }

        private void SetDefaultResolution()
        {
            var currentResolutionIndex = FindResolutionIndex(Screen.currentResolution);
            if (currentResolutionIndex != -1)
            {
                resolutionDropdown.value = currentResolutionIndex;
                resolutionDropdown.RefreshShownValue();
            }
        }

        public void ApplySelectedResolution()
        {
            var selectedResolutionIndex = resolutionDropdown.value;
            if (selectedResolutionIndex >= 0 && selectedResolutionIndex < _resolutions.Length)
            {
                var selectedResolution = _resolutions[selectedResolutionIndex];
                Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
            }
        }

        private int FindResolutionIndex(Resolution resolutionToFind)
        {
            for (var i = 0; i < _resolutions.Length; i++)
                if (_resolutions[i].width == resolutionToFind.width &&
                    _resolutions[i].height == resolutionToFind.height)
                    return i;
            return -1;
        }
    }
}