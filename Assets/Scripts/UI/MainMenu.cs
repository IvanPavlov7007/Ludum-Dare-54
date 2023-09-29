using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MenuBase
{
    public string sceneToLoad;

    public void OpenSettings()
    {
        uiStateMachine.ChangeState("Settings");
    }

    public void StartGame()
    {
        IntersceneData.LoadNextScene(sceneToLoad);
    }

    public void Quit()
    {
        IntersceneData.Quit();
    }
}
