using UnityEngine;

namespace Rimaethon._Scripts.MusicSync
{
    public class AudioSpectrum : MonoBehaviour 
    {
        private readonly float[] _audioSpectrum = new float[128];
        public static float[] AveragedSpectrum { get; private set; } = new float[20];

        private void Update()
        {
            AudioListener.GetSpectrumData(_audioSpectrum, 0, FFTWindow.Hamming);
            float maxVal = Mathf.Max(_audioSpectrum);
            for (int i = 0; i < _audioSpectrum.Length; i++)
            {
                _audioSpectrum[i] /= maxVal;
            }

            // Average the spectrum data
            for (int i = 0; i < AveragedSpectrum.Length; i++)
            {
                int start = i * 6;
                int end = start + 6;
                float average = 0;
                for (int j = start; j < end; j++)
                {
                    average += _audioSpectrum[j];
                }
                AveragedSpectrum[i] = (average / 6) * 100;
            }
        }
    }
}