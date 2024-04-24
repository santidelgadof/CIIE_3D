using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    public static bool isGamePaused = false;
    [SerializeField] private GameObject pauseMenuUi;
    [SerializeField] private GameObject ResetScore;
    private GameObject FillIndicatorsUi;
    

    private void Awake()
    {
        Instance = this;
        FillIndicatorsUi = GameObject.Find("FillinIndicators");
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
        GameManager.Instance.UpdateGameState(GameState.StartMenu);
        //SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        ResetScore.SetActive(true);
        Application.Quit();
    }
}
