using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Dan.Main;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> namesC;
    [SerializeField] private List<TextMeshProUGUI> scoresC;
    [SerializeField] private List<TextMeshProUGUI> namesE;
    [SerializeField] private List<TextMeshProUGUI> scoresE;

    private string publicLeaderBoardKey = "b358e015d6cd5fb4fc6aab834193a7613aba9b48fd4c8987fb1428d05456c0c6";
    private string EndlessKey = "a9402ba04f95fffcfbfc9cc9e2ca09548edb314b35661da1cb2b85418fdd0a16";

    private void Start()
    {
        GetLeaderBoard();
    }

    public void GetLeaderBoard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderBoardKey, ((msg) =>
        {
            int loopLenght = (msg.Length < namesC.Count) ? msg.Length : namesC.Count;

            for(int i = 0; i<loopLenght; ++i)
            {
                namesC[i].text = msg[i].Username;
                scoresC[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void GetLeaderBoardEndless()
    {
        LeaderboardCreator.GetLeaderboard(EndlessKey, ((msg) =>
        {
            int loopLenght = (msg.Length < namesE.Count) ? msg.Length : namesE.Count;

            for (int i = 0; i < loopLenght; ++i)
            {
                namesE[i].text = msg[i].Username;
                scoresE[i].text = msg[i].Score.ToString();
            }
        }));
    }



    public void SetLeaderBoard(string username, int score, bool casual)
    {

        LeaderboardCreator.UploadNewEntry(casual ? publicLeaderBoardKey : EndlessKey, username, score, ((msg) =>
        {
            LeaderboardCreator.ResetPlayer();
            GetLeaderBoard();
        }));
    }

    
}
