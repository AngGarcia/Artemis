using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace General
{
    public class InicioPaciente : MonoBehaviour
    {
        [SerializeField] private GameObject infoPanel;
        [SerializeField] private GameObject fondoPausa;
        [SerializeField] private TMP_Text nombrePaciente;

        [Header("INTERFACES")]
        [SerializeField] private GameObject interfazListaPacientes;
        [SerializeField] private GameObject interfazInicioPaciente;
        [SerializeField] private GameObject mapa;

        [Header("BOTONES")]
        [SerializeField] private Button btnAyuda;
        [SerializeField] private Button btnJugar;
        [SerializeField] private Button btnPopUpLogOut;
        [SerializeField] private Button btnPopUpSave;
        [Header("POP-UPS")]
        [SerializeField] private GameObject popLogOut;
        [SerializeField] private GameObject popSaveData;
        [SerializeField] private GameObject popDataSaved;

        private bool activeInfoPanel;
        private bool pausar;
        private Sprite spriteBtnAyudaPressed;
        private Sprite spriteBtnAyudaNormal;

        void Start()
        {
            infoPanel.SetActive(false);
            activeInfoPanel = false;
            fondoPausa.SetActive(false);
            mapa.SetActive(false);

            spriteBtnAyudaNormal = btnAyuda.GetComponent<Image>().sprite;
            spriteBtnAyudaPressed = btnAyuda.GetComponent<Button>().spriteState.disabledSprite;

            btnAyuda.onClick.AddListener(toggleHelp);
            btnJugar.onClick.AddListener(irAlJuego);
            btnPopUpLogOut.onClick.AddListener(LogOut);
            btnPopUpSave.onClick.AddListener(SaveData);
        }

        private void OnDestroy()
        {
            btnAyuda.onClick.RemoveListener(toggleHelp);
            btnJugar.onClick.RemoveListener(irAlJuego);
            btnPopUpLogOut.onClick.RemoveListener(LogOut);
            btnPopUpSave.onClick.RemoveListener(SaveData);
        }

        public void setPaciente(Paciente pacienteActual)
        {
            ConectToDatabase.Instance.setCurrentPaciente(pacienteActual);
            nombrePaciente.text = ConectToDatabase.Instance.getCurrentPaciente().nombre + "!";

            Debug.Log("PACIENTE ACTUAL");
            ConectToDatabase.Instance.getCurrentPaciente().printValues();
        }

        private void irAlJuego()
        {
            ConectToDatabase.Instance.getCurrentPaciente().addNuevaSesion();
            //establecemos la última sesión añadida como la actual; cada vez que ingresemos en el juego, se crea una nueva sesión
            ConectToDatabase.Instance.setCurrentSesion(ConectToDatabase.Instance.getCurrentPaciente().sesiones[ConectToDatabase.Instance.getCurrentPaciente().sesiones.Count - 1]);
            ConectToDatabase.Instance.startTimeSesion();
            mapa.SetActive(true);
            interfazInicioPaciente.SetActive(false);
        }

        public void toggleHelp()
        {
            activeInfoPanel = !activeInfoPanel;
            infoPanel.SetActive(activeInfoPanel);

            if (activeInfoPanel)
            {
                btnAyuda.GetComponent<Image>().sprite = spriteBtnAyudaPressed;
            }
            else
            {
                btnAyuda.GetComponent<Image>().sprite = spriteBtnAyudaNormal;
            }
        }

        public void pausarJuego()
        {
            pausar = !pausar;
            fondoPausa.SetActive(pausar);

            if (pausar)
            {
                Time.timeScale = 0;
            }
            else
            {
                 
                Time.timeScale = 1;
            }
        }

        private void LogOut()
        {
            //reanudamos el juego
            pausarJuego();
            ConectToDatabase.Instance.resetCurrentPaciente();
            popLogOut.SetActive(false);
            interfazListaPacientes.SetActive(true);
            interfazInicioPaciente.SetActive(false);
        }

        private async void SaveData()
        {
            //guardamos los datos
            await ConectToDatabase.Instance.SaveDataPaciente();
            popSaveData.SetActive(false);
            popDataSaved.SetActive(true);
        }
    }
}
