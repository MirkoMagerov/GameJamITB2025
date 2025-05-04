using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordBoxHandler : MonoBehaviour
{
    public int IdWordSlot;
    public int points;
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

    public void PhraseComplete()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<WordHandler>())
            {
                WordHandler wordHandler = child.GetComponent<WordHandler>();
                if (wordHandler.Word.idWordSlot == IdWordSlot)
                {
                    if (GameManager.Instance.leftPlayerPoem)
                    {
                    }
                    else
                    {
                    }
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Word"))
        {
            WordHandler wordHandler = collision.gameObject.GetComponent<WordHandler>();
            if (wordHandler.Word.idWordSlot == IdWordSlot)
            {
                if (GameManager.Instance.leftPlayerPoem)
                {
                }
                else
                {
                }
            }
        }
    }
}
