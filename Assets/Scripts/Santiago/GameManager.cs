using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentCompass;
    public List<WordBoxHandler> wordBoxHandlers = new List<WordBoxHandler>();

    public int totalPoints;
    internal static Action<int> OnEndOfCompass;

    // Start is called before the first frame update
    void Start()
    {
        OnEndOfCompass?.Invoke(currentCompass);
        foreach (WordBoxHandler wordBoxHandler in wordBoxHandlers)
        {
            wordBoxHandler.EndOfCompass();
            totalPoints += wordBoxHandler.points;
        }
    }

    public void End()
    {
        foreach (WordBoxHandler wordBoxHandler in wordBoxHandlers)
        {
            wordBoxHandler.EndOfCompass();
            totalPoints += wordBoxHandler.points;
        }
        currentCompass++;
        OnEndOfCompass?.Invoke(currentCompass);
        Debug.Log("Total Points: " + totalPoints);
    }
}
