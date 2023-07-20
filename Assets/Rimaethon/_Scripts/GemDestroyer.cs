using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemDestroyer : MonoBehaviour
{
    private GameObject magicOrb;

    private void Start()
    {
        magicOrb=GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (gameObject.transform.position.z+0.1f < magicOrb.transform.position.z)
        {
            Destroy(gameObject);
        }
    }
}
