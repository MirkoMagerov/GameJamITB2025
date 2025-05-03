using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelWordsSO", menuName = "ScriptableObjects/LevelWordsSO")]
public class LevelWordsSO : ScriptableObject
{
    [SerializeField]
    public Phrase[] phrasesCompass;
}

[Serializable]
public class Phrase
{
    public string phrase;
    public Word[] words;
}

[Serializable]
public class Word
{
    public int points;
    public string word;
    public int idWordSlot;

    public Word(string word, int points)
    {
        this.word = word;
        this.points = points;
    }
}
