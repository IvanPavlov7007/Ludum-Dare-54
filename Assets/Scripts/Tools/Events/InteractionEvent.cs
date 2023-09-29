using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionEvent : OneShotEvent
{
    public float dist;
    protected bool waitFrame = false;
    protected virtual void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < dist && !played)
        {
            if(Input.GetKeyDown(KeyCode.E) && waitFrame)
            {
                action.Invoke();
                played = true;
            }

            waitFrame = true;
        }
    }

    public override void Reset()
    {
        base.Reset();
        waitFrame = false;
    }
}
