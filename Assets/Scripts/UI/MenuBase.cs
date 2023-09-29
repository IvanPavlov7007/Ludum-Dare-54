using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class MenuBase : MonoBehaviour
{
    protected StateMachine uiStateMachine;
    protected virtual void Start()
    {
        uiStateMachine = GetComponentInParent<StateMachine>();
    }
}
