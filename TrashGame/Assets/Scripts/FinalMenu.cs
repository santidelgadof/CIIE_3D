using Dan.Main;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalMenu : MonoBehaviour
{
    public FinalMenu Instance;
    [SerializeField] private FloatOs scoreSo;
    [SerializeField] private LivesCounter livesCnt;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private AudioSource mainSong;

    private int score;
    private string CasualKey = "b358e015d6cd5fb4fc6aab834193a7613aba9b48fd4c8987fb1428d05456c0c6";
    private string EndlessKey = "a9402ba04f95fffcfbfc9cc9e2ca09548edb314b35661da1cb2b85418fdd0a16";

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 0f;
        if (scoreText != null)
        {
            if (livesCnt.Lives > 0)
            {
                scoreSo.Value += livesCnt.Lives * 50;
            }
            score = (int)scoreSo.Value;
            scoreText.text = score.ToString();
        }
    }

    public void Submit()
    {
        string username;
        string key;
        if (name != "FinalWinMenu") username = name;
        else username = "Player";

        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            key = EndlessKey;
        }
        else
        {
            key = CasualKey;
        }
        LeaderboardCreator.UploadNewEntry(key, username, score, livesCnt.Lives.ToString(), ((msg) =>
        {
            LeaderboardCreator.ResetPlayer();
        }));

        mainSong.Stop();
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.BackToMenu();
        
    }

  

    
}
