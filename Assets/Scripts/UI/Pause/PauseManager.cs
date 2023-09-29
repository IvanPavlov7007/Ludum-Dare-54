using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class PauseManager : MonoBehaviour
{
    StateMachine uiStateMachine;

    private void Start()
    {
        uiStateMachine = GetComponentInChildren<StateMachine>();
        Hide();
    }

    bool activated;

    public void Hide()
    {
        uiStateMachine.gameObject.SetActive(false);
        activated = false;
    }

    public void Show()
    {
        uiStateMachine.gameObject.SetActive(true);
        //uiStateMachine.ChangeState("Pause Menu");
        activated = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (activated)
                Hide();
            else
                Show();
        }
    }
}
