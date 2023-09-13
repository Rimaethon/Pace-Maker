using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class VfxColorRandomizer : MonoBehaviour
{
    private Vector4 _colorVector;
    private VisualEffect _vfx;

    private void Start()
    {
        _vfx = GetComponent<VisualEffect>();
        _colorVector = new Vector4();
        StartCoroutine(RandomizeColorVFX());
    }

    private void Update()
    {
        SetVFXColor();
    }

    private IEnumerator RandomizeColorVFX()
    {
        while (true)
        {
            _colorVector.x = Random.Range(0.2f, 1f);
            _colorVector.y = Random.Range(0.2f, 1f);
            _colorVector.z = Random.Range(0.2f, 1f);
            yield return new WaitForSeconds(1f);
        }
    }

    private void SetVFXColor()
    {
        _vfx.SetVector4("Color2", _colorVector);
        _vfx.SetVector4("Color", _colorVector);
    }
}