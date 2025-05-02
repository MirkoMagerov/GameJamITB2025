using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyErrorDetection : MonoBehaviour
{

    public bool givesError = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        givesError = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        givesError = true;
    }

}
