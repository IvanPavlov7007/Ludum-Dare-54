using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
public class Door : MonoBehaviour, Punchable
{
    public float rotationDuration = 0.2f;
    public AudioClip meme;
    public AudioClip openSound;
    AudioSource aud;

    private void Start()
    {
        aud = GetComponentInChildren<AudioSource>();
    }

    bool played;
    public void Punch(Vector3 position, Vector3 direction, float impulse)
    {
        if (played)
            return;
        played = true;
        aud.clip = (meme);
        aud.PlayDelayed(2f);
    }


    public void Open()
    {
        Tween.LocalRotation(transform, Quaternion.Euler(0f, -90f, 0f), rotationDuration,0f);
        aud.PlayOneShot(openSound);
    }

    public void Close()
    {
        Tween.LocalRotation(transform, Quaternion.Euler(0f, 0f, 0f), rotationDuration,0f);
        aud.PlayOneShot(openSound);
    }
}
