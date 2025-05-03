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
}
