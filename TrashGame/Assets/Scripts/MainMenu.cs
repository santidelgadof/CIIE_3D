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
    [SerializeField] LeaderBoard LB;
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
        optionsScreen.SetActive(false);
        leaderBoardScreen.SetActive(false);
        instructionsScreen.SetActive(!instructionsScreen.activeSelf);
    }

    public void Options()
    {
        instructionsScreen.SetActive(false);
        leaderBoardScreen.SetActive(false);
        optionsScreen.SetActive(!optionsScreen.activeSelf);
    }

    public void LeaderBoard()
    {
        instructionsScreen.SetActive(false);
        optionsScreen.SetActive(false);
        leaderBoardScreen.SetActive(!leaderBoardScreen.activeSelf);
        LB.GetLeaderBoard();
    }

   
}
