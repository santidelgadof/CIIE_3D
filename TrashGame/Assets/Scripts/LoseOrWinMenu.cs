using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseOrWinMenu : MonoBehaviour
{
    public static LoseOrWinMenu Instance;

    [SerializeField] private int finalScene = 1;
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
            gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void Lose() {
        GameManager.Instance.UpdateGameState(GameState.StartMenu);
        //SceneManager.LoadScene("SampleScene");
    }

    public void No(){}

}

