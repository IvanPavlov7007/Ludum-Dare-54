using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class Bed : MonoBehaviour, Punchable
{
    public Vector3 closedPos, openedPose;

    public float cooldownTime = 2f;
    Timer timer;

    public bool isOpen { get; private set; }

    private void Awake()
    {
        timer = new Timer(cooldownTime,true);
    }

    private void Update()
    {
        timer.UpdateTime(Time.deltaTime);
    }
    public void Punch(Vector3 position, Vector3 direction, float impulse)
    {
        if(timer.TimeOut)
        {
            timer.Reset();
            MoveBed();
        }    
    }

    public void MoveBed()
    {
        isOpen = !isOpen;
        Tween.Position(transform, isOpen ? openedPose : closedPos, cooldownTime, 0f);
    }
}
