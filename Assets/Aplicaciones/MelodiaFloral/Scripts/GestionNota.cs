using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace AlteracionMelodia
{
    public class GestionNota : MonoBehaviour
    {
        [SerializeField]
        private GameObject menuTipoNota;
        [SerializeField]
        private GameObject menuInstrumento;
        [SerializeField]
        private GameObject[] botonesTiposNotas;
        [SerializeField]
        private GameObject[] botonesInstrumentos;

        private int tipo;
        private int instrumentoActual;
        private bool menusActivados;
        private GameObject notaActual; //necesitamos saber qu� nota concreta ha solicitado el men�
        private bool ultimoToqueSobreMenu = false; // necesitamos saber si en el toque anterior hemos pulsado el men� o no

        void Start()
        {
            menusActivados = false;
            tipo = 2;  // negra
            instrumentoActual = 0; //piano
        }

        private void Update()
        {
            if (menusActivados)
            {
                //si la nota NO est� siendo pulsada
                if (!notaActual.GetComponent<Nota>().getNotaMoviendose())
                {
                    ClickFueraMenu();
                }
            }
        }

        public bool getEstadoMenu()
        {
            return menusActivados;
        }

        public void activarMenus(GameObject nota)
        {
            notaActual = nota;
            menusActivados = true;
            menuTipoNota.SetActive(true);
            menuInstrumento.SetActive(true);

            //comprobamos en que tipo de nota est� y la resaltamos en el men�
            tipo = notaActual.GetComponent<Nota>().getTipoNota();
            instrumentoActual = notaActual.GetComponent<Nota>().getInstrumento();

            //pasamos por los botones para avisar de que es el tipo de nota actual de la nota que estamos modificando
            for (int i = 0; i < botonesTiposNotas.Length; i++)
            {
                if (i == tipo) //tenemos que guardar en orden los tipos de notas
                {
                    botonesTiposNotas[i].GetComponent<Image>().color = Color.red;
                }
                else
                {
                    botonesTiposNotas[i].GetComponent<Image>().color = Color.white;
                }

            }

            //hacemos lo mismo para los botones del men� de instrumentos
            for (int i = 0; i < botonesInstrumentos.Length; i++)
            {
                if (i == instrumentoActual) //tenemos que guardar en orden los tipos de notas
                {
                    botonesInstrumentos[i].GetComponent<Image>().color = Color.red;
                }
                else
                {
                    botonesInstrumentos[i].GetComponent<Image>().color = Color.white;
                }

            }
        }

        public void desactivarMenu()
        {
            menusActivados = false;
            menuTipoNota.SetActive(false);
            menuInstrumento.SetActive(false);
            //decirle a la nota que ya hemos acabado de editar
            notaActual.GetComponent<Nota>().finalizarModificacion();
        }

        public void getTipo(int num) //obtenemos el tipo actual seleccionado
        {
            tipo = num;

            //modificamos el aspecto los botones para establecer el tipo de nota actual
            for (int i = 0; i < botonesTiposNotas.Length; i++)
            {
                if (i == tipo) //tenemos que guardar en orden los tipos de notas
                {
                    botonesTiposNotas[i].GetComponent<Image>().color = Color.red;
                }
                else
                {
                    botonesTiposNotas[i].GetComponent<Image>().color = Color.white;
                }

            }

            //Debug.Log("Se cambi� el tipo de nota a " + tipo);
            //enviamos el tipo nuevo a la nota
            notaActual.GetComponent<Nota>().establecerTipoNota(tipo);

        }

        public void getIntrumento(int num)
        {
            instrumentoActual = num;

            //hacemos lo mismo para los botones de los instrumentos
            for (int i = 0; i < botonesInstrumentos.Length; i++)
            {
                if (i == instrumentoActual) //tenemos que guardar en orden los tipos de notas
                {
                    botonesInstrumentos[i].GetComponent<Image>().color = Color.red;
                }
                else
                {
                    botonesInstrumentos[i].GetComponent<Image>().color = Color.white;
                }

            }

            notaActual.GetComponent<Nota>().cambiarInstrumento(instrumentoActual);
        }

        public void ClickFueraMenu()
        {
            // si hacemos click fuera del men�, estando este activo
            /*if (Input.GetMouseButtonDown(0) && menu.activeSelf &&
            !EventSystem.current.IsPointerOverGameObject())
            {
                desactivarMenu();
            }*/

            //Implementado esta manera, saldr�amos del men� al hacer dobletap fuera del men�
            if ((Input.touchCount > 0 && menuTipoNota.activeSelf) || (Input.touchCount > 0 && menuInstrumento.activeSelf))
            {
                Touch touch = Input.GetTouch(0); // el toque del dedo lo almacenamos

                bool toqueSobreUI = EventSystem.current.IsPointerOverGameObject(); //comprobamos si estamos tocando el men�

                //si el anterior toque fue sobre el men� pero el siguiente fue fuera, desactivamos el men�
                if (ultimoToqueSobreMenu && !toqueSobreUI)
                {
                    desactivarMenu();
                }

                //indicamos que el �ltimo toque se hizo sobre el men� (true)
                ultimoToqueSobreMenu = toqueSobreUI;
            }
        }

    }
}
