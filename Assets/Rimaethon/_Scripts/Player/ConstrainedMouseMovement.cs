using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class ConstrainedMouseMovement : MonoBehaviour
{
    private Vector3 mouseDelta;

    private void Start()
    {
        InitializeSettings();

    }
    private void Update()
    {
        HandleMouseMovement();

    }
    private void InitializeSettings()
    {
       // Cursor.visible = false;
    }

   
    private void HandleMouseMovement()
    {
        mouseDelta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);

        if (mouseDelta != Vector3.zero)
        {
            transform.position = CalculateNewPosition(mouseDelta);
        }
    }
    
    private Vector3 CalculateNewPosition(Vector3 mouseDelta)
    {
        Vector3 newPosition = transform.position + mouseDelta;
    
        newPosition.x = Mathf.Clamp(newPosition.x, -13f, 13f);
        newPosition.y = Mathf.Clamp(newPosition.y, -7f, 7f);

        return newPosition;
    }
}

