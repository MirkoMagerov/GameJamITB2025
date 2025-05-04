using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    internal static Action<int> OnEndOfCompass;
    public static Action SwapTurn;
    public static Action OnEndOfGame;
    public static GameManager Instance;
    public bool isplaying = true;
    public Player[] players = new Player[2];
    public bool leftPlayerPoem = true;
    public int currentPhrase;
    [SerializeField] private GameObject victoryCanvas;
    public int phrasesPerRound = 2;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI swapText;
    [SerializeField] private float swapThresholdPercentage = 0.7f;
    private bool hasSwapped = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        ScoreManager.Instance.OnEndOfGame += EndOfGame;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        StartCoroutine(StartGameRoutine());
    }

    void OnDisable()
    {
        ScoreManager.Instance.OnEndOfGame -= EndOfGame;
    }

    public void EndOfPhrase()
    {
        currentPhrase++;
        //ChangeOfTurn();
        OnEndOfCompass?.Invoke(currentPhrase);
    }

    public void ChangeOfTurn()
    {
        SwapTurn?.Invoke();
        // if (currentPhrase % phrasesPerRound != 0)
        //     return;
        leftPlayerPoem = !leftPlayerPoem;
        if (!leftPlayerPoem)
        {
            players[0].poemGameObject.SetActive(false);
            players[0].laudGameObject.SetActive(true);

            players[1].poemGameObject.SetActive(true);
            players[1].laudGameObject.SetActive(false);
        }
        else
        {
            players[1].poemGameObject.SetActive(false);
            players[1].laudGameObject.SetActive(true);

            players[0].poemGameObject.SetActive(true);
            players[0].laudGameObject.SetActive(false);
        }
    }

    private void EndOfGame(int playerWin)
    {
        OnEndOfGame?.Invoke();
        victoryCanvas.SetActive(true);
        victoryCanvas.GetComponent<VictoryManager>().SetWinner(playerWin);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        foreach (var player in players)
        {
            player.poemGameObject.SetActive(false);
            player.laudGameObject.SetActive(false);
        }

        isplaying = false;
    }

    public void ResetGame()
    {
        hasSwapped = false;
        StopAllCoroutines();
        victoryCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentPhrase = 0;
        isplaying = true;
        leftPlayerPoem = !leftPlayerPoem;
        ScoreManager.Instance.ResetScore();
        StartCoroutine(StartGameRoutine());
    }

    private IEnumerator StartGameRoutine()
    {
        yield return null;

        int randomPlayer = UnityEngine.Random.Range(0, 2);
        leftPlayerPoem = randomPlayer == 0;

        yield return CountdownRoutine(ChangeOfTurn);

        isplaying = true;
    }

    private IEnumerator CountdownRoutine(Action onComplete)
    {
        foreach (var player in players)
        {
            player.poemGameObject.SetActive(false);
            player.laudGameObject.SetActive(false);
        }

        countdownText.gameObject.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        swapText.gameObject.SetActive(false);
        countdownText.text = "";
        yield return new WaitForSeconds(0.5f);
        countdownText.gameObject.SetActive(false);
        onComplete?.Invoke();
    }

    public void CheckForForcedSwap()
    {
        if (hasSwapped) return;

        float sliderValue = ScoreManager.Instance.GetCurrentScore();
        float distanceFromCenter = Mathf.Abs(sliderValue - 125f);

        float swapThreshold = 70f * swapThresholdPercentage;

        if (distanceFromCenter >= swapThreshold)
        {
            SwapTurn?.Invoke();
            swapText.gameObject.SetActive(true);
            hasSwapped = true;
            StartCoroutine(CountdownRoutine(ChangeOfTurn));
        }
    }
}
