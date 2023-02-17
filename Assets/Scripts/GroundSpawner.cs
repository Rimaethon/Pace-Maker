using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject lastGround;
    Vector3 spawnLocation;
    int randomXY;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnLocation = new Vector3(-1,0,2);
        for (int i = 0; i < 16; i++)
        {
            GenerateGround();
        }
    }
      
    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateGround()
    {
        randomXY=Random.Range(0, 2);
        switch(randomXY)
        {
            case 0:
                spawnLocation.x +=-1;
                break;
            case 1:
                spawnLocation.z += 1;
                break;
        }
        lastGround=Instantiate(lastGround, spawnLocation, Quaternion.identity);
        
    }
    
    


    
}
