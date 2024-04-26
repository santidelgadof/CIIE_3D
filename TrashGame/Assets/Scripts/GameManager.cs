using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] LivesCounter lives;
    [SerializeField] FloatOs score;
    public GameState state;
    public static Action<GameState> onGameStateChanged;
    private void Awake()
    {
        Instance = this;
        
    }

    private void Start()
    {
        
        //UpdateGameState(GameState.StartMenu);
    }
    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void BackToMenu() {
        SceneManager.LoadScene(0);
    }


    public void NextLevel() {
        switch (state) 
        {
            case GameState.StartMenu:
                lives.Lives = 5;
                SceneManager.LoadScene(1);
                break;
            case GameState.Level1:
                SceneManager.LoadScene(2);
                break;
            case GameState.Level2:
                SceneManager.LoadScene(3);
                break;
            case GameState.Level3:
                //SceneManager.LoadScene(4);
                break;
            case GameState.Lose:
                //SceneManager.LoadScene(4); una escena para pantalla de perder y ganar?? 
                break;
            case GameState.Victory:
                //SceneManager.LoadScene(5);
                break;
            default: break;
        }
    }
}
public enum GameState
{
    StartMenu,
    Level1,
    Level2,
    Level3,
    Lose,
    Victory
}
