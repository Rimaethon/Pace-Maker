using UnityEngine;

namespace Rimaethon._Scripts
{
    public class PlayerMovementWithMouse : MonoBehaviour
    {
        public float speed = 1f; // The speed at which the object move
        public float xMin = -7f;
        public float xMax = 7f;
        public float yMin = -7f;
        public float yMax = 7f;
        private Vector3 lastMousePosition;

        private void Update()
        {
            var mouseMovement = new Vector3(Input.mousePosition.x - lastMousePosition.x,
                Input.mousePosition.y - lastMousePosition.y, 0);


            var position = transform.position;
            position.x = Mathf.Clamp(position.x + mouseMovement.x * speed * Time.deltaTime, xMin, xMax);
            position.y = Mathf.Clamp(position.y + mouseMovement.y * speed * Time.deltaTime, yMin, yMax);
            transform.position = position;
            lastMousePosition = Input.mousePosition;
        }

        private void OnEnable()
        {
            lastMousePosition = transform.position;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            SetCursorToStartPosition();
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void SetCursorToStartPosition()
        {
            var startPosition = transform.position;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            var screenPosition = Camera.main.WorldToScreenPoint(startPosition);
            screenPosition.z = 0f;
            var mousePosition = Camera.main.ScreenToWorldPoint(screenPosition);
            mousePosition.z = transform.position.z;
            transform.position = mousePosition;
        }
    }
}