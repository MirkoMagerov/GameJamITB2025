using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessScale : MonoBehaviour
{

    public float finalScale = 2.5f;

    private void Start()
    {
        // Start the scaling coroutine
        StartCoroutine(ScalePrincess());
    }

    private IEnumerator ScalePrincess()
    {

        while (transform.localScale.x < finalScale)
        {
            // Scale the princess up
            Vector3 newScale = transform.localScale;
            newScale.x += Time.deltaTime * 0.5f;
            newScale.y += Time.deltaTime * 0.5f;
            transform.localScale = newScale;
            yield return null; // Wait for the next frame
        }

    }
}   
