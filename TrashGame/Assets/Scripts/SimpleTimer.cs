using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimpleTimer : MonoBehaviour
{
    public float minutes;

    [SerializeField] public TextMeshProUGUI text;
    public GameObject winCanvas;
    private float targetTime;

    void Start()
    {
        targetTime = minutes * 60f;
        int wholeMinutes = Mathf.FloorToInt(targetTime / 60);
        int seconds = Mathf.FloorToInt(targetTime % 60);
        string formattedTime = string.Format("{0:00}:{1:00}", wholeMinutes, seconds);
        text.text = formattedTime;
    }

    void Update()
    {
        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            text.text = "0:00";
            timerEnded();
        } else {

        int wholeMinutes = Mathf.FloorToInt(targetTime / 60);
        int seconds = Mathf.FloorToInt(targetTime % 60);
        string formattedTime = string.Format("{0:00}:{1:00}", wholeMinutes, seconds);
        text.text = formattedTime;
        }
    }

    void timerEnded()
    {
        winCanvas.SetActive(true);
        Time.timeScale = 0;
    }
}
