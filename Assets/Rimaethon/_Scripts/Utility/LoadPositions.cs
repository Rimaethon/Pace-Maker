using System.Globalization;
using System.IO;
using UnityEngine;

namespace Rimaethon._Scripts
{
    public class LoadPositions : MonoBehaviour
    {
        public GameObject prefab;
        private const string Filename = "recorded_positions.txt";

        void Start()
        {
            LoadRecordedPositions();
        }
        
        private void LoadRecordedPositions()
        {
            string filePath = Path.Combine(Application.dataPath, Filename);
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] elements = line.Split(',');
                    if (elements.Length == 3 && TryParseVector3(elements, out Vector3 position))
                    {
                        InstantiatePrefabAtPosition(prefab, position);
                    }
                }
            }
            else
            {
                Debug.LogWarning("File not found: " + filePath);
            }
        }
        
        private bool TryParseVector3(string[] elements, out Vector3 position)
        {
            position = Vector3.zero;
            if (elements.Length == 3 &&
                float.TryParse(elements[0], NumberStyles.Float, CultureInfo.InvariantCulture, out float x) &&
                float.TryParse(elements[1], NumberStyles.Float, CultureInfo.InvariantCulture, out float y) &&
                float.TryParse(elements[2], NumberStyles.Float, CultureInfo.InvariantCulture, out float z))
            {
                position = new Vector3(x, y, z);
                return true;
            }
            return false;
        }

        private void InstantiatePrefabAtPosition(GameObject prefab, Vector3 position)
        {
            Instantiate(prefab, position, Quaternion.identity);
        }
    }
}