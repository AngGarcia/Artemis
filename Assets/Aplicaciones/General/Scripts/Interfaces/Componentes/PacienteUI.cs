using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace General { 
    public class PacienteUI : MonoBehaviour
    {
        [Header("COMPONENTES UI")]
        [SerializeField] private TMP_Text ID;
        [SerializeField] private Button btnVerDatos;
        [SerializeField] private Button btnJuego;

        public GameObject DatosPaciente;
        public GameObject ListaPacientes;
        public GameObject InicioPaciente;

        public IngresoDatosPaciente scriptDatosPaciente;
        public InicioPaciente scriptInicioPaciente;

        private Paciente paciente;

        private void Start()
        {
            btnVerDatos.onClick.AddListener(verDatosPaciente);
            btnJuego.onClick.AddListener(irAlJuego);
        }

        private void OnDestroy()
        {
            btnVerDatos.onClick.RemoveAllListeners();
            btnJuego.onClick.RemoveAllListeners();
        }

        public void setPaciente(Paciente pacienteActual)
        {
            paciente = new Paciente();
            paciente = pacienteActual;
            paciente.printValues();
            ID.text = paciente.id;
        }

        public void setInterfaces(GameObject datosPaciente, GameObject listaPacientes, GameObject inicioPaciente)
        {
            DatosPaciente = datosPaciente;
            ListaPacientes = listaPacientes;
            InicioPaciente = inicioPaciente;
        }
        public void setScripts(IngresoDatosPaciente script1, InicioPaciente script2)
        {
            scriptDatosPaciente = script1;
            scriptInicioPaciente = script2;
        }
        private void verDatosPaciente()
        {
            DatosPaciente.SetActive(true);
            paciente.printValues();
            scriptDatosPaciente.verDatosPaciente(paciente);
            ListaPacientes.SetActive(false);
        }

        private void irAlJuego()
        {
            InicioPaciente.SetActive(true);
            scriptInicioPaciente.setPaciente(paciente);
            paciente.printValues();
            ListaPacientes.SetActive(false);
        }
    }
}
