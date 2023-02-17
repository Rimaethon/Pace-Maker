using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform ballLocation;
    Vector3 locationDifference;
    void Start()
    {
        locationDifference=transform.position-ballLocation.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {   if (BallMovement.ballFall == false)
        {
            transform.position = ballLocation.position + locationDifference;
        }
    }
}
