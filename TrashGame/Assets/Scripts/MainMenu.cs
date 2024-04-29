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
    [SerializeField] GameObject CasualLB;
    [SerializeField] GameObject EndlessLB;
    [SerializeField] GameObject GameModeScreen;
    [SerializeField] LeaderBoard LB;
    private void Awake()
    {
        Instance = this;
    }

    public void Play()
    {
        optionsScreen.SetActive(false);
        leaderBoardScreen.SetActive(false);
        instructionsScreen.SetActive(false);

        GameModeScreen.SetActive(!GameModeScreen.activeSelf);
        //GameManager.Instance.StartGame();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Casual()
    {
        GameManager.Instance.StartGame();
    }

    public void Endless()
    {
        GameManager.Instance.StartEndlessGame();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Instructions()
    {
        optionsScreen.SetActive(false);
        leaderBoardScreen.SetActive(false);
        GameModeScreen.SetActive(false);
        instructionsScreen.SetActive(!instructionsScreen.activeSelf);
    }

    public void Options()
    {
        instructionsScreen.SetActive(false);
        leaderBoardScreen.SetActive(false);
        GameModeScreen.SetActive(false);
        optionsScreen.SetActive(!optionsScreen.activeSelf);
    }

    public void LeaderBoard()
    {
        instructionsScreen.SetActive(false);
        optionsScreen.SetActive(false);
        GameModeScreen.SetActive(false);
        leaderBoardScreen.SetActive(!leaderBoardScreen.activeSelf);
        LB.GetLeaderBoard();
        LB.GetLeaderBoardEndless();
    }

   

    public void SwitchLeaderBoard()
    {
        CasualLB.SetActive(!CasualLB.activeSelf);
        EndlessLB.SetActive(!EndlessLB.activeSelf);
    }

    

   
}
