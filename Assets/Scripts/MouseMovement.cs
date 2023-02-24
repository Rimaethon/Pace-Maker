using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
public class MouseMovement : MonoBehaviour
{
    public Camera mainCamera;
    public float zCoordinate = 0f; // public variable for the z coordinate

    private void Start()
    {
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        // Get the mouse position
        Vector3 mousePosition = Input.mousePosition;

        // Convert the mouse position from screen space to world space
        mousePosition.z = mainCamera.transform.position.y;
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition); 
        // set the z coordinate to the public variable

        // Calculate the distance between the object and the center of the circular area
        Vector2 center = new Vector2(0,5.3f); // assuming the circular area is the parent object
        float distance = Vector2.Distance(worldPosition, center);

        // Constrain the movement of the object to the circular area
        if (distance < 6)
        {
            transform.position = new Vector3(worldPosition.x,worldPosition.y,transform.position.z);
        }
        else
        {
            Vector2 directionDistance = worldPosition - center;
            // If the mouse is outside the circular area, move the object along the circumference of the circle
            Vector2 direction = directionDistance.normalized;
            transform.position = center + direction * 6;
        }
    }
}
