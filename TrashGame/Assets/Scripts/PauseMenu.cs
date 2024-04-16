using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    [SerializeField] private GameObject pauseMenuUi;

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
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    private void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
