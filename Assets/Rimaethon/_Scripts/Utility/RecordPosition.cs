using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RecordPosition : MonoBehaviour
{
    private const string Filename = "recorded_positions.txt";
    private List<Vector3> _positions;

    private void Start()
    {
        _positions = new List<Vector3>();
        LoadRecordedPositions();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Record();

        if (Input.GetKeyDown(KeyCode.H)) Save();
    }

    private void Record()
    {
        // Add current position to list only when Space button is pressed
        _positions.Add(transform.position);
    }

    private void Save()
    {
        var filePath = Path.Combine(Application.dataPath, Filename);

        using (var writer = new StreamWriter(filePath))
        {
            foreach (var position in _positions) writer.WriteLine(position.x + "," + position.y + "," + position.z);
        }

        Debug.Log("Recorded positions saved to file: " + filePath);
    }

    private void LoadRecordedPositions()
    {
        var filePath = Path.Combine(Application.dataPath, Filename);

        if (File.Exists(filePath))
        {
            var lines = File.ReadAllLines(filePath);
            _positions = new List<Vector3>();
            foreach (var line in lines)
            {
                var components = line.Split(',');
                if (components.Length == 3)
                {
                    var x = float.Parse(components[0]);
                    var y = float.Parse(components[1]);
                    var z = float.Parse(components[2]);
                    _positions.Add(new Vector3(x, y, z));
                }
            }

            Debug.Log("Recorded positions loaded from file: " + filePath);
        }
    }
}