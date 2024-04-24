using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void Play()
    {
        GameManager.Instance.UpdateGameState(GameState.Level1);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Instructions()
    {

    }

    public void Options()
    {

    }

    public void LeaderBoard()
    {

    }
}
