using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RecordPosition : MonoBehaviour
{
    private List<Vector3> _positions;
    private const float RecordInterval = 0.01f;
    private float _recordTimer;

    private const string Filename = "recorded_positions.txt";

    void Start()
    {
        _positions = new List<Vector3>();
        LoadRecordedPositions();
    }

    void Update()
    {
        _recordTimer += Time.deltaTime;
        if (_recordTimer >= RecordInterval)
        {
            _recordTimer = 0f;
            Record();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Save();
        }
    }

    void Record()
    {
        // Add current position to list
        _positions.Add(transform.position);
    }

    public void Save()
    {
        string filePath = Path.Combine(Application.dataPath, Filename);

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (Vector3 position in _positions)
            {
                writer.WriteLine(position.x + "," + position.y + "," + position.z);
            }
        }

        Debug.Log("Recorded positions saved to file: " + filePath);
    }

    private void LoadRecordedPositions()
    {
        string filePath = Path.Combine(Application.dataPath, Filename);

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            _positions = new List<Vector3>();
            foreach (string line in lines)
            {
                string[] components = line.Split(',');
                if (components.Length == 3)
                {
                    float x = float.Parse(components[0]);
                    float y = float.Parse(components[1]);
                    float z = float.Parse(components[2]);
                    _positions.Add(new Vector3(x, y, z));
                }
            }
            Debug.Log("Recorded positions loaded from file: " + filePath);
        }
    }
}
