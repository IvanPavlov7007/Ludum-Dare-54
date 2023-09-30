using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Punchable
{
    public AudioClip meme;
    AudioSource aud;

    private void Start()
    {
        aud = GetComponentInChildren<AudioSource>();
    }

    public void Punch()
    {
        aud.PlayOneShot(meme);
    }
}
