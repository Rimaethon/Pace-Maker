using System.Globalization;
using System.IO;
using UnityEngine;

namespace Rimaethon._Scripts.Utility
{
    public class LoadPositions : MonoBehaviour
    {
        private const string Filename = "recorded_positions.txt";
        [SerializeField] private GameObject prefab;

        private void Start()
        {
            LoadRecordedPositions();
            Debug.Log("Positions loaded");
        }

        private void LoadRecordedPositions()
        {
            var filePath = Path.Combine(Application.dataPath, Filename);
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var elements = line.Split(',');
                    if (elements.Length == 3 && TryParseVector3(elements, out var position))
                    {
                        InstantiatePrefabAtPosition(prefab, position);
                        Debug.Log("Object instansiated at position: " + position);
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
                float.TryParse(elements[0], NumberStyles.Float, CultureInfo.InvariantCulture, out var x) &&
                float.TryParse(elements[1], NumberStyles.Float, CultureInfo.InvariantCulture, out var y) &&
                float.TryParse(elements[2], NumberStyles.Float, CultureInfo.InvariantCulture, out var z))
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