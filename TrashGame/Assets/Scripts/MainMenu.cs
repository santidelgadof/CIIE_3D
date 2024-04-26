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
        GameManager.Instance.StartGame();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Instructions()
    {
        instructionsScreen.SetActive(!instructionsScreen.activeSelf);
    }

    public void Options()
    {
        optionsScreen.SetActive(!optionsScreen.activeSelf);
    }

    public void LeaderBoard()
    {
        leaderBoardScreen.SetActive(!leaderBoardScreen.activeSelf);
    }
}
