using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
/// <summary>
/// Singleton, input for the pause 
/// </summary>
public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public Door door;
    public Bed bed;
    public Jailer jailer;
    public Hole hole;

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
