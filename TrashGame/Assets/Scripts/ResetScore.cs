using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScore : MonoBehaviour
{
    [SerializeField] private FloatOs scoreSO;

    private void Start()
    {
        scoreSO.Value = 0;
    }
}
