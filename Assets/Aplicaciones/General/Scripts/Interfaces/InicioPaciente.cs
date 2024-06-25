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

        [Header("BOTONES")]
        [SerializeField] private Button btnAyuda;
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

            spriteBtnAyudaNormal = btnAyuda.GetComponent<Image>().sprite;
            spriteBtnAyudaPressed = btnAyuda.GetComponent<Button>().spriteState.disabledSprite;

            btnAyuda.onClick.AddListener(toggleHelp);
            btnPopUpLogOut.onClick.AddListener(LogOut);
            btnPopUpSave.onClick.AddListener(SaveData);
        }

        private void OnDestroy()
        {
            btnAyuda.onClick.RemoveListener(toggleHelp);
            btnPopUpLogOut.onClick.RemoveListener(LogOut);
            btnPopUpSave.onClick.RemoveListener(SaveData);
        }

        public void setPaciente(Paciente pacienteActual)
        {
            ConectToDatabase.Instance.setCurrentPaciente(pacienteActual);
            nombrePaciente.text = pacienteActual.nombre;

            Debug.Log("PACIENTE ACTUAL");
            ConectToDatabase.Instance.getCurrentPaciente().printValues();
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
            await ConectToDatabase.Instance.SaveData();
            popSaveData.SetActive(false);
            popDataSaved.SetActive(true);
        }
    }
}
