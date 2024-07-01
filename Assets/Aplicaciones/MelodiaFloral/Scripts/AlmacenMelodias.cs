using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AlteracionMelodia
{

    public class AlmacenMelodias : MonoBehaviour
    {
        public struct Melodia
        {
            //public List<float> posNotas;
            public List<float> valNotas;
            public List<int> tipoNotas;
        }

        [SerializeField]
        private GameObject btnBibliotecaMelodias;
        [SerializeField] private GameObject menu;
        [SerializeField] private Button btnCloseMenu;
        public GameObject[] notas; //las notas f�sicas del pentagrama

        private List<Melodia> melodiasDisponibles;

        //private bool menuActivado;
        //private bool ultimoToqueSobreMenu = false; // necesitamos saber si en el toque anterior hemos pulsado el men� o no

        private Sprite spriteBTNPressed;
        private Sprite spriteBTNNormal;

        private void Start()
        {
            //menuActivado = false;
            melodiasDisponibles = new List<Melodia>();

            spriteBTNPressed = btnBibliotecaMelodias.GetComponent<Button>().spriteState.pressedSprite;
            spriteBTNNormal = btnBibliotecaMelodias.GetComponent<Image>().sprite;
            btnCloseMenu.onClick.AddListener(desactivarMenu);

            inicializarMelodias();
        }

        private void OnDestroy()
        {
            btnCloseMenu.onClick.RemoveListener(desactivarMenu);
        }

        /*private void Update()
        {
            if (menuActivado)
            {
                ClickFueraMenu();
            }
        }*/

        public void activarMenu()
        {
            menu.SetActive(true);
            //menuActivado = true;
            btnBibliotecaMelodias.GetComponent<Image>().sprite = spriteBTNPressed;

        }

        public void desactivarMenu()
        {
            menu.SetActive(false);
           // menuActivado = false;
            btnBibliotecaMelodias.GetComponent<Image>().sprite = spriteBTNNormal;
        }


        public void ponerMelodia(int index)
        {

            Melodia melodiaActual = new Melodia();
            // melodiaActual.posNotas = new List<float>();
            melodiaActual.valNotas = new List<float>();
            melodiaActual.tipoNotas = new List<int>();

            melodiaActual = melodiasDisponibles[index];
            //Vector3 newPos = new Vector3(0, 0, 0);

            for (int i = 0; i < melodiaActual.valNotas.Count; i++)
            {
                //cambiamos la posici�n de la nota (en este caso s�lo se mueve la Y)
                /* newPos = notas[i].GetComponent<Nota>().transform.localPosition;
                 newPos.y = melodiaActual.posNotas[i];
                 notas[i].GetComponent<Nota>().transform.localPosition = newPos;*/
                notas[i].GetComponent<Nota>().cambiarValorSlider(melodiaActual.valNotas[i]);
            }

            for (int i = 0; i < melodiaActual.tipoNotas.Count; i++)
            {
                notas[i].GetComponent<Nota>().establecerTipoNota(melodiaActual.tipoNotas[i]);
            }


        }

        private void inicializarMelodias()
        {
            //HABR� QUE AUTOMATIZARLO PARA PODER A�ADIR Y GUARDAR

            Melodia felizCumplea�os = new Melodia();
            felizCumplea�os.valNotas = new List<float>();
            felizCumplea�os.tipoNotas = new List<int>();
            Melodia juegoDeTronos = new Melodia();
            juegoDeTronos.valNotas = new List<float>();
            juegoDeTronos.tipoNotas = new List<int>();

            //cada tupla por cada nota de la melodia
            felizCumplea�os.valNotas.Add(ValorNotasSlider.DO4);
            felizCumplea�os.tipoNotas.Add(TiposNotas.CORCHEA);
            felizCumplea�os.valNotas.Add(ValorNotasSlider.DO4);
            felizCumplea�os.tipoNotas.Add(TiposNotas.CORCHEA);
            felizCumplea�os.valNotas.Add(ValorNotasSlider.RE4);
            felizCumplea�os.tipoNotas.Add(TiposNotas.NEGRA);
            felizCumplea�os.valNotas.Add(ValorNotasSlider.DO4);
            felizCumplea�os.tipoNotas.Add(TiposNotas.NEGRA);
            felizCumplea�os.valNotas.Add(ValorNotasSlider.FA4);
            felizCumplea�os.tipoNotas.Add(TiposNotas.NEGRA);
            felizCumplea�os.valNotas.Add(ValorNotasSlider.MI4);
            felizCumplea�os.tipoNotas.Add(TiposNotas.BLANCA);
            felizCumplea�os.valNotas.Add(ValorNotasSlider.DO4);
            felizCumplea�os.tipoNotas.Add(TiposNotas.NEGRA);
            felizCumplea�os.valNotas.Add(ValorNotasSlider.DO4);
            felizCumplea�os.tipoNotas.Add(TiposNotas.NEGRA);
            felizCumplea�os.valNotas.Add(ValorNotasSlider.DO4);
            felizCumplea�os.tipoNotas.Add(TiposNotas.NEGRA);
            felizCumplea�os.valNotas.Add(ValorNotasSlider.DO4);
            felizCumplea�os.tipoNotas.Add(TiposNotas.NEGRA);

            juegoDeTronos.valNotas.Add(ValorNotasSlider.LA4);
            juegoDeTronos.tipoNotas.Add(TiposNotas.NEGRA);
            juegoDeTronos.valNotas.Add(ValorNotasSlider.RE4);
            juegoDeTronos.tipoNotas.Add(TiposNotas.NEGRA);
            juegoDeTronos.valNotas.Add(ValorNotasSlider.FA4);
            juegoDeTronos.tipoNotas.Add(TiposNotas.CORCHEA);
            juegoDeTronos.valNotas.Add(ValorNotasSlider.SOL4);
            juegoDeTronos.tipoNotas.Add(TiposNotas.CORCHEA);
            juegoDeTronos.valNotas.Add(ValorNotasSlider.LA4);
            juegoDeTronos.tipoNotas.Add(TiposNotas.NEGRA);
            juegoDeTronos.valNotas.Add(ValorNotasSlider.RE4);
            juegoDeTronos.tipoNotas.Add(TiposNotas.NEGRA);
            juegoDeTronos.valNotas.Add(ValorNotasSlider.FA4);
            juegoDeTronos.tipoNotas.Add(TiposNotas.CORCHEA);
            juegoDeTronos.valNotas.Add(ValorNotasSlider.SOL4);
            juegoDeTronos.tipoNotas.Add(TiposNotas.CORCHEA);
            juegoDeTronos.valNotas.Add(ValorNotasSlider.LA4);
            juegoDeTronos.tipoNotas.Add(TiposNotas.NEGRA);
            juegoDeTronos.valNotas.Add(ValorNotasSlider.RE4);
            juegoDeTronos.tipoNotas.Add(TiposNotas.NEGRA);


            melodiasDisponibles.Add(felizCumplea�os);
            melodiasDisponibles.Add(juegoDeTronos);

        }

        /*public void ClickFueraMenu()
        {

            //Implementado esta manera, saldr�amos del men� al hacer dobletap fuera del men�
            if (Input.touchCount > 0 && menu.activeSelf)
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
        }*/
    }

}
