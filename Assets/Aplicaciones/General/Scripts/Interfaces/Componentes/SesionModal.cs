using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace General
{
    public class SesionModal : MonoBehaviour
    {
        [SerializeField] private GameObject prefabTest;
        [SerializeField] private Transform spawnLista;
        [Header("COMPONENTES")]
        [SerializeField] private GameObject modal;
        [SerializeField] private TMP_Text numSesion;
        [SerializeField] private TMP_Text fecha;
        [SerializeField] private TMP_Text tiempo;
        [SerializeField] private TMP_InputField observaciones;
        [SerializeField] private Button btnCloseModal;
        [SerializeField] private GameObject listaTests;

        private Sesion sesion;
        private List<GameObject> testsUI;

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

            //tenemos que crear la lista de test
            testsUI = new List<GameObject>();

            for (int i=0; i< sesion.progreso.Count; i++)
            {
                GameObject filaLista = Instantiate(prefabTest, spawnLista);
                filaLista.SetActive(true);
                filaLista.GetComponent<TestUI>().setInfo(sesion.progreso[i].momento, sesion.progreso[i].estado, sesion.progreso[i].tiempo);
                testsUI.Add(filaLista);
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
            destroyUIComponents();
        }

        private void destroyUIComponents()
        {
            for (int i = 0; i < testsUI.Count; i++)
            {
                Destroy(testsUI[i]);
            }

            testsUI = new List<GameObject>();
        }
    }
}
