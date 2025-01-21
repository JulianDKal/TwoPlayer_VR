using System;
using UnityEngine;

public class Audioplayer : MonoBehaviour
{
    public AudioSource backgroundMusic;

    private void Awake()
    {
        backgroundMusic.Play();
        backgroundMusic.loop = true;
    }
}
