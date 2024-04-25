using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;
    [SerializeField] GameObject instructionsScreen;
    [SerializeField] GameObject optionsScreen;
    [SerializeField] GameObject leaderBoardScreen;
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
        instructionsScreen.SetActive(true);
    }

    public void Options()
    {
        optionsScreen.SetActive(true);
    }

    public void LeaderBoard()
    {
        leaderBoardScreen.SetActive(true);
    }
}
