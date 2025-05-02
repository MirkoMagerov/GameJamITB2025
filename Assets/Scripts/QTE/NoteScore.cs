using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScore : MonoBehaviour
{


    public static NoteScore instance;

    public int score = 50;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
