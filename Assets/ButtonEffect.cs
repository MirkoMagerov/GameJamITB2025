using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEffect : MonoBehaviour
{

    private SpriteRenderer theSR;

    [SerializeField] private Color colorPerfect;
    [SerializeField] private Color colorOk;
    [SerializeField] private Color colorMiss;

    private void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }


    public void ChangeColor(int color)
    {   

        if (color == 0)
        {
            Color newColor = new Color(colorMiss.r, colorMiss.g, colorMiss.b, theSR.color.a);
            theSR.color = newColor;
        }
        else if (color == 1)
        {
            Color newColor = new Color(colorOk.r, colorOk.g, colorOk.b, theSR.color.a);
            theSR.color = newColor;
        }
        else
        {
            Color newColor = new Color(colorPerfect.r, colorPerfect.g, colorPerfect.b, theSR.color.a);
            theSR.color = newColor;
        }

    }

}
