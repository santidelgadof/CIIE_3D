using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SimpleTimer : MonoBehaviour
{
    public float minutes;

    [SerializeField] public TextMeshProUGUI text;
    public GameObject winCanvas;
    private float targetTime;
    private bool timeEnd = false;
   

    void Start()
    {
        targetTime = minutes * 100f;
        int wholeMinutes = Mathf.FloorToInt(targetTime / 60);
        int seconds = Mathf.FloorToInt(targetTime % 60);
        string formattedTime = string.Format("{0:00}:{1:00}", wholeMinutes, seconds);
        text.text = formattedTime;
    }

    void Update()
    {
        if (!timeEnd)
        {
            if (targetTime <= 0.0f)
            {
                timeEnd = true;

                text.text = "0:00";

                timerEnded();

                //gameObject.SetActive(false); //Volver a activar m�s tarde
            }
            else
            {
                targetTime -= Time.deltaTime;
                int wholeMinutes = Mathf.FloorToInt(targetTime / 60);
                int seconds = Mathf.FloorToInt(targetTime % 60);
                string formattedTime = string.Format("{0:00}:{1:00}", wholeMinutes, seconds);
                text.text = formattedTime;
            }
        }
        else
        {
            if(targetTime <= 0.0f)
            {
                Time.timeScale = 0;
            }
        }
    }

    void timerEnded()
    {
       
        winCanvas.SetActive(true);
        Time.timeScale = 0;
        
    }

    public void activateTimer()
    {
        gameObject.SetActive(true);
    }
}
