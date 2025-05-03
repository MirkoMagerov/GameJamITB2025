using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player[] players = new Player[2];
    public GameObject laud;
    public bool isPlayer1Attacking = true;
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

    public void EndOfPhrase()
    {
        currentPhrase++;
        Debug.Log(currentPhrase);
        ChangeOfTurn();
        OnEndOfCompass?.Invoke(currentPhrase);
    }

    public void ChangeOfTurn()
    {
        if (currentPhrase % phrasesPerRound != 0)
            return;
        isPlayer1Attacking = !isPlayer1Attacking;
        if (isPlayer1Attacking == false)
        {
            players[0].poemGameObject.gameObject.SetActive(false);
            players[0].TimerHandler.gameObject.SetActive(false);
            laud.transform.position = players[0].laudGameObject.gameObject.transform.position;

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
            laud.transform.position = players[0].laudGameObject.gameObject.transform.position;
        }
    }
}
