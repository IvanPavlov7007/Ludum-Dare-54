using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton, input for the pause 
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public bool timeStopped { get; private set; }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            timeStopped = !timeStopped;
            if (timeStopped)
                Time.timeScale = 0f;
            else
                Time.timeScale = 1f;
        }
    }


    // ToDo: add Properties AND Events
    public void StopTime()
    {

    }

    public void ContinueTime()
    {

    }

    public void StopInputs()
    {

    }

    public void ContinueInputs()
    {

    }

}
