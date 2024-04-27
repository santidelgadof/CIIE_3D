using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScore : MonoBehaviour
{
    [SerializeField] private FloatOs scoreSO;
    [SerializeField] private LivesCounter lives;
    [SerializeField] private CharacterScript player;


    private void Start()
    {
        scoreSO.Value = 0;
        lives.Lives = 5;
        player.UpdateHeartsUI();
        
    }
}
