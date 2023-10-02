using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using UnityEngine.SceneManagement;
/// <summary>
/// Singleton, input for the pause 
/// </summary>
public class GameManager : Singleton<GameManager>
{
    public Door door;
    public Bed bed;
    public Jailer jailer;
    public Hole hole;
    public RoomChange roomChange;

    public GameObject winScreen, loseScreen;
    bool paused = false;
    bool gameOver = false;

    [Range(0f,1f)]
    public float _p;
    public float gameProgress { get {
            //return _p; 
            return hole.progress; 
        } }

    public void Win()
    {
        Debug.Log("You escape!");
        gameOver = true;
        //winScreen.SetActive(true);
        //Time.timeScale = 0f;
    }

    public void Lose()
    {
        Debug.Log("I've seen your hole, You lost!");
        gameOver = true;
        //loseScreen.SetActive(true);
        //Time.timeScale = 0f;
    }

    void Update()
    {
        _p = gameProgress;
        if(gameProgress == 1f)
        {
            roomChange.BreakRoom();
        }
        if (Input.GetKeyDown(KeyCode.R) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
            paused = !paused;
            Time.timeScale = paused ? 0f : 1f;
        }
    }

}
