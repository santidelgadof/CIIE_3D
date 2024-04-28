using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseOrWinMenu : MonoBehaviour
{
    public static LoseOrWinMenu Instance;

    private int finalScene = 3;
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
        GameManager.Instance.BackToMenu();
        //SceneManager.LoadScene("SampleScene");
    }

    public void No(){}

}

