using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float zAxisMovementSpeed;
    private void FixedUpdate()
    {
        // Get the current position of the object
        Vector3 currentPosition = transform.position;

        // Add 1 to the z component of the position
        currentPosition.z += zAxisMovementSpeed;

        // Set the object's position to the updated position
        transform.position = currentPosition;
    }
}
