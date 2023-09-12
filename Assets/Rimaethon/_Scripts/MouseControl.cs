using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseControl : MonoBehaviour, PlayerInputs.IPlayerActions
{
    private Vector2 mousePosition;

    public void OnMovement(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
    private void Update()
    {
        Debug.Log(mousePosition);
    }


}