using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalMenu : MonoBehaviour
{
    [SerializeField] private FloatOs scoreSo;
    [SerializeField] private LeaderBoardData leaderList;
    [SerializeField] private TextMeshProUGUI scoreText;

    private float score;
    
    private void Start()
    {
        Time.timeScale = 0f;
        if (scoreText != null)
        {
            score = scoreSo.Value;
            scoreText.text = score.ToString();
        }
    }

    public void Submit()
    {

        if (name != null)
        {
            leaderList.AddNewScore(name, score);
        }
        else
        {
            //handle error (msg or smth)
        }

        gameObject.SetActive(false);
        // Game manager que te lleve al menu de inicio
        Time.timeScale = 1f;
    }

  

    
}
