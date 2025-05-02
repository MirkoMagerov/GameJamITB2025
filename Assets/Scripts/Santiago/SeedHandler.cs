using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeedHandler : MonoBehaviour
{
    [SerializeField]
    private LevelWordsSO levelWordsSO;
    [SerializeField]
    private List<WordHandler> words1 = new List<WordHandler>();
    [SerializeField]
    private List<WordHandler> words2 = new List<WordHandler>();
    [SerializeField]
    private List<WordHandler> words3 = new List<WordHandler>();
    [SerializeField]
    private List<WordHandler> words4 = new List<WordHandler>();
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetWords(int compasIndex)
    {
        // Get the words from the LevelWordsSO
        Phrase[] phrases = levelWordsSO.phrasesLevel;
        // Loop through the words and set them in the WordHandler
        for (int i = 0; i < words1.Count; i++)
        {
            words1[i].GetComponent<WordHandler>().Word.word = phrases[0].words[i].word;
            words1[i].GetComponent<WordHandler>().Word.points = phrases[0].words[i].points;
            words1[i].ChangeWord();
        }
        for (int i = 0; i < words2.Count; i++)
        {
            words2[i].GetComponent<WordHandler>().Word.word = phrases[1].words[i].word;
            words2[i].GetComponent<WordHandler>().Word.points = phrases[1].words[i].points;
            words2[i].ChangeWord();
        }
        for (int i = 0; i < words3.Count; i++)
        {
            words3[i].GetComponent<WordHandler>().Word.word = phrases[2].words[i].word;
            words3[i].GetComponent<WordHandler>().Word.points = phrases[2].words[i].points;
            words3[i].ChangeWord();
        }
        for (int i = 0; i < words4.Count; i++)
        {
            words4[i].GetComponent<WordHandler>().Word.word = phrases[3].words[i].word;
            words4[i].GetComponent<WordHandler>().Word.points = phrases[3].words[i].points;
            words4[i].ChangeWord();
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
