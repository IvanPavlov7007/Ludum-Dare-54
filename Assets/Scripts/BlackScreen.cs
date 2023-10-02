using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pixelplacement;
public class BlackScreen :  Singleton<BlackScreen>
{
    Image image;
    void Start()
    {
        image = GetComponent<Image>();
    }

    [Range(0f,1f)]
    public float value;
    public void HideOverTime(float time)
    {
        StartCoroutine(hidingRoutine(time));
    }

    IEnumerator hidingRoutine(float time)
    {
        float elapsedTime = 0f;
        while (elapsedTime <= time)
        {
            value = 1f - elapsedTime / time;
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
        }
        value = 0f;
    }

    IEnumerator showingRoutine(float time)
    {
        float elapsedTime = 0f;
        while(elapsedTime <= time)
        {
            value = elapsedTime / time;
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
        }
        value = 1f;
    }

    public void ShowOverTime(float time)
    {
        StartCoroutine(showingRoutine(time));
    }

    void Update()
    {
        var c = image.color;
        c.a = value;
        image.color = c;
    }
}
