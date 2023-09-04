using UnityEngine;

namespace Rimaethon._Scripts.Player
{
    public class MouseMovement : MonoBehaviour
    {
        [SerializeField]
        private Camera mainCamera;

        [SerializeField]
        private float zAxisMovementSpeed;

        private void Start()
        {
            InitializeSettings();
        }

        private void Update()
        {
            HandleMouseMovement();
        }

        private void FixedUpdate()
        {
            MoveAlongZAxis();
        }

        private void OnCollisionEnter(Collision collision)
        {
            HandleCollision(collision.gameObject);
        }

        private void InitializeSettings()
        {
            Cursor.visible = false;
        }

        private void HandleMouseMovement()
        {
            var mouseDelta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);

            if (mouseDelta != Vector3.zero)
            {
                Vector3 newPosition = CalculateNewPosition(mouseDelta);
                transform.position = newPosition;
            }
        }

        private Vector3 CalculateNewPosition(Vector3 mouseDelta)
        {
            var newPosition = transform.position + mouseDelta;

            var center = new Vector3(0, 5.3f, transform.position.z);
            var distance = Vector3.Distance(newPosition, center);
            var direction = (newPosition - center).normalized;
            var constrainedPosition = center + direction * 5.0f;

            return distance <= 5.0f ? newPosition : constrainedPosition;
        }

        private void MoveAlongZAxis()
        {
            var currentPosition = transform.position;
            currentPosition.z += zAxisMovementSpeed;
            transform.position = currentPosition;
        }

        private void HandleCollision(GameObject other)
        {
            GameManager.instance.IncrementScore();
            Destroy(other);
        }
    }
}