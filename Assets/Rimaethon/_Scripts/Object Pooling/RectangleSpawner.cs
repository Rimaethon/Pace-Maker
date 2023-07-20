using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;
    float z=0;
    float y = 5.31f;
    float spawnCount;
    
    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        StartCoroutine(NextPlacement());
    }




    IEnumerator NextPlacement()
    {
        while (true)
        {
            if(spawnCount<3)
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
}
