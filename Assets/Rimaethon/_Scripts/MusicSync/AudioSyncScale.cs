using System.Collections;
using Rimaethon._Scripts.MusicSync;
using UnityEngine;

public class AudioSyncScale : AudioSyncer
{
    private float restScaleY = 0.1f;
    private Coroutine[] beatCoroutines;

    private void Awake()
    {
        base.Awake();
        beatCoroutines = new Coroutine[_childObjects.Length];
    }

    private IEnumerator MoveToScale(Transform target, float targetY, float scaleTime)
    {
        float initialY = target.localScale.y;
        float timeCounter = 0;

        while (Mathf.Abs(target.localScale.y - targetY) > 0.01f)
        {
            float newY = Mathf.Lerp(initialY, targetY, timeCounter / scaleTime);
            timeCounter += Time.deltaTime;
            target.localScale = new Vector3(target.localScale.x, newY, target.localScale.z);
            yield return null;
        }

        target.localScale = new Vector3(target.localScale.x, targetY, target.localScale.z);
        isBeat = false;
    }

    protected override void OnBeat(int barIndex)
    {
        base.OnBeat(barIndex);

        Transform childToScale = _childObjects[barIndex];
        float scaleToUse = AudioSpectrum.AveragedSpectrum[barIndex];

        if (beatCoroutines[barIndex] != null)
            StopCoroutine(beatCoroutines[barIndex]);

        beatCoroutines[barIndex] = StartCoroutine(MoveToScale(childToScale, scaleToUse, timeToBeat));
        StartCoroutine(MoveToScale(childToScale, restScaleY, restSmoothTime));
    }
}