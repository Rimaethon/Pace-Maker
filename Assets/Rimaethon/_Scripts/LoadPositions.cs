using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class LoadPositions : MonoBehaviour
{
    public GameObject prefab;
    private string filename = "recorded_positions.txt";

    // Start is called before the first frame update
    void Start()
    {
        // Load recorded positions from file, if it exists
        string filePath = Path.Combine(Application.dataPath, filename + ".txt");
        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    line = line.Trim(); // Remove any leading or trailing whitespace
                    line = line.TrimStart('('); // Remove the opening parenthesis
                    line = line.TrimEnd(')'); // Remove the closing parenthesis
                    string[] elements = line.Split(','); // Split the line into three elements
                    float x, y, z;
                    if (float.TryParse(elements[0], NumberStyles.Float, CultureInfo.InvariantCulture, out x) 
                        && float.TryParse(elements[1], NumberStyles.Float, CultureInfo.InvariantCulture, out y) 
                        && float.TryParse(elements[2], NumberStyles.Float, CultureInfo.InvariantCulture, out z))
                    {
                        Vector3 position = new Vector3(x, y, z);
                        Instantiate(prefab, position, Quaternion.identity);
                    }

                }
            }
        }
    }
}