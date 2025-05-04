using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isplaying = true;
    public Player[] players = new Player[2];
    public bool leftPlayerPoem = true;
    public int currentPhrase;
    [SerializeField] private GameObject victoryCanvas;
    public int phrasesPerRound = 2;
    internal static Action<int> OnEndOfCompass;
    public static Action SwapTurn;
    public static Action OnEndOfGame;

    // Start is called before the first frame update
    void Start()
    {
        int randomPlayer = UnityEngine.Random.Range(0, 2);
        leftPlayerPoem = randomPlayer == 0;

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        ScoreManager.Instance.OnEndOfGame += EndOfGame;
        ChangeOfTurn();
    }

    void OnDisable()
    {
        ScoreManager.Instance.OnEndOfGame -= EndOfGame;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeOfTurn();
        }
    }

    public void EndOfPhrase()
    {
        currentPhrase++;
        Debug.Log(currentPhrase);
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
        players[0].poemGameObject.SetActive(false);
        players[0].laudGameObject.SetActive(false);
        players[1].poemGameObject.SetActive(false);
        players[1].laudGameObject.SetActive(false);
    }

    public void ResetGame()
    {
        victoryCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentPhrase = 0;
        isplaying = true;
        leftPlayerPoem = !leftPlayerPoem;
        ChangeOfTurn();
        ScoreManager.Instance.ResetScore();
    }
}
