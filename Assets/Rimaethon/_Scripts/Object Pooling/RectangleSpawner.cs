using System.Collections;
using UnityEngine;

public class RectangleSpawner : MonoBehaviour
{
    private ObjectPooler objectPooler;
    private float spawnCount;
    private readonly float y = 5.31f;
    private float z;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        StartCoroutine(NextPlacement());
    }


    private IEnumerator NextPlacement()
    {
        while (true)
            if (spawnCount < 3)
            {
                objectPooler.SpawnFromPool("Rectangle", new Vector3(0, y, z), Quaternion.identity);
                spawnCount++;
                z += 25f;
            }
            else
            {
                yield return new WaitForSeconds(8f);
                objectPooler.SpawnFromPool("Rectangle", new Vector3(0, y, z), Quaternion.identity);
                z += 25f;
            }
    }
}