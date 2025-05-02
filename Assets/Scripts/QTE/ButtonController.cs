using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    public GameObject rippleEffect;

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
                ChangeEffectMaterial(effectMiss);
                NoteScore.instance.score -= 3;
            }

            laudString.GetComponent<Animator>().Play("Cuerda");
            rippleEffect.GetComponent<ParticleSystem>().Play();
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
