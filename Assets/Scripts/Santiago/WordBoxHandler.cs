using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordBoxHandler : MonoBehaviour
{
    public int IdWordSlot;
    public int points;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndOfCompass()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<WordHandler>())
            {
                WordHandler wordHandler = child.GetComponent<WordHandler>();
                if (wordHandler.Word.idWordSlot == IdWordSlot)
                {
                    points += wordHandler.Word.points;
                    //Destroy(child.gameObject);
                }
            }
        }
    }
}
