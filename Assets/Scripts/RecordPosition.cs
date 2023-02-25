using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class RecordPosition : MonoBehaviour
{
    private List<Vector3> positions;
    private float recordInterval = 0.01f;
    private float recordTimer = 0f;

    private string filename = "recorded_positions.txt";

    // Start is called before the first frame update
    void Start()
    {
        positions = new List<Vector3>();

        // Load recorded positions from file, if it exists

    }



    // Update is called once per frame
    void Update()
    {
        // Record position every recordInterval seconds
        recordTimer += Time.deltaTime;
        if (recordTimer >= recordInterval)
        {
            recordTimer = 0f;
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
        positions.Add(transform.position);
    }

    public void Save()
    {
        // Get the full path to the file in the project's Assets folder
        string filePath = Path.Combine(Application.dataPath, filename + ".txt");

        // Save recorded positions to file
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (Vector3 position in positions)
            {
                writer.WriteLine(position.ToString());
                Debug.Log("This is saved"+ position.ToString());
            }
        }

        // Refresh the AssetDatabase to make sure the file shows up in the Editor
        AssetDatabase.Refresh();
    }

}