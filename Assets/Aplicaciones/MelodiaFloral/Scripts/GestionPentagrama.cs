using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlteracionMelodia
{
    public class GestionPentagrama : MonoBehaviour
    {
        //desde aquí gestionaremos la selección de la nota a modificar y el que no se puedan cambiar varias al mismo tiempo
        public GameObject[] notas;
        [SerializeField] private float segundosEntreNotas;

        private GameObject notaSeleccionada; //indicamos cuál es la nota que está siendo modificada en un momento dado
        private bool melodiaEnCurso;  //nos indica si la melodía está en reproducción
        void Start()
        {
            melodiaEnCurso = false;
            restablecerSeleccion();
        }

        //función que llamamos desde la nota pasada para que el pentagrama bloquee el resto de notas
        public void establecerNotaActual(GameObject notaActual)
        {

            //si al establecer una nueva nota para modificar ya había otra antes, finalizamos su modificación
            if (notaSeleccionada != null)
            {
                notaSeleccionada.GetComponent<Nota>().finalizarModificacion();
            }

            notaSeleccionada = notaActual;

            for (int i = 0; i < notas.Length; i++)
            {
                if (notas[i] == notaActual) //si la nota es la misma que hemos pulsado
                {
                    //notas[i].GetComponent<Nota>().desbloquearNota();
                    notas[i].GetComponent<Nota>().setPuedeModificarse(true);
                }
                else
                {
                    //notas[i].GetComponent<Nota>().bloquearNota();
                    notas[i].GetComponent<Nota>().setPuedeModificarse(false);
                }

            }

        }

        //para poder seleccionar una nueva nota, tenemos que desbloquear todas
        public void restablecerSeleccion()
        {
            for (int i = 0; i < notas.Length; i++)
            {
                notas[i].GetComponent<Nota>().desbloquearNota();
                notas[i].GetComponent<Nota>().setPuedeModificarse(false);
            }
        }

        //función la cual tocará la melodía entera, detectando cuándo ha acabado el audio de la nota
        //Una redonda son 4 negras, una blanca son 2 negras, y una corchea es la mitad de una negra
        public void escucharMelodia()
        {

            if (melodiaEnCurso) //si la melodía está en curso, la paramos y volvemos a ponerla desde el principio
            {
                pausarMelodia();
                StartCoroutine(tocarMelodia());
            }
            else
            {
                StartCoroutine(tocarMelodia());
            }

        }

        public void pausarMelodia()
        {
            StopAllCoroutines();

            //devolvemos las notas a su tamaño normal
            for (int i = 0; i < notas.Length; i++)
            {
                notas[i].GetComponent<Nota>().cambiarTallaNormal();
            }

        }

        //Tal y como tengo el estado del juego ahora, simplemente estoy aumentando los tiempos de espera entre las notas, pero no se mantiene el sonido
        //El sonido depende del audio como tal, yo no puedo mantener ese audio
        public IEnumerator tocarMelodia()
        {
            melodiaEnCurso = true;  //ha empezado la corrutina
            int tipoNota;

            for (int i = 0; i < notas.Length; i++)
            {
                notas[i].GetComponent<Nota>().cambiarTallaGrande();
                notas[i].GetComponent<Nota>().tocarNota();
                //vemos qué tipo de nota es y vemos cuanto tiempo hay entre nota y nota
                tipoNota = notas[i].GetComponent<Nota>().getTipoNota();

                switch (tipoNota)
                {
                    case 0: //silencio
                        yield return new WaitForSeconds(VelocidadNotas.SILENCIO);
                        break;
                    case 1: //redonda
                        yield return new WaitForSeconds(VelocidadNotas.REDONDA);
                        break;
                    case 2: //blanca
                        yield return new WaitForSeconds(VelocidadNotas.BLANCA);
                        break;
                    case 3: //negra
                        yield return new WaitForSeconds(VelocidadNotas.NEGRA);
                        break;
                    case 4: //corchea
                        yield return new WaitForSeconds(VelocidadNotas.CORCHEA);
                        break;

                }
                //notas[i].GetComponent<Nota>().tocarNota();
                //yield return new WaitForSeconds(segundosEntreNotas);
                notas[i].GetComponent<Nota>().cambiarTallaNormal();
            }

            melodiaEnCurso = false; //ha acabado la corrutina
        }

    }
}
