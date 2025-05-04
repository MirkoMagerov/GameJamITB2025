using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PoetryGenerator : MonoBehaviour
{
    //public static PoetryGenerator Instance;

    [SerializeField] private Timer timerSlider;

    public TextMeshProUGUI poemText;
    public LevelWordsSO[] specialWordsList;
    public LevelWordsSO specialWords;

    public TextMeshProUGUI wordSlot1;
    public TextMeshProUGUI wordSlot2;
    public TextMeshProUGUI wordSlot3;

    public bool isDisplaying = false;
    public float typingSpeed = 0.1f;
    public float pauseDuration = 1.0f;

    private Queue<string> poemLines = new();
    private bool isTyping = false;
    private Coroutine typingCoroutine = null;
    private string currentLine;

    // Lista para mantener un seguimiento de todas las palabras especiales
    private List<Word> allSpecialWords = new List<Word>();

    private Phrase currentPhrase;
    private Verse currentVerse;
    private int currentVerseIndex = 0;
    private int currentPhraseIndex = 0;
    private Coroutine pauseCoroutine;
    private bool optionSelected;

    void OnEnable()
    {
        StartPoem();
    }

    void OnDisable()
    {
        timerSlider.gameObject.SetActive(false);
    }

    public void StartPoem()
    {

        int randomIndex = Random.Range(0, specialWordsList.Length);

        specialWords = specialWordsList[randomIndex];

        StartCoroutine(StartPoemCoroutine());
    }

    private IEnumerator StartPoemCoroutine()
    {
        isDisplaying = true;
        poemText.text = "";
        poemLines.Clear();
        allSpecialWords.Clear();

        currentVerseIndex = 0;
        currentPhraseIndex = 0;
        currentVerse = specialWords.verses[currentVerseIndex];

        foreach (Phrase phrase in currentVerse.phrases)
        {
            poemLines.Enqueue(phrase.phrase);
        }

        yield return new WaitForSeconds(1.5f);

        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (currentPhraseIndex >= currentVerse.phrases.Length)
        {
            currentVerseIndex++;
            if (currentVerseIndex >= specialWords.verses.Length)
            {
                EndPoem();
                return;
            }

            poemText.text = "";

            currentVerse = specialWords.verses[currentVerseIndex];
            currentPhraseIndex = 0;
        }


        currentPhrase = currentVerse.phrases[currentPhraseIndex];
        currentPhraseIndex++;

        currentLine = currentPhrase.phrase;

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeLine(currentLine, currentPhrase.words));
    }


    public IEnumerator TypeLine(string line, Word[] wordOptions)
    {
        isTyping = true;
        string previousText = poemText.text;
        string beforePlaceholder = "";
        string afterPlaceholder = "";
        int insertIndex = line.IndexOf('[');

        if (insertIndex == -1)
        {
            // Frase sin palabra seleccionable: mostrar toda la línea de forma normal
            string currentLineTextWithoutOption = "";
            for (int i = 0; i < line.Length; i++)
            {
                currentLineTextWithoutOption += line[i];
                poemText.text = previousText + currentLineTextWithoutOption;
                yield return new WaitForSeconds(typingSpeed);
            }

            poemText.text += "\n";
            isTyping = false;
            typingCoroutine = null;
            yield return new WaitForSeconds(0.5f);
            DisplayNextLine();
            yield break;
        }

        // Separar la línea antes y después del marcador
        beforePlaceholder = line[..insertIndex];
        afterPlaceholder = line[(insertIndex + 2)..]; // Excluir el marcador

        string currentLineText = "";
        for (int i = 0; i < beforePlaceholder.Length; i++)
        {
            currentLineText += beforePlaceholder[i];
            poemText.text = previousText + currentLineText;
            yield return new WaitForSeconds(typingSpeed);
        }

        timerSlider.gameObject.SetActive(true);
        timerSlider.SetupTimer(pauseDuration);
        ShowWordOptions(wordOptions);

        Word chosenWord = null;
        float timer = 0f;
        bool selected = false;

        void OnKeyInput(KeyCode key, int index)
        {
            if (!selected && Input.GetKeyDown(key))
            {
                timerSlider.gameObject.SetActive(false);
                chosenWord = wordOptions[index];
                ScoreManager.Instance.ApplyScore(chosenWord.points, GameManager.Instance.leftPlayerPoem);
                selected = true;
                Debug.Log("Palabra seleccionada: " + chosenWord.word + " - Puntos: " + chosenWord.points);
            }
        }

        while (timer < pauseDuration && !selected)
        {
            KeyCode[] keys;

            if (GameManager.Instance.leftPlayerPoem)
            {
                keys = new[] { KeyCode.Q, KeyCode.W, KeyCode.E }; // jugador izquierdo
            }
            else
            {
                keys = new[] { KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.RightArrow }; // jugador derecho
            }

            OnKeyInput(keys[0], 0);
            OnKeyInput(keys[1], 1);
            OnKeyInput(keys[2], 2);

            timer += Time.deltaTime;
            yield return null;
        }

        if (!selected)
        {
            timerSlider.gameObject.SetActive(false);
            List<Word> negativeWords = new List<Word>();
            foreach (var w in wordOptions)
            {
                if (w.points < 0) negativeWords.Add(w);
            }

            if (negativeWords.Count > 0)
                chosenWord = negativeWords[Random.Range(0, negativeWords.Count)];
            else
                chosenWord = wordOptions[Random.Range(0, wordOptions.Length)];
            ScoreManager.Instance.ApplyScore(chosenWord.points, GameManager.Instance.leftPlayerPoem);
        }

        HideWordOptions();

        for (int i = 0; i < chosenWord.word.Length; i++)
        {
            currentLineText += chosenWord.word[i];
            poemText.text = previousText + currentLineText;
            yield return new WaitForSeconds(typingSpeed);
        }

        for (int i = 0; i < afterPlaceholder.Length; i++)
        {
            currentLineText += afterPlaceholder[i];
            poemText.text = previousText + currentLineText;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        typingCoroutine = null;

        poemText.text += "\n";
        yield return new WaitForSeconds(0.5f);

        DisplayNextLine();
    }

    public void EndPoem()
    {
        Debug.Log("Fin del poema");
        isDisplaying = false;
        GameManager.Instance.ChangeOfTurn();
    }

    private void ShowWordOptions(Word[] options)
    {
        wordSlot1.text = options[0].word;
        wordSlot2.text = options[1].word;
        wordSlot3.text = options[2].word;
    }

    private void HideWordOptions()
    {
        wordSlot1.text = "";
        wordSlot2.text = "";
        wordSlot3.text = "";
    }
}