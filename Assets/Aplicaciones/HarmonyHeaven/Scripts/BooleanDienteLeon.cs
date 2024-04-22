using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BooleanDienteLeon : MonoBehaviour
{
    public bool dienteLeonCorrecto;

    private Image srCmp;
    public Sprite circle;
    public Sprite Cruz;

    private void Start()
    {
        srCmp = GetComponent<Image>();
    }
    public void ComprobarCorrecto()
    {
        if (dienteLeonCorrecto == true)
        {
            srCmp.sprite = circle;
        }
        else if (dienteLeonCorrecto == false)
        {
            srCmp.sprite = Cruz;

        }
    }
}
