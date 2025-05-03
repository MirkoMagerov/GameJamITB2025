using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{

    public ButtonController buttonController;

    public float beatTempo;

    public float fadeInDuration = 2f;

    public bool canBePressed;

    public int currentScore = 0;

    public KeyCode keyToPress;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float fadeDuration = fadeInDuration;
        float elapsedTime = 0f;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        Color startColor = spriteRenderer.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }

    private void Update()
    {

        transform.position -= new Vector3(0f, 5 / beatTempo * Time.deltaTime, 0f);

        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                NoteScore.instance.score += currentScore;
                BackgroundGuitarEffects.Instance.GetComponent<Animator>().Play("GuitarBackground");
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PerfectScore")
        {
            canBePressed = true;
            buttonController.ChangeEffectMaterial(buttonController.effectPerfect);
            BackgroundGuitarEffects.Instance.ChangeColor(2);
            currentScore = 5;
        }
        else if (collision.tag == "OkScore")
        {
            Debug.Log("Ok score");
            canBePressed = true;
            buttonController.ChangeEffectMaterial(buttonController.effectOk);
            BackgroundGuitarEffects.Instance.ChangeColor(1);
            currentScore = 2;
        }
        
        if (collision.tag == "MissScore")
        {
            NoteScore.instance.score -= 5;
            BackgroundGuitarEffects.Instance.ChangeColor(0);
            BackgroundGuitarEffects.Instance.GetComponent<Animator>().Play("GuitarBackground");
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ButtonCollider")
        {
            Debug.Log("Missed");
            canBePressed = false;
            currentScore = 0;
        }
    }


}
