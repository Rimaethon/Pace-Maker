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
    
    public float zAxisMovementSpeed;

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
        // Get the mouse delta input
        Vector3 mouseDelta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
        
        Debug.Log(mouseDelta);
        // Check if the mouse delta input is not zero
        if (mouseDelta != Vector3.zero)
        {
            
            // Calculate the new position of the object based on the mouse delta input
            Vector3 newPosition = transform.position + mouseDelta;

            // Get the center point of the circular area
            Vector3 center = new Vector3(0, 5.3f, transform.position.z);

            // Calculate the distance between the new position and the center point of the circular area
            float distance = Vector3.Distance(newPosition, center);

            // Calculate the constrained position of the object on the circumference of the circle
            Vector3 direction = (newPosition - center).normalized;
            Vector3 constrainedPosition = center + direction * 5.0f;

            // Check if the new position of the object is within the circular area
            bool isWithinCircle = (distance <= 5.0f);

            // Set the position of the object to the constrained position if it's outside the circular area
            transform.position = isWithinCircle ? newPosition : constrainedPosition;
        }
    }





    private void FixedUpdate()
    {
        // Get the current position of the object
        Vector3 currentPosition = transform.position;

        // Add to the z component of the position
        currentPosition.z += zAxisMovementSpeed;

        // Set the object's position to the updated position with unchanged y-component
        transform.position = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z);
    }
    private void OnCollisionEnter(Collision collision)
    {
        
            GameManager.instance.IncrementScore();
            Destroy(collision.gameObject);

    }

}
