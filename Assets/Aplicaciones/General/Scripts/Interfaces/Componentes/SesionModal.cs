using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace General
{
    public class SesionModal : MonoBehaviour
    {
        [Header("COMPONENTES")]
        [SerializeField] private GameObject modal;
        [SerializeField] private TMP_Text numSesion;
        [SerializeField] private TMP_Text fecha;
        [SerializeField] private TMP_Text tiempo;
        [SerializeField] private TMP_InputField observaciones;
        [SerializeField] private Button btnCloseModal;

        [SerializeField] private TMP_Text[] estados;

        private Sesion sesion;

        void Start()
        {
            btnCloseModal.onClick.AddListener(closeModal);
        }

        private void OnDestroy()
        {
            btnCloseModal.onClick.RemoveListener(closeModal);
        }

        public void openModal(Sesion sesionActual, string numSesion)
        {
            modal.SetActive(true);

            sesion = sesionActual;

            //ponemos en los campos los valores concretos del paciente
            this.numSesion.text = numSesion;
            fecha.text = sesion.getFecha();
            observaciones.text = sesion.getObservaciones();
            tiempo.text = sesion.getDuracion();

            for (int i=0; i < sesion.progreso.Count; i++) // el máximo siempre será 12, el mismo tamaño que el array 'estados'
            {
                estados[i].text = sesion.progreso[i].estado.ToString(); //se pondrá el número? si se pone hacemos un switch
            }
        }

        private async void closeModal()
        {
            modal.SetActive(false);

            //reseteamos los campos del modal
            this.numSesion.text = "";
            fecha.text = "";

            //guardar la nueva observación en la BBDD y después resetear el campo
            sesion.setObservaciones(observaciones.text);
            int index = int.Parse(sesion.id);
            
            ConectToDatabase.Instance.getCurrentPaciente().sesiones[index] = sesion;
            ConectToDatabase.Instance.getCurrentPaciente().sesiones[index].printSesionValues();
            await ConectToDatabase.Instance.SaveDataPaciente();

            observaciones.text = "";
            tiempo.text = "";

            for (int i = 0; i < estados.Length; i++) 
            {
                estados[i].text = "-";
            }

        }
    }
}
