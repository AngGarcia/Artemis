using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pasarPagina : MonoBehaviour
{
    public GameObject pagAnt;
    public GameObject pagPost;

    public void BotonPasarPag()
    {
        pagAnt.SetActive(false);
        pagPost.SetActive(true);

    }
}
