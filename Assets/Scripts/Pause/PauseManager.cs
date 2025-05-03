using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseCanvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        if (pauseCanvas.activeSelf)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        pauseCanvas.SetActive(true);
    }
    private void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        pauseCanvas.SetActive(false);
    }

    public void Settings()
    {

    }

    public void RestartLevel()
    {
        int currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(currentScene);
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
