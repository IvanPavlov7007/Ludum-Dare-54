using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite idle, left, right;
    public float changeBackTime = 0.3f;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    bool flipped;
    public void Punch()
    {
        spriteRenderer.sprite = flipped? right : left;
        Run.After(changeBackTime, () => { spriteRenderer.sprite = idle; });
        flipped = !flipped;
    }
}
