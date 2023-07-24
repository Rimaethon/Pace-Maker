using UnityEngine;

namespace Rimaethon._Scripts.MusicSync
{
    public class AudioSpectrum : MonoBehaviour 
    {
        private float[] audioSpectrum;
        public static float spectrumValue { get; private set; }

        private void Start()
        {
            audioSpectrum = new float[128]; 
        }

        private void Update()
        {
            AudioListener.GetSpectrumData(audioSpectrum, 0, FFTWindow.Hamming);

            float maxVal = Mathf.Max(audioSpectrum);
            for (int i = 0; i < audioSpectrum.Length; i++)
            {
                audioSpectrum[i] /= maxVal;
            }

            if (audioSpectrum != null && audioSpectrum.Length > 0)
            {
                spectrumValue = audioSpectrum[0] * 100;
            }
            Debug.Log(spectrumValue);
        }
    }
}