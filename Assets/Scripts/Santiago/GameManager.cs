using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player[] players = new Player[2];
    public bool isPlayer1Attacking = true;
    public static GameManager Instance;
    public int currentPhrase;

    public int pointsPlayer1;
    public int pointsPlayer2;
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
        if (currentPhrase % 4 != 0)
            return;
        isPlayer1Attacking = !isPlayer1Attacking;
        if (isPlayer1Attacking == false)
        {
            players[0].poemHandler.gameObject.SetActive(false);
            players[0].TimerHandler.gameObject.SetActive(false);
            players[0].laudHanderl.gameObject.SetActive(true);

            players[1].poemHandler.gameObject.SetActive(true);
            players[1].TimerHandler.gameObject.SetActive(true);
            players[1].laudHanderl.gameObject.SetActive(false);
        }
        else
        {
            players[1].poemHandler.gameObject.SetActive(false);
            players[1].TimerHandler.gameObject.SetActive(false);
            players[1].laudHanderl.gameObject.SetActive(true);

            players[0].poemHandler.gameObject.SetActive(true);
            players[0].TimerHandler.gameObject.SetActive(true);
            players[0].laudHanderl.gameObject.SetActive(false);
        }
    }
}
