using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RangeEvent : OneShotEvent
{
    public float dist;
    protected void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < dist && !played)
        {
            played = true;
            action.Invoke();
        }
    }
}
