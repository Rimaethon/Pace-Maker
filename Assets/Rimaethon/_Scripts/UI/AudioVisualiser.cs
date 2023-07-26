using UnityEngine;

namespace Rimaethon._Scripts.UI
{
    public class AudioVisualiser : MonoBehaviour
    {
        public AudioSpectrum spectrum;
        public GameObject prefab;
        public int numObjects = 50;
        private GameObject[] _objects;

        void Start()
        {
            _objects = new GameObject[numObjects];
            for (int i = 0; i < numObjects; i++)
            {
                GameObject g = GameObject.Instantiate(prefab, this.transform);
                g.transform.localPosition = new Vector3(i * 0.1f, 0, 0);
                _objects[i] = g;
            }
        }

        void Update()
        {
            float[] data = spectrum.Levels;
            for (int i = 0; i < _objects.Length; i++)
            {
                float scale = data[i % data.Length]*1000;
                _objects[i].transform.localScale = new Vector3(1, scale, 1);
            }
        }
    }
}   