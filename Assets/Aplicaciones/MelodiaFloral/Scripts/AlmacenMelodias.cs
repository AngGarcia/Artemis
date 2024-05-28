using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AlteracionMelodia
{

    public class AlmacenMelodias : MonoBehaviour
    {
        public struct Melodia
        {
            public List<float> posNotas;
            public List<int> tipoNotas;
        }

        [SerializeField]
        private GameObject menu; 
        public GameObject[] notas; //las notas f�sicas del pentagrama

        private List<Melodia> melodiasDisponibles;

        private bool menuActivado;
        private bool ultimoToqueSobreMenu = false; // necesitamos saber si en el toque anterior hemos pulsado el men� o no

        private void Start()
        {
            menuActivado = false;
            melodiasDisponibles = new List<Melodia>();

            inicializarMelodias();
        }

        private void Update()
        {
            if (menuActivado)
            {
                ClickFueraMenu();
            }
        }

        public void activarMenu()
        {
            menu.SetActive(true);
            menuActivado = true;

        }

        public void desactivarMenu()
        {
            menu.SetActive(false);
            menuActivado = false;
        }


        public void ponerMelodia(int index)
        {

            Melodia melodiaActual = new Melodia();
            melodiaActual.posNotas = new List<float>();
            melodiaActual.tipoNotas = new List<int>();

            melodiaActual = melodiasDisponibles[index];
            Vector3 newPos = new Vector3(0, 0, 0);

            for (int i = 0; i < melodiaActual.posNotas.Count; i++)
            {
                //cambiamos la posici�n de la nota (en este caso s�lo se mueve la Y)
                newPos = notas[i].GetComponent<Nota>().transform.localPosition;
                newPos.y = melodiaActual.posNotas[i];
                notas[i].GetComponent<Nota>().transform.localPosition = newPos;
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
            felizCumplea�os.posNotas = new List<float>();
            felizCumplea�os.tipoNotas = new List<int>();
            Melodia juegoDeTronos = new Melodia();
            juegoDeTronos.posNotas = new List<float>();
            juegoDeTronos.tipoNotas = new List<int>();

            //cada tupla por cada nota de la melodia
            felizCumplea�os.posNotas.Add(PosicionAxisYNotas.DO);
            felizCumplea�os.tipoNotas.Add(TiposNotas.CORCHEA);
            felizCumplea�os.posNotas.Add(PosicionAxisYNotas.DO);
            felizCumplea�os.tipoNotas.Add(TiposNotas.CORCHEA);
            felizCumplea�os.posNotas.Add(PosicionAxisYNotas.RE);
            felizCumplea�os.tipoNotas.Add(TiposNotas.NEGRA);
            felizCumplea�os.posNotas.Add(PosicionAxisYNotas.DO);
            felizCumplea�os.tipoNotas.Add(TiposNotas.NEGRA);
            felizCumplea�os.posNotas.Add(PosicionAxisYNotas.FA);
            felizCumplea�os.tipoNotas.Add(TiposNotas.NEGRA);
            felizCumplea�os.posNotas.Add(PosicionAxisYNotas.MI);
            felizCumplea�os.tipoNotas.Add(TiposNotas.BLANCA);
            felizCumplea�os.posNotas.Add(PosicionAxisYNotas.DO);
            felizCumplea�os.tipoNotas.Add(TiposNotas.NEGRA);
            felizCumplea�os.posNotas.Add(PosicionAxisYNotas.DO);
            felizCumplea�os.tipoNotas.Add(TiposNotas.NEGRA);
            felizCumplea�os.posNotas.Add(PosicionAxisYNotas.DO);
            felizCumplea�os.tipoNotas.Add(TiposNotas.NEGRA);
            felizCumplea�os.posNotas.Add(PosicionAxisYNotas.DO);
            felizCumplea�os.tipoNotas.Add(TiposNotas.NEGRA);

            juegoDeTronos.posNotas.Add(PosicionAxisYNotas.LA);
            juegoDeTronos.tipoNotas.Add(TiposNotas.NEGRA);
            juegoDeTronos.posNotas.Add(PosicionAxisYNotas.RE);
            juegoDeTronos.tipoNotas.Add(TiposNotas.NEGRA);
            juegoDeTronos.posNotas.Add(PosicionAxisYNotas.FA);
            juegoDeTronos.tipoNotas.Add(TiposNotas.CORCHEA);
            juegoDeTronos.posNotas.Add(PosicionAxisYNotas.SOL);
            juegoDeTronos.tipoNotas.Add(TiposNotas.CORCHEA);
            juegoDeTronos.posNotas.Add(PosicionAxisYNotas.LA);
            juegoDeTronos.tipoNotas.Add(TiposNotas.NEGRA);
            juegoDeTronos.posNotas.Add(PosicionAxisYNotas.RE);
            juegoDeTronos.tipoNotas.Add(TiposNotas.NEGRA);
            juegoDeTronos.posNotas.Add(PosicionAxisYNotas.FA);
            juegoDeTronos.tipoNotas.Add(TiposNotas.CORCHEA);
            juegoDeTronos.posNotas.Add(PosicionAxisYNotas.SOL);
            juegoDeTronos.tipoNotas.Add(TiposNotas.CORCHEA);
            juegoDeTronos.posNotas.Add(PosicionAxisYNotas.LA);
            juegoDeTronos.tipoNotas.Add(TiposNotas.NEGRA);
            juegoDeTronos.posNotas.Add(PosicionAxisYNotas.RE);
            juegoDeTronos.tipoNotas.Add(TiposNotas.NEGRA);


            melodiasDisponibles.Add(felizCumplea�os);
            melodiasDisponibles.Add(juegoDeTronos);

        }

        public void ClickFueraMenu()
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
        }
    }

}
