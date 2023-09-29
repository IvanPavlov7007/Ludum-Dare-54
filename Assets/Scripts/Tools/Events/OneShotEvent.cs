using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OneShotEvent : MonoBehaviour
{
    protected Transform player;
    public UnityEvent action;

    protected virtual void Start()
    {
        player = PlayerMovement.Instance.transform;
    }
    protected bool played = false;

    public virtual void Reset()
    {
        played = false;
    }
}
