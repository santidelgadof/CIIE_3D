using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
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

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.StartMenu:
                SceneManager.LoadScene(0);
                break;
            case GameState.Level1:
                SceneManager.LoadScene(1);
                break;
            case GameState.Level2:
                //SceneManager.LoadScene(2);
                break;
            case GameState.Level3:
                //SceneManager.LoadScene(3);
                break;
            case GameState.Lose:
                //SceneManager.LoadScene(4); una escena para pantalla de perder y ganar?? 
                break;
            case GameState.Victory:
                //SceneManager.LoadScene(5);
                break;
            default: break;
        }

        onGameStateChanged?.Invoke(newState);
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
