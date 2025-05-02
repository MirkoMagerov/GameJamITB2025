using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelWordsSO", menuName = "ScriptableObjects/LevelWordsSO")]
public class LevelWordsSO : ScriptableObject
{
    [SerializeField]
    public Phrase[] phrasesLevel1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


[Serializable]
public class Phrase
{
    public string phrase;
    public string[] words;
}
