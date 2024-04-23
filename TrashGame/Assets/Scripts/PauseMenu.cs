using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    [SerializeField] private GameObject pauseMenuUi;
    private GameObject FillIndicatorsUi;
    private GameObject ResetScore;

    private void Awake()
    {
        FillIndicatorsUi = GameObject.Find("FillinIndicators");
        ResetScore = GameObject.Find("ResetScore");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                //Cursor.visible = false;
                Resume();

            }
            else
            {
                //Cursor.visible = true;
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        FillIndicatorsUi.SetActive(true);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    private void Pause()
    {
        pauseMenuUi.SetActive(true);
        FillIndicatorsUi.SetActive(false);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
        ResetScore.SetActive(true);
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
