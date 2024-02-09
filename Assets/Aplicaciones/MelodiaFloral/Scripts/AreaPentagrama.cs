using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlteracionMelodia
{
    public class AreaPentagrama : MonoBehaviour
    {

        public int numNota; //la usaremos para indicar qu� nota tiene que sonar en qu� �rea del pentagrama
        public int tipoPalo;  //la usaremos para indicar a la nota si tiene que dibujar los palitos del pentagram y qu� palitos

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Nota"))
            {
                //Debug.Log("Entr� en el �rea " + numNota);
                collision.GetComponent<Nota>().cambiarNota(numNota);
                collision.GetComponent<Nota>().agregarPalitoPentagrama(tipoPalo);
            }
        }
    }
}
