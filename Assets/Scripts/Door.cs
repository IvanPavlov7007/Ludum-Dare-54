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

    bool played;
    public void Punch(Vector3 position, Vector3 direction, float impulse)
    {
        if (played)
            return;
        played = true;
        aud.clip = (meme);
        aud.PlayDelayed(2f);
    }
}
