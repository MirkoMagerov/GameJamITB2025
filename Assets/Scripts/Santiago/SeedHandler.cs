using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeedHandler : MonoBehaviour
{
    [SerializeField]
    private LevelWordsSO levelWordsSO;
    [SerializeField]
    private List<WordHandler> words = new List<WordHandler>();

    public void SetWords(int phrase)
    {
        // Get the words from the LevelWordsSO
        Phrase[] phrases = levelWordsSO.verses[0].phrases;
        // Loop through the words and set them in the WordHandler
        for (int i = 0; i < words.Count; i++)
        {
            words[i].GetComponent<WordHandler>().Word.word = phrases[phrase].words[i].word;
            words[i].GetComponent<WordHandler>().Word.points = phrases[phrase].words[i].points;
            words[i].GetComponent<WordHandler>().Word.idWordSlot = phrases[phrase].words[i].idWordSlot;
            words[i].ChangeWord();
        }
    }

    private void OnDisable()
    {
        GameManager.OnEndOfCompass -= SetWords;
    }
    private void OnEnable()
    {
        GameManager.OnEndOfCompass += SetWords;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
