using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowLeaderBoard : MonoBehaviour
{
    [SerializeField] private LeaderBoardData leaderList;
    [SerializeField] private TextMeshProUGUI LbText;
    private List<(string, float)> list;

    private void Start()
    {
        list = leaderList.Lblist;
        LbText.text = " ";
        PrintLeaderBoard();
    }


    private void PrintLeaderBoard()
    {
        if(LbText != null)
        {
            Debug.Log(list);
            foreach (var pair in list)
            {
                string name = pair.Item1;
                string score = pair.Item2.ToString();
                Debug.Log(name);

                LbText.text += name + ": " + score + "\n";
            }
        }
    }
}
