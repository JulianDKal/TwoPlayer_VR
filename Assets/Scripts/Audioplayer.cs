using System;
using UnityEngine;

public class Audioplayer : MonoBehaviour
{
    AudioSource backgroundMusic;

    private void Awake()
    {
        backgroundMusic.Play();
        backgroundMusic.loop = true;
    }
}
