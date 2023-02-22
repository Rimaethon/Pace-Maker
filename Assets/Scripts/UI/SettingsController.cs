using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    private float volume;
    private AudioSource audioSource;
    public Slider slider;

    private void Start()
    {
        audioSource=gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        volume = slider.value;
        audioSource.volume = volume;
    }
}
