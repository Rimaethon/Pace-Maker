using System;
using UnityEngine;

namespace Rimaethon._Scripts
{
    public class MoveObjectOnZAxisConstantly : MonoBehaviour
    {
        private const float Speed = 2f;
        private void FixedUpdate()
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }
    }
}
