using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public BackgroundGuitarEffects guitarEffect;

    public GameObject rippleEffect;

    public GameObject notesEffectOne;
    public GameObject notesEffectTwo;

    public GameObject laudString;

    public Material effectPerfect;
    public Material effectOk;
    public Material effectMiss;

    private SpriteRenderer theSR;
    public Sprite defaultImage;
    public Sprite pressedImage;

    public KeyCode keyToPress;

    private void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            theSR.sprite = pressedImage;

            if (GetComponentInChildren<KeyErrorDetection>().givesError)
            {
                AudioManager myObject = GameObject.Find("MissSFX").GetComponent<AudioManager>();
                myObject.PlayRandomPitch();

                ChangeEffectMaterial(effectMiss);
                guitarEffect.ChangeColor(0);
                guitarEffect.GetComponent<Animator>().Play("GuitarBackground");

                if (GameObject.Find("Square" + keyToPress.ToString()) != null)
                {
                    ButtonEffect buttonEffect = GameObject.Find("Square" + keyToPress.ToString()).GetComponent<ButtonEffect>();
                    if (buttonEffect != null)
                    {
                        buttonEffect.ChangeColor(0);
                        buttonEffect.GetComponent<Animator>().Play("SquareOpacity");
                    }
                }

                ScoreManager.Instance.ApplyScore(3, !GameManager.Instance.leftPlayerPoem);
            }

            laudString.GetComponent<Animator>().Play("Cuerda");
            rippleEffect.GetComponent<ParticleSystem>().Play();
            notesEffectOne.GetComponent<ParticleSystem>().Play();
            notesEffectTwo.GetComponent<ParticleSystem>().Play();
        }

        if (Input.GetKeyUp(keyToPress))
        {
            theSR.sprite = defaultImage;
        }
    }

    public void ChangeEffectMaterial(Material material)
    {
        rippleEffect.GetComponent<ParticleSystem>().GetComponent<Renderer>().material = material;
    }

}
