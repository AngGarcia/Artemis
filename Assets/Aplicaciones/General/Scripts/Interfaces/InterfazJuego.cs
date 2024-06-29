using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static SceneChanger;

namespace General {
    public class InterfazJuego : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private GameObject interfazTest;
        [SerializeField] private DisplayTest testPsicometrico;
        [Space]
        [Header("Botones")]
        [SerializeField] private GameObject botonSalir;
        [SerializeField] private GameObject botonPausa;
        [SerializeField] private GameObject botonMapa;
        [SerializeField] private Button botonSiguiente;
        [SerializeField] private Button botonRealizarTest;
        [SerializeField] private Button botonPopUpIrMapa;
        [SerializeField] private Button botonPopUpExitActividad;
        [SerializeField] private Button botonQuitTest;
        [SerializeField] private Button botonSaveTest;

        [Header("Elementos")]
        [SerializeField] private TMP_Text nombrePacienteTest;
        [SerializeField] private GameObject popExit;
        [SerializeField] private GameObject popMapa;
        [SerializeField] private GameObject juegoPausa;
        [SerializeField] private GameObject fondoPausa;

        [Space]
        [SerializeField] private Scenes nextScene;

        private bool btnPausaClicked;
        private bool btnMapaClicked;
        private bool btnExitClicked;

        private Sprite spriteMapaPressed;
        private Sprite spriteMapaNormal;
        private Sprite spritePausaPressed;
        private Sprite spritePausaNormal;
        void Start()
        {
            juegoPausa.SetActive(false);
            popMapa.SetActive(false);
            popExit.SetActive(false);
            fondoPausa.SetActive(false);
            interfazTest.SetActive(false);
            btnMapaClicked = false;
            btnPausaClicked = false;
            btnExitClicked = false;

            spriteMapaNormal = botonMapa.GetComponent<Image>().sprite;
            spriteMapaPressed = botonMapa.GetComponent<Button>().spriteState.pressedSprite;

            spritePausaNormal = botonPausa.GetComponent<Image>().sprite;
            spritePausaPressed = botonPausa.GetComponent<Button>().spriteState.disabledSprite;

            botonPopUpIrMapa.onClick.AddListener(delegate { SceneChanger.Instance.GoToScene(Scenes.MainMenu); });

            botonSiguiente.onClick.AddListener(delegate { SceneChanger.Instance.GoToScene(nextScene); });

            botonPopUpExitActividad.onClick.AddListener(delegate { SceneChanger.Instance.GoToScene(Scenes.MainMenu); });

            botonRealizarTest.onClick.AddListener(hacerTest);
            botonQuitTest.onClick.AddListener(cerrarTest);
            botonSaveTest.onClick.AddListener(guardarTest);
        }

        public void pausarJuego()
        {
            if (btnPausaClicked)
            {
                Time.timeScale = 1;
                botonPausa.GetComponent<Image>().sprite = spritePausaNormal;
                juegoPausa.SetActive(false);
                btnPausaClicked = false;
            }
            else
            {
                Time.timeScale = 0;
                botonPausa.GetComponent<Image>().sprite = spritePausaPressed;
                juegoPausa.SetActive(true);
                btnPausaClicked = true;
            }
        }

        public void pulsarMapa()
        {
            if (btnMapaClicked)
            {
                Time.timeScale = 1;
                fondoPausa.SetActive(false);
                botonMapa.GetComponent<Image>().sprite = spriteMapaNormal;
                popMapa.SetActive(false);
                btnMapaClicked = false;
            }
            else
            {
                Time.timeScale = 0;
                fondoPausa.SetActive(true);
                botonMapa.GetComponent<Image>().sprite = spriteMapaPressed;
                popMapa.SetActive(true);
                btnMapaClicked = true;
            }
        }

        public void salirActividad()
        {
            if (btnExitClicked)
            {
                Time.timeScale = 1;
                fondoPausa.SetActive(false);
                popExit.SetActive(false);
                btnExitClicked = false;
            }
            else
            {
                Time.timeScale = 0;
                fondoPausa.SetActive(true);
                popExit.SetActive(true);
                btnExitClicked = true;
            }
        }

        public void cerrarMapaPopUp()
        {
            Time.timeScale = 1;
            fondoPausa.SetActive(false);
            botonMapa.GetComponent<Image>().sprite = spriteMapaNormal;
            popMapa.SetActive(false);
            btnMapaClicked = false;
        }

        public void cerrarExitPopUp()
        {
            Time.timeScale = 1;
            fondoPausa.SetActive(false);
            popExit.SetActive(false);
            btnExitClicked = false;
        }

        private void hacerTest()
        {
            nombrePacienteTest.text = ConectToDatabase.Instance.getCurrentPaciente().nombre + "?";
            Time.timeScale = 0;
            interfazTest.SetActive(true);
        }

        private void cerrarTest()
        {
            interfazTest.SetActive(false);
            Time.timeScale = 1;
        }

        private void guardarTest()
        {
            int valorTest = (int)testPsicometrico.getValue();
            int tiempoActualSesion = (int)ConectToDatabase.Instance.getCurrentTimeSesion();
            //Debug.Log("tiempoActualSesion: " + tiempoActualSesion);
            Scenes escena = SceneChanger.Instance.actualScene;
            string momento;

            if (escena == Scenes.MainMenu)
            {
                momento = "Mapa";
            }
            else
            {
                momento = escena.ToString();
            }

            //"momento" es de prueba, hay que detectar en qué escena está
            ConectToDatabase.Instance.getCurrentSesion().addNuevoEstado(momento, (EstadoPaciente)valorTest, tiempoActualSesion);
            ConectToDatabase.Instance.getCurrentSesion().printSesionValues();
        }

    }
}
