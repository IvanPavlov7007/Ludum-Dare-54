using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class PauseMenu : MenuBase
{

    bool oneFrameAfterEnableWaited = true;

    private void OnEnable()
    {
        oneFrameAfterEnableWaited = false;
    }

    public void OpenSettings()
    {
        uiStateMachine.ChangeState("Settings");
    }

    public void Resume()
    {
        gameObject.SetActive(false);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        IntersceneData.LoadNextScene("MainMenu");
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        IntersceneData.Quit();
    }
}
