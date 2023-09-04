using UnityEngine;

namespace Rimaethon._Scripts.Utility
{
    public class DestroyIfPassed : MonoBehaviour
    {
        private GameObject player;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.LogWarning("No GameObject with the tag 'Player' was found in the scene.");
            }
        }

        void Update()
        {
            if (player != null && player.transform.position.z > transform.position.z)
            {
                Destroy(gameObject);
            }
        }
    }
}