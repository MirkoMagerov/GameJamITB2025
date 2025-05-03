using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isplaying = true;
    public Player[] players = new Player[2];
    public GameObject laud;
    public bool leftPlayerPoem = true;
    public int currentPhrase;

    public int pointsPlayer1;
    public int pointsPlayer2;
    public int phrasesPerRound = 2;
    internal static Action<int> OnEndOfCompass;

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
        OnEndOfCompass?.Invoke(currentPhrase);
        ChangeOfTurn();
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
        // if (currentPhrase % phrasesPerRound != 0)
        //     return;
        leftPlayerPoem = !leftPlayerPoem;
        if (!leftPlayerPoem)
        {
            players[0].poemGameObject.gameObject.SetActive(false);
            players[0].TimerHandler.gameObject.SetActive(false);
            players[0].laudGameObject.gameObject.SetActive(true);

            players[1].poemGameObject.gameObject.SetActive(true);
            players[1].TimerHandler.gameObject.SetActive(true);
            players[1].laudGameObject.gameObject.SetActive(false);
        }
        else
        {
            players[1].poemGameObject.gameObject.SetActive(false);
            players[1].TimerHandler.gameObject.SetActive(false);
            players[1].laudGameObject.gameObject.SetActive(true);

            players[0].poemGameObject.gameObject.SetActive(true);
            players[0].TimerHandler.gameObject.SetActive(true);
            players[0].laudGameObject.gameObject.SetActive(false);
        }
    }

    public void OnEnable()
    {
        ScoreManager.OnEndOfGame += EndOfGame;
    }

    private void EndOfGame(int playerWin)
    {
        Debug.Log($"Player {playerWin}");
        players[0].poemGameObject.gameObject.SetActive(!players[0].poemGameObject.gameObject.activeSelf);
        players[0].TimerHandler.gameObject.SetActive(!players[0].TimerHandler.gameObject.activeSelf);
        players[0].laudGameObject.gameObject.SetActive(!players[0].laudGameObject.gameObject.activeSelf);
        players[1].poemGameObject.gameObject.SetActive(!players[1].poemGameObject.gameObject.activeSelf);
        players[1].TimerHandler.gameObject.SetActive(!players[1].TimerHandler.gameObject.activeSelf);
        players[1].laudGameObject.gameObject.SetActive(!players[1].laudGameObject.gameObject.activeSelf);
    }
}
