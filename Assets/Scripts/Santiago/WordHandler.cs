using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordHandler : MonoBehaviour
{
    public TMP_Text wordText;
    public Word Word;

    public void ChangeWord()
    {
        wordText.text = Word.word;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
