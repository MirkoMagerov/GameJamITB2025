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
        transform.position -= new Vector3(0f, beatTempo * 8 * Time.deltaTime, 0f);

        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                ScoreManager.Instance.ApplyQTEResult(currentScore, GameManager.Instance.leftPlayerPoem);
                BackgroundGuitarEffects.Instance.GetComponent<Animator>().Play("GuitarBackground");

                if (currentScore == 5)
                {
                    AudioManager myObject = GameObject.Find("PerfectSFX").GetComponent<AudioManager>();
                    myObject.PlaySound();
                    buttonController.ChangeEffectMaterial(buttonController.effectPerfect);
                    BackgroundGuitarEffects.Instance.ChangeColor(2);
                }
                else if (currentScore == 2)
                {
                    AudioManager myObject = GameObject.Find("OkSFX").GetComponent<AudioManager>();
                    myObject.PlaySound();
                    buttonController.ChangeEffectMaterial(buttonController.effectOk);
                    BackgroundGuitarEffects.Instance.ChangeColor(1);
                }

                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PerfectScore")
        {
            canBePressed = true;
            currentScore = 5;
        }
        else if (collision.tag == "OkScore")
        {
            canBePressed = true;
            currentScore = 2;
        }

        if (collision.tag == "MissScore")
        {
            ScoreManager.Instance.ApplyQTEResult(-5, GameManager.Instance.leftPlayerPoem);
            BackgroundGuitarEffects.Instance.ChangeColor(0);
            BackgroundGuitarEffects.Instance.GetComponent<Animator>().Play("GuitarBackground");

            AudioManager myObject = GameObject.Find("MissSFX").GetComponent<AudioManager>();
            myObject.PlayRandomPitch();

            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ButtonCollider")
        {
            canBePressed = false;
            currentScore = 0;
        }
    }


}
