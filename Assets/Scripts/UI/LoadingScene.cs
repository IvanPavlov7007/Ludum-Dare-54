using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// Use for asset management during loading
/// </summary>
public class LoadingScene : MonoBehaviour
{
    AsyncOperation asyncOperation;
    public string sceneName;
    void Start()
    {
        asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;
    }

    public void Continue()
    {
        asyncOperation.allowSceneActivation = true;
    }

    public float currentLoadingTime = 0f;
}
