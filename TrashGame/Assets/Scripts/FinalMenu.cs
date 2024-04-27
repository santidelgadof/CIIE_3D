using Dan.Main;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FinalMenu : MonoBehaviour
{
    public FinalMenu Instance;
    [SerializeField] private FloatOs scoreSo;
    [SerializeField] private LeaderBoardData leaderList;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score;
    private string publicLeaderBoardKey = "b358e015d6cd5fb4fc6aab834193a7613aba9b48fd4c8987fb1428d05456c0c6";

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 0f;
        if (scoreText != null)
        {
            score = (int)scoreSo.Value;
            scoreText.text = score.ToString();
        }
    }

    public void Submit()
    {
        string username;
        if (name != "FinalWinMenu") username = name;
        else username = "Player";
        LeaderboardCreator.UploadNewEntry(publicLeaderBoardKey, username, score, ((msg) =>
        {
            LeaderboardCreator.ResetPlayer();
        }));

        /*if (name != null)
        {
           leaderList.AddNewScore(name, score);
        }
        else
        {
            leaderList.AddNewScore(" ", score);
        }*/

        gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.BackToMenu();
        
    }

  

    
}
