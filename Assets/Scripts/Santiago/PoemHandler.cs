using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PoemHandler : MonoBehaviour
{
    [SerializeField]
    private LevelWordsSO levelWordsSO;
    [SerializeField]
    private TMP_Text phrase;

    private void Awake()
    {
        ClosePhrase();
    }
    // Start is called before the first frame update
    void Start()
    {
    }
    public void ChangePhrase(int phraseIndex)
    {
        ClosePhrase();
        phrase.text = levelWordsSO.phrasesCompass[phraseIndex].phrase;
    }
    private void ClosePhrase()
    {
        phrase.text = "";
    }
    private void OnDisable()
    {
        GameManager.OnEndOfCompass -= ChangePhrase;
    }
    private void OnEnable()
    {
        GameManager.OnEndOfCompass += ChangePhrase;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
