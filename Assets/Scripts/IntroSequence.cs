using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Events;
using Pixelplacement;
public class IntroSequence : MonoBehaviour
{
    public Image image;
    public Sprite[] sprites;
    Timer timer;
    public TextMeshProUGUI hint;
    int currentSprite = -1;

    public UnityEvent lastAction;

    public float timeToLookAt = 1f;
    public void Start()
    {
        showNextImage();
    }

    void showNextImage()
    {
        currentSprite++;
        if (currentSprite >= sprites.Length)
        {
            if (lastAction != null)
                lastAction.Invoke();
            return;
        }

        timer = new Timer(timeToLookAt);
        image.sprite = sprites[currentSprite];

        allowedToGoToNextSlide = true;
    }

    bool allowedToGoToNextSlide;

    private void Update()
    {
        //if(timer != null)
        //{
        //    if(timer.TimeOut)
        //    {
        //        allowedToGoToNextSlide = true;
        //    }
        //}
    }

    void OnE(InputValue value)
    {
        if(value.isPressed)
            if(allowedToGoToNextSlide)
            {
                showNextImage();
            }
    }
}
