using UnityEngine;
using System.Collections;
using Rimaethon._Scripts.MusicSync;

public class AudioSyncScale : AudioSyncer 
{
    [SerializeField] private float beatScaleY=2f;
    private float restScaleY=0.1f;


    private IEnumerator MoveToScale(float targetY)
    {
        Debug.Log($"Starting scale change to {targetY}");

        float initialY = transform.localScale.y;
        float timeCounter = 0;
        while (Mathf.Abs(transform.localScale.y - targetY) > 0.01f) // Comparison with a small tolerance
        {
            float newY = Mathf.Lerp(initialY, targetY, timeCounter / timeToBeat);
            timeCounter += Time.deltaTime;

            transform.localScale = new Vector3(transform.localScale.x, newY, transform.localScale.z);
            yield return null;
        }
        transform.localScale = new Vector3(transform.localScale.x, targetY, transform.localScale.z); // Ensure exact value
        isBeat = false;
        Debug.Log("Finished scale change");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (isBeat) return;

        float newY = Mathf.Lerp(transform.localScale.y, restScaleY, restSmoothTime * Time.deltaTime);
        transform.localScale = new Vector3(transform.localScale.x, newY, transform.localScale.z);
    }

    public override void OnBeat()
    {
        base.OnBeat();
        Debug.Log("Beat detected");
        StopCoroutine(MoveToScale(beatScaleY));
        StartCoroutine(MoveToScale(beatScaleY));
    }
}