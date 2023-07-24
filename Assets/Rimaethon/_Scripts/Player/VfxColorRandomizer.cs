using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VfxColorRandomizer : MonoBehaviour
{
    public float x;
    public float y;
    public float z;
    public float w;
    public VisualEffect vfx;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomizeColorVFX());

    }

    // Update is called once per frame
    void Update()
    {
        
        vfx.SetVector4("Color2", new Vector4(x, y, z, w));
        vfx.SetVector4("Color", new Vector4(x, y, z, w));
        
    }
    IEnumerator RandomizeColorVFX()
    {
        while (true)
        {
            x = Random.Range(-100f, 100f);
            y = Random.Range(-100f, 100f);
            z = Random.Range(-100f, 100f);
            yield return new WaitForSeconds(1f);
        }
    }
}
