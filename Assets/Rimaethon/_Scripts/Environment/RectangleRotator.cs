using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleRotator : MonoBehaviour,IPooledObject
{
    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, 0, 3);
    }
}
