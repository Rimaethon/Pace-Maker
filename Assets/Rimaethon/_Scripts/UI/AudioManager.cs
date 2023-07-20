using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI volumeText;
        private AudioSource _audioSource;
        [SerializeField] private Slider volumeSlider;

        private void Awake()
        {
            _audioSource= GetComponent<AudioSource>();
        }

        private void Start()
        {
            UpdateVolume(volumeSlider.value);
            volumeSlider.onValueChanged.AddListener(UpdateVolume);
        }
        

        
        
        
        void UpdateVolume(float volume)
        {
            _audioSource.volume = volume;
            volumeText.text = volume.ToString("0.0");
            PlayerPrefs.SetFloat("Volume", volume);
            
        }
      
    }
}
