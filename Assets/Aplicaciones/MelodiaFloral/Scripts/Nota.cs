using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AlteracionMelodia
{
    public class Nota : MonoBehaviour//, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public enum TiposNotasLocal
        {
            Silencio = 0,
            Redonda = 1,
            Blanca = 2,
            Negra = 3,
            Corchea = 4
        }

        public ListaNotasFMOD listaNotasFMOD;

        [Header("VARIABLES GENERALES")]
        public GestionNota gestorNota; //gestor del panel/menú de cada nota (es el mismo para todas)
        public GestionPentagrama gestionPentagrama; //gestor del pentagrama general, que dice qué notas estan activadas en cada momento
        [SerializeField] private Slider sliderValorNota;

        [SerializeField] private GameObject brillo;
        [SerializeField] private GameObject notaSonando;
        private Vector3 lastTouchPosition; //última posición del dedo

        private int sonidoActual;  //tendríamos que usar enumerados aquí
        private bool sePuedeMover; //booleano para indicar si se puede arrastrar la nota o no en ese momento
        private bool notaMoviendose;  //booleano para indicar que estamos moviendo/pulsando la nota
        private TiposNotasLocal tipoNotaActual;
        private int instrumentoActual;

        private bool sePuedeModificar;  //variable que activa las funciones de modificación de la nota
        private bool notaModificandose; //para el slider, ver si se ha tocado por primera vez

        // 25-10-2023 --> ya no usamos activamente esta variable para que el jugador pueda saltar a modificar una nota fácilmente
        private bool bloqueada;  //variable que bloquea la nota; si la nota está bloqueada, no podemos interactuar con ella de ninguna manera


        public GameObject[] tiposNotas; //tal y como lo tengo, para cambiar la apariencia de la nota, tengo varios hijos y cada uno es un tipo de nota diferente
                                        //lo hice así porque no se si se puede ajustar el sprite de la nota en tiempo real para que cuadre
        public GameObject[] palosPentagrama;  //al igual que con los tipos de notas, si la nota se dibuja fuera de los límites del pentagrama, hay que añadir
                                              // pequeñas líneas para indicar que hay más pentagrama por arriba y por abajo


        void Start()
        {
            notaModificandose = false;
            notaMoviendose = false;
            sePuedeModificar = false;
            bloqueada = false;
            sonidoActual = 0;
            sePuedeMover = false;
            tipoNotaActual = TiposNotasLocal.Negra;
            brillo.SetActive(false);
            notaSonando.SetActive(false);
            instrumentoActual = 0; //piano
            sliderValorNota.onValueChanged.AddListener(modificarNota);

            //el tipo de nota inicial es una negra
            for (int i = 0; i < tiposNotas.Length; i++)
            {
                if (tiposNotas[i].CompareTag("Negra"))
                {
                    tiposNotas[i].SetActive(true);
                }
                else
                {
                    tiposNotas[i].SetActive(false);
                }
            }

            for (int i = 0; i < palosPentagrama.Length; i++)
            {
                palosPentagrama[i].SetActive(false);
            }
        }


        private void Update()
        {
            /*if (!bloqueada)
            {
                if (notaMoviendose)
                {
                    if (!sePuedeModificar)
                    {
                        //le mando al gestor del pentagrama que me bloqueé el resto de notas;
                        // Esto es lo primero que va a suceder al intentar modificar la nota, el resto de funciones que permiten mover, tocar o modificar la nota dependen de esta
                        gestionPentagrama.establecerNotaActual(this.gameObject);
                        activarModificacion();
                    }
                    else if (sePuedeModificar && sePuedeMover)
                    {
                        /*if (Input.touchCount > 0)
                        {
                            Touch touch = Input.GetTouch(0); // cogemos el Input del dedo

                            if (touch.phase == TouchPhase.Moved) //si estamos moviendo el dedo
                            {
                                Vector3 touchDeltaPosition = new Vector3(touch.position.x, touch.position.y, 0) - lastTouchPosition;
                                Vector3 newPosition = transform.position;
                                newPosition.y += touchDeltaPosition.y * 0.01f; // Puedes ajustar el factor 0.01f según tus necesidades
                                transform.position = newPosition;
                            }

                            lastTouchPosition = touch.position;
                        }
                    }
                }
            }*/
        }
        //GET y SET de la variable sePuedeModificar
        public void setPuedeModificarse(bool modificacion)
        {
            sePuedeModificar = modificacion;
        }

        public bool getPuedeModiciarse()
        {
            return sePuedeModificar;
        }

        public bool getNotaMoviendose()
        {
            return notaMoviendose;
        }

        //funciones para bloquear y desbloquear la nota
        public void bloquearNota()
        {
            bloqueada = true;
        }
        public void desbloquearNota()
        {
            bloqueada = false;
        }

        public int getTipoNota()
        {
            return (int)tipoNotaActual;
        }

        public int getInstrumento()
        {
            return instrumentoActual;
        }

        //para poner la nota al establecer una melodía nueva a través de AlmacenMelodias
        public void cambiarValorSlider(float newVal)
        {
            sliderValorNota.value = newVal;
            cambiarNota((int)sliderValorNota.value);
        }

        //VERSION UI, se ejecuta cada vez que movemos el slider de la nota
        private void modificarNota(float value)
        {
            if (notaModificandose) //seguimos en la misma nota
            {
                cambiarNota((int)value);
            }
            else //entra si es la primera vez que lo tocamos (después de haber modificado otra nota)
            {
                gestionPentagrama.establecerNotaActual(this.gameObject);
                activarModificacion();
            }
        }

        //VERSION ANTIGUA, NO UI
        public void cambiarNota(int num)
        {
            if (tipoNotaActual == TiposNotasLocal.Silencio)
            {
                sonidoActual = 0; //el 0 equivale al silencio en el array de sonidos
            }
            else
            {
                sonidoActual = num;

                switch (num)
                {
                    case -1: //La3
                        agregarPalitoPentagrama(0);
                        break;
                    case 0: //Si3
                        agregarPalitoPentagrama(1);
                        break;
                    case 1: //Do4
                        agregarPalitoPentagrama(2);
                        break;
                    default: //notas dentro del pentagrama que no necesitan palo
                        agregarPalitoPentagrama(-1);
                        break;
                }
            }
            tocarNota();
        }

        public void cambiarInstrumento(int instrumento)
        {
            switch (instrumento)
            {
                case 0:
                    instrumentoActual = Intrumentos.PIANO;
                    break;
                case 1:
                    instrumentoActual = Intrumentos.FLAUTA;
                    break;
                case 2:
                    instrumentoActual = Intrumentos.CLARINETE;
                    break;
                case 3:
                    instrumentoActual = Intrumentos.OBOE;
                    break;
                case 4:
                    instrumentoActual = Intrumentos.VIOLIN;
                    break;
            }
        }

        public void tocarNota()
        {

            //Aquí, en vez de esa línea de código, pondríamos la del FMOD
            //primero comprobamos el instrumento y después la nota
            if(sonidoActual != -1)
            {
                listaNotasFMOD.tocarNotaFMOD(instrumentoActual, sonidoActual, (int)tipoNotaActual);
            }

        }

        public void agregarPalitoPentagrama(int tipoPalo)
        {
            //según el tipo, pondremos palito o no

            for (int i = 0; i < palosPentagrama.Length; i++)
            {
                if (i == tipoPalo) //está pensado para que el número enviado por el pentagrama sea el mismo que la posición del objeto en el array
                {
                    palosPentagrama[i].SetActive(true);
                }
                else
                {
                    palosPentagrama[i].SetActive(false);
                }
            }
           
        }

        public void establecerTipoNota(int numTipo)
        {
            tipoNotaActual = (TiposNotasLocal)numTipo;
            string tipoNotaActualStr = tipoNotaActual.ToString();
            Debug.Log(tipoNotaActualStr);

            //activar el hijo correcto (tal y como lo tengo, los hijos serían SOLO las apariencias de la nota)
            for (int i = 0; i < tiposNotas.Length; i++)
            {

                if (tiposNotas[i].tag.Equals(tipoNotaActualStr))
                {
                    tiposNotas[i].SetActive(true);
                }
                else
                {
                    tiposNotas[i].SetActive(false);
                }
            }

            //Debug.Log(tipoNotaActual);
        }

        public void activarModificacion() //función que establece lo necesario para cambiar la nota
        {
            brillo.SetActive(true);
            gestorNota.activarMenus(this.gameObject);
            sePuedeMover = true;
            notaModificandose = true;
        }

        public void finalizarModificacion()
        {
            brillo.SetActive(false);
            sePuedeMover = false;
            sePuedeModificar = false;
            notaModificandose = false;

            //decirle al gestor del pentagrama que ponga en activas el resto de notas
            gestionPentagrama.restablecerSeleccion();
        }


        //Para mover la nota
        void OnMouseDrag()
        {
            notaMoviendose = true;
        }
        private void OnMouseDown()
        {
            notaMoviendose = true;
            lastTouchPosition = Input.mousePosition;
        }

        private void OnMouseUp()
        {
            notaMoviendose = false;
        }

        //cambiamos el tamaño de la nota para indicar que su audio se está reproduciendo
        public void cambiarTallaGrande()
        {
            //this.GetComponent<Transform>().localScale = new Vector3(0.8f, 0.8f, 0.8f);
            notaSonando.SetActive(true);
        }

        public void cambiarTallaNormal()
        {
            //this.GetComponent<Transform>().localScale = new Vector3(0.74f, 0.74f, 0.74f);
            notaSonando.SetActive(false);
        }
    }
}
