using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    [SerializeField] private GameObject leftPlayer;
    [SerializeField] private GameObject rightPlayer;

    public void SetWinner(int winner)
    {
        if (winner == -1)
        {
            leftPlayer.SetActive(true);
        }
        else
        {
            rightPlayer.SetActive(true);
        }
    }

    public void ResetGame()
    {
        leftPlayer.SetActive(false);
        rightPlayer.SetActive(false);
        GameManager.Instance.ResetGame();
    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
