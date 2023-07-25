using UnityEngine;

namespace Rimaethon._Scripts.MusicSync
{
    public class AudioSyncer : MonoBehaviour 
    {
        protected Transform[] _childObjects;
        internal float timeToBeat = 0.75f;
        internal float restSmoothTime = 0.5f;
        protected bool isBeat;

        protected void Awake()
        {
            _childObjects = GetComponentsInChildren<Transform>();
        }

        private void Update()
        {
            OnUpdate();
        }

        protected virtual void OnUpdate()
        { 
            float[] spectrum = AudioSpectrum.AveragedSpectrum;

            for (int i = 0; i < _childObjects.Length; i++)
            {
                if (spectrum[i] > 0)
                {
                    OnBeat(i);
                }
            }
        }

        protected virtual void OnBeat(int barIndex)
        {
            // Override in derived classes
        }
    }
}