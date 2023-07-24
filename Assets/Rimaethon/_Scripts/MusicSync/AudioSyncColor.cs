﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Rimaethon._Scripts.MusicSync
{
    [RequireComponent(typeof(Image))]
    public class AudioSyncColor : AudioSyncer 
    {
        private Image imageComponent;
        private Color[] beatColors = {Color.red, Color.blue, Color.green};

        private Color restColor = Color.white; // Default to white color
        private int randomIndex;

        private void Start()
        {
            imageComponent = GetComponent<Image>();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (!isBeat)
            {
                imageComponent.color = Color.Lerp(imageComponent.color, restColor, restSmoothTime * Time.deltaTime);
            }
        }

        public override void OnBeat()
        {
            base.OnBeat();
            StopCoroutine(MoveToColor(beatColors[randomIndex]));
            randomIndex = Random.Range(0, beatColors.Length);
            StartCoroutine(MoveToColor(beatColors[randomIndex]));
        }

        private IEnumerator MoveToColor(Color target)
        {
            float timer = 0;
            Color currentColor = imageComponent.color;
            while (currentColor != target)
            {
                currentColor = Color.Lerp(currentColor, target, timer / timeToBeat);
                timer += Time.deltaTime;
                imageComponent.color = currentColor;
                yield return null;
            }
            isBeat = false;
        }
    }
}