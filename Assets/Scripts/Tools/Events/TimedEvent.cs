using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedEvent : OneShotEvent
{

    public bool onStart, unscaledTime;
    public float triggerTime;

    protected override void Start()
    {
        base.Start();
        if (onStart) 
            StartTimer();
    }

    bool started;
    float t = 0;
    public void StartTimer()
    {
        started = true;
        t = triggerTime;
    }

    public override void Reset()
    {
        base.Reset();
        started = false;
    }

    private void Update()
    {
        if (started)
        {
            t -= unscaledTime? Time.unscaledDeltaTime : Time.deltaTime;
            if (t <= 0f && !played)
            {
                played = true;
                action.Invoke();
            }
        }
    }
}
