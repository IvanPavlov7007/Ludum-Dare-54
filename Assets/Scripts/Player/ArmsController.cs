using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmsController : MonoBehaviour
{
    Image image;
    SpriteRenderer spriteRenderer;
    public Sprite idle, left, right;
    public float changeBackTime = 0.3f;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        image = GetComponentInChildren<Image>();
    }

    bool flipped;
    public void Punch()
    {
        //spriteRenderer.sprite 
        image.sprite = flipped ? right : left;
        //Run.After(changeBackTime, () => { spriteRenderer.sprite = idle; });
        Run.After(changeBackTime, () => { image.sprite = idle; });
        flipped = !flipped;
    }
}
