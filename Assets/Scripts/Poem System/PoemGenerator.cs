using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PoetryGenerator : MonoBehaviour
{
    public static PoetryGenerator Instance;

    public TextMeshProUGUI poemText;
    public LevelWordsSO specialWords;

    public TextMeshProUGUI optionIText;
    public TextMeshProUGUI optionOText;
    public TextMeshProUGUI optionPText;

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
    private int currentPhraseIndex = 0;
    private Coroutine pauseCoroutine;
    private bool optionSelected;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void CompleteCurrentLine()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        isTyping = false;
        typingCoroutine = null;

        poemText.text += currentLine;
    }

    public void StartPoem()
    {
        isDisplaying = true;
        poemText.text = "";
        poemLines.Clear();
        allSpecialWords.Clear();

        foreach (Phrase phrase in specialWords.phrasesLevel)
        {
            poemLines.Enqueue(phrase.phrase);
        }

        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        // Verificar si ya no quedan frases
        if (currentPhraseIndex >= specialWords.phrasesLevel.Length)
        {
            EndPoem();
            return;
        }

        currentPhrase = specialWords.phrasesLevel[currentPhraseIndex];
        currentPhraseIndex++;

        currentLine = currentPhrase.phrase;

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeLine(currentLine));
    }



    public IEnumerator TypeLine(string line)
    {
        isTyping = true;

        // Conservar el texto anterior
        string previousText = poemText.text;
        string currentLineText = "";

        // Mostrar la línea letra por letra
        for (int i = 0; i < line.Length; i++)
        {
            currentLineText += line[i];
            poemText.text = previousText + currentLineText;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        typingCoroutine = null;

        // Mostrar las opciones de palabras
        DisplayWordOptions(currentPhrase.words);

        // Esperar la elección del jugador
        pauseCoroutine = StartCoroutine(WaitForPlayerChoice(currentPhrase.words));
    }

    private IEnumerator WaitForPlayerChoice(Word[] words)
    {
        optionSelected = false;

        float elapsed = 0f;
        while (elapsed < pauseDuration)
        {
            if (Input.GetKeyDown(KeyCode.I) && words.Length > 0)
            {
                Debug.Log("Seleccionaste: " + words[0].word + " (" + words[0].points + " puntos)");
                optionSelected = true;
                break;
            }
            else if (Input.GetKeyDown(KeyCode.O) && words.Length > 1)
            {
                Debug.Log("Seleccionaste: " + words[1].word + " (" + words[1].points + " puntos)");
                optionSelected = true;
                break;
            }
            else if (Input.GetKeyDown(KeyCode.P) && words.Length > 2)
            {
                Debug.Log("Seleccionaste: " + words[2].word + " (" + words[2].points + " puntos)");
                optionSelected = true;
                break;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Limpiar opciones
        optionIText.text = "";
        optionOText.text = "";
        optionPText.text = "";

        // Saltar línea
        poemText.text += "\n";
        DisplayNextLine();
    }

    private void DisplayWordOptions(Word[] words)
    {
        optionIText.text = words.Length > 0 ? words[0].word : "";
        optionOText.text = words.Length > 1 ? words[1].word : "";
        optionPText.text = words.Length >= 2 ? words[2].word : "";
    }


    public void EndPoem()
    {
        isDisplaying = false;
        // Puedes añadir alguna animación o evento para cuando termine el poema
    }
}