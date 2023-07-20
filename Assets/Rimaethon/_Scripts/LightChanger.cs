using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChanger : MonoBehaviour
{
    public float x,y,z;
    public float r,g,b;
    public float h, s, v;
    Color outputColor;
   
    float randomSign;
    Light lt;
    // Start is called before the first frame update
    void Start()
    {
        lt = GetComponent<Light>();
        Color.RGBToHSV(lt.color,out h,out s, out v);
        
        StartCoroutine(RandomizeColor());
        StartCoroutine(RandomizeRotation());
        
    }

    // Update is called once per frame
    void Update()
    {
        lt.color=Color.HSVToRGB(h, s, v);
        lt.color = new Color(lt.color.r, lt.color.g, lt.color.b, 0f); 

        transform.Rotate(x*randomSign, y * randomSign, 0);
        
        
    }
    IEnumerator RandomizeColor()
    {
        while (true)
        {
            h = Random.Range(0f, 1f);

            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator RandomizeRotation()
    {
        while (true)
        {
            x = Random.Range(65f, 111f);
            y = Random.Range(65f, 111f);
            randomSign = Mathf.Sign(Random.Range(-1, 1));
            yield return new WaitForSeconds(1f);
        }
    }
}
