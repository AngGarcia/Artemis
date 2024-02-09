using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AlteracionMelodia{
    public class BarraReproductora : MonoBehaviour
{
    // Start is called before the first frame update
    private bool moviendose = false;
    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (moviendose)
        {
            transform.Translate(Vector3.right * 4.0f * Time.deltaTime);
        }
        else
        {
            transform.position = posicionInicial;
        }
    }

    public void empezar()
    {
        moviendose = true;
    }

    public void detener()
    {
        moviendose = false;
    }

    //se va a mover cuando pulsemos un botón


    //va a activar el sonido de las notas cuando pase
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Nota"))
        {
            collision.GetComponent<Nota>().tocarNota();

        }


    }
}
}