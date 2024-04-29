using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseOrWinMenu : MonoBehaviour
{
    public static LoseOrWinMenu Instance;

    private int finalScene = 3;
    private int endlessScene = 4;
    [SerializeField] private GameObject finalWindow;

    private void Awake()
    {
        Instance = this;
    }

    public void Win() {
        /// TODO: Open Next scene
        
        if(SceneManager.GetActiveScene().buildIndex == finalScene)
        {
            gameObject.SetActive(false);
            finalWindow.SetActive(true);
        }
        else
        {
            GameManager.Instance.NextLevel();
        }
    }

    public void Lose() {
        if (SceneManager.GetActiveScene().buildIndex == endlessScene)
        {
            gameObject.SetActive(false);
            finalWindow.SetActive(true);
        }
        else
            GameManager.Instance.BackToMenu();
        
    }

    public void No(){}

}

