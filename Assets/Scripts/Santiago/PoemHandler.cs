using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PoemHandler : MonoBehaviour
{
    [SerializeField]
    private LevelWordsSO levelWordsSO;
    public int compasIndex;
    [SerializeField]
    private List<TMP_Text> phrases = new List<TMP_Text>();

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < levelWordsSO.phrasesLevel1.Length; i++)
        {
            phrases[i].text = levelWordsSO.phrasesLevel1[i].phrase;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
