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
        public GameObject[] notas; //las notas físicas del pentagrama

        private List<Melodia> melodiasDisponibles;

        //private bool menuActivado;
        //private bool ultimoToqueSobreMenu = false; // necesitamos saber si en el toque anterior hemos pulsado el menú o no

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
                //cambiamos la posición de la nota (en este caso sólo se mueve la Y)
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
            //HABRÁ QUE AUTOMATIZARLO PARA PODER AÑADIR Y GUARDAR

            Melodia felizCumpleaños = new Melodia();
            felizCumpleaños.valNotas = new List<float>();
            felizCumpleaños.tipoNotas = new List<int>();
            Melodia juegoDeTronos = new Melodia();
            juegoDeTronos.valNotas = new List<float>();
            juegoDeTronos.tipoNotas = new List<int>();

            //cada tupla por cada nota de la melodia
            felizCumpleaños.valNotas.Add(ValorNotasSlider.DO4);
            felizCumpleaños.tipoNotas.Add(TiposNotas.CORCHEA);
            felizCumpleaños.valNotas.Add(ValorNotasSlider.DO4);
            felizCumpleaños.tipoNotas.Add(TiposNotas.CORCHEA);
            felizCumpleaños.valNotas.Add(ValorNotasSlider.RE4);
            felizCumpleaños.tipoNotas.Add(TiposNotas.NEGRA);
            felizCumpleaños.valNotas.Add(ValorNotasSlider.DO4);
            felizCumpleaños.tipoNotas.Add(TiposNotas.NEGRA);
            felizCumpleaños.valNotas.Add(ValorNotasSlider.FA4);
            felizCumpleaños.tipoNotas.Add(TiposNotas.NEGRA);
            felizCumpleaños.valNotas.Add(ValorNotasSlider.MI4);
            felizCumpleaños.tipoNotas.Add(TiposNotas.BLANCA);
            felizCumpleaños.valNotas.Add(ValorNotasSlider.DO4);
            felizCumpleaños.tipoNotas.Add(TiposNotas.NEGRA);
            felizCumpleaños.valNotas.Add(ValorNotasSlider.DO4);
            felizCumpleaños.tipoNotas.Add(TiposNotas.NEGRA);
            felizCumpleaños.valNotas.Add(ValorNotasSlider.DO4);
            felizCumpleaños.tipoNotas.Add(TiposNotas.NEGRA);
            felizCumpleaños.valNotas.Add(ValorNotasSlider.DO4);
            felizCumpleaños.tipoNotas.Add(TiposNotas.NEGRA);

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


            melodiasDisponibles.Add(felizCumpleaños);
            melodiasDisponibles.Add(juegoDeTronos);

        }

        /*public void ClickFueraMenu()
        {

            //Implementado esta manera, saldríamos del menú al hacer dobletap fuera del menú
            if (Input.touchCount > 0 && menu.activeSelf)
            {
                Touch touch = Input.GetTouch(0); // el toque del dedo lo almacenamos

                bool toqueSobreUI = EventSystem.current.IsPointerOverGameObject(); //comprobamos si estamos tocando el menú

                //si el anterior toque fue sobre el menú pero el siguiente fue fuera, desactivamos el menú
                if (ultimoToqueSobreMenu && !toqueSobreUI)
                {
                    desactivarMenu();
                }

                //indicamos que el último toque se hizo sobre el menú (true)
                ultimoToqueSobreMenu = toqueSobreUI;
            }
        }*/
    }

}
