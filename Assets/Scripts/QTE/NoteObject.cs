using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{

    public ButtonController buttonController;

    public float beatTempo;

    public bool canBePressed;

    public int currentScore = 0;

    public KeyCode keyToPress;

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
        if (collision.tag == "OkScore")
        {
            canBePressed = false;
            currentScore = 0;
        }
    }


}
