using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PoemHandler : MonoBehaviour
{
    [SerializeField]
    private LevelWordsSO levelWordsSO;
    [SerializeField]
    private List<TMP_Text> compasses = new List<TMP_Text>();
    private string actualPhrase = "";

    private void Awake()
    {
        CloseCompasses();
    }
    // Start is called before the first frame update
    void Start()
    {
    }
    public void ChangeCompas(int compasIndex)
    {
        CloseCompasses();
        compasses[compasIndex].gameObject.SetActive(true);
        compasses[compasIndex].text = "";
        for (int i = 0; i < levelWordsSO.phrasesLevel.Length; i++)
        {
            compasses[compasIndex].text += levelWordsSO.phrasesLevel[i].phrase + "\n";
        }
    }

    private void CloseCompasses()
    {
        foreach (TMP_Text text in compasses)
        {
            text.text = "";
            text.gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        GameManager.OnEndOfCompass -= ChangeCompas;
    }
    private void OnEnable()
    {
        GameManager.OnEndOfCompass += ChangeCompas;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
