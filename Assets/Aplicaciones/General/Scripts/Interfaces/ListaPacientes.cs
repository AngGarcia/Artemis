using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


namespace General
{
    public class ListaPacientes : MonoBehaviour
    {
        [SerializeField] private GameObject prefabPacienteUI;
        [SerializeField] private GameObject prefabTagRestoPacientes;
        [SerializeField] private Transform spawnListaTerapeuta;
        [SerializeField] private Transform spawnListaResto;

        [Header("INTERFACES")]
        [SerializeField] private GameObject interfazDatosPaciente;
        [SerializeField] private GameObject interfazListaPacientes;
        [SerializeField] private GameObject interfazInicioPaciente;

        [Header("SCRIPTS")]
        [SerializeField] private IngresoDatosPaciente scriptDatosPaciente;
        [SerializeField] private InicioPaciente scriptInicioPaciente;

        private List<Paciente> pacientesTotales;
        private List<Paciente> pacientesTerapeuta;
        private List<Paciente> restoPacientes;
        private List<GameObject> pacientesUI; //array de los GameObjects creados

        private int separacionAxisY = 60;
        private int separacionAxisX = 525;

        private void Start()
        {
            pacientesTotales = new List<Paciente>();
            pacientesTerapeuta = new List<Paciente>();
            restoPacientes = new List<Paciente>();
            pacientesUI = new List<GameObject>();
        }

        public void startGetPacientes()
        {
            // StartCoroutine(getAllPacientes());
            getAllPacientes();
        }

        private void maquetarUI()
        {
            Transform spawnFila = spawnListaTerapeuta;

            for (int i = 0; i < pacientesTerapeuta.Count; i++)
            {
                GameObject filaLista = Instantiate(prefabPacienteUI, spawnFila);
                filaLista.SetActive(true);
                filaLista.GetComponent<PacienteUI>().setPaciente(pacientesTerapeuta[i]);
                filaLista.GetComponent<PacienteUI>().setInterfaces(interfazDatosPaciente, interfazListaPacientes, interfazInicioPaciente);
                filaLista.GetComponent<PacienteUI>().setScripts(scriptDatosPaciente, scriptInicioPaciente);
                pacientesUI.Add(filaLista);
            }

            spawnFila = spawnListaResto;

            for (int i = 0; i < restoPacientes.Count; i++)
            {
                GameObject filaLista = Instantiate(prefabPacienteUI, spawnFila);
                filaLista.SetActive(true);
                filaLista.GetComponent<PacienteUI>().setPaciente(restoPacientes[i]);
                filaLista.GetComponent<PacienteUI>().setInterfaces(interfazDatosPaciente, interfazListaPacientes, interfazInicioPaciente);
                filaLista.GetComponent<PacienteUI>().setScripts(scriptDatosPaciente, scriptInicioPaciente);
                pacientesUI.Add(filaLista);
            }
        }

        //creamos los componentes de la UI
        private void maquetarUI_v1()
        {
            Transform spawnFila = spawnListaTerapeuta;
            float yPos;
            int index = 0;

            for (int i=0; i < pacientesTerapeuta.Count; i++)
            {
                yPos = spawnFila.position.y - (i * separacionAxisY * 3);

                GameObject filaLista = Instantiate(prefabPacienteUI, spawnFila);
                filaLista.transform.position = new Vector3(spawnFila.position.x, yPos, spawnFila.position.z);
                filaLista.GetComponent<PacienteUI>().setPaciente(pacientesTerapeuta[i]);
                filaLista.SetActive(true);
                pacientesUI.Add(filaLista);

                index = i;
            }

            index++;
            yPos = spawnFila.position.y - (index * separacionAxisY * 3);
            GameObject tagRestoPacientes = Instantiate(prefabTagRestoPacientes, spawnFila);
            tagRestoPacientes.transform.position = new Vector3(spawnFila.position.x - separacionAxisX, yPos, spawnFila.position.z);
            index++;

            for (int i = 0; i < restoPacientes.Count; i++)
            {
                yPos = spawnFila.position.y - (index * separacionAxisY * 3);

                GameObject filaLista = Instantiate(prefabPacienteUI, spawnFila);
                filaLista.transform.position = new Vector3(spawnFila.position.x, yPos, spawnFila.position.z);
                filaLista.GetComponent<PacienteUI>().setPaciente(restoPacientes[i]);
                filaLista.SetActive(true);
                pacientesUI.Add(filaLista);

                index++;
            }

        }

        private async void getAllPacientes()
        {
            pacientesTotales = await ConectToDatabase.Instance.getAllPacientes();
            //Debug.Log("nº pacientes totales:");
            //Debug.Log("Tamaño: " + pacientesTotales.Count);
            //Debug.Log("ID del medico actual: " + ConectToDatabase.Instance.getCurrentMedico().id); 
           // printAllPacientes();

            for (int i=0; i < pacientesTotales.Count; i++)
            {
               // Debug.Log("pacientesTotales[i].idMedico: " + pacientesTotales[i].idMedico);
                if (pacientesTotales[i].idMedico == ConectToDatabase.Instance.getCurrentMedico().id)
                {
                    pacientesTerapeuta.Add(pacientesTotales[i]);
                }
                else
                {
                    restoPacientes.Add(pacientesTotales[i]);
                }
            }

            //listaAMostrar = pacientesTerapeuta;
            maquetarUI();
        }

        //DEBUG
        private void printAllPacientes() {

            for (int i=0; i< pacientesTotales.Count; i++)
            {
                pacientesTotales[i].printValues();
            }
        }

        private void destroyUIComponents()
        {
            for (int i=0; i < pacientesUI.Count; i++)
            {
                Destroy(pacientesUI[i]);
            }
        }

        public void resetLists()
        {
            //HAY QUE BORRAR LOS OBJETOS DEL CANVAS GENERADOS
            destroyUIComponents();

            pacientesTotales = new List<Paciente>();
            pacientesTerapeuta = new List<Paciente>();
            restoPacientes = new List<Paciente>();
            pacientesUI = new List<GameObject>();
        }
    }
}
