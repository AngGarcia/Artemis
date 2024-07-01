using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace General
{
    public class IngresoDatosPaciente : MonoBehaviour
    {
        [SerializeField] private GameObject prefabSesion;
        [SerializeField] private GameObject sesionModal;
        [SerializeField] private Transform spawnLista;
        //INPUTS DE CREAR PACIENTE
        [Header("INPUTS Y TEXTOS")]
        [SerializeField] private TMP_Text nicknamePaciente;
        [SerializeField] private TMP_InputField inputPacienteNombre;
        [SerializeField] private TMP_InputField inputPacienteApellidos;
        [SerializeField] private TMP_Text labelPacienteNombre;
        [SerializeField] private TMP_Text labelPacienteApellidos;
        [SerializeField] private TMP_Text medicoAsignado;

        [Header("BOTONES")]
        [SerializeField] private Button btnCreatePaciente;
        [SerializeField] private Button btnEditarPaciente;
        [SerializeField] private Button btnBack;

        [Header("COMPONENTES")]
        [SerializeField] private GameObject scroll;
        [SerializeField] private GameObject avisoSinDatos;
        [SerializeField] private GameObject inputNombre;
        [SerializeField] private GameObject inputApellidos;
        [SerializeField] private GameObject tagNombre;
        [SerializeField] private GameObject tagApellidos;
        [SerializeField] private GameObject BotonCrearPaciente;
        [SerializeField] private GameObject BotonEditarPaciente;
        [SerializeField] private GameObject avisoApellidos;
        [SerializeField] private GameObject avisoDatos;

        [Header("INTERFACES")]
        [SerializeField] private GameObject listaPacientes; 
        [SerializeField] private GameObject ingresoDatosPaciente;

        [Header("SCRIPTS")]
        [SerializeField] private ListaPacientes scriptListaPacientes;

        private Paciente paciente;
        private List<GameObject> sesionesUI;

        void Start()
        {
            btnBack.onClick.AddListener(goToListaPacientes);
            btnCreatePaciente.onClick.AddListener(createNewPaciente);
            //HACER EL MODO DE EDITAR EL PACIENTE
        }

        // Update is called once per frame
        private void OnDestroy()
        {
            btnBack.onClick.RemoveListener(goToListaPacientes);
            btnCreatePaciente.onClick.RemoveAllListeners();
        }

        public void establecerNuevoPaciente()
        {
            avisoSinDatos.SetActive(true);
            scroll.SetActive(false);
            BotonCrearPaciente.SetActive(true);
            BotonEditarPaciente.SetActive(false);

            nicknamePaciente.text = "";
            medicoAsignado.text = ConectToDatabase.Instance.getCurrentMedico().nombre;

            inputNombre.SetActive(true);
            inputApellidos.SetActive(true);
            tagNombre.SetActive(false);
            tagApellidos.SetActive(false);
            avisoApellidos.SetActive(false);
            avisoDatos.SetActive(false);
        }

        public void verDatosPaciente(Paciente pacienteActual)
        {
            paciente = new Paciente();
            sesionesUI = new List<GameObject>();

            BotonCrearPaciente.SetActive(false);
            BotonEditarPaciente.SetActive(true);
            inputNombre.SetActive(false);
            inputApellidos.SetActive(false);
            tagNombre.SetActive(true);
            tagApellidos.SetActive(true);
            avisoApellidos.SetActive(false);
            avisoDatos.SetActive(false);

            //obtener los datos del paciente
            paciente = pacienteActual;
            paciente.printValues();

            nicknamePaciente.text = paciente.id;
            labelPacienteNombre.text = paciente.nombre;
            labelPacienteApellidos.text = paciente.apellidos;
            medicoAsignado.text = paciente.medicoAsignado;

            //poner en el scroll las sesiones; si está vacio, dejar los datos previos
            if(paciente.sesiones.Count == 0)
            {
                avisoSinDatos.SetActive(true);
                scroll.SetActive(false);
            }
            else
            {
                destroyUIComponents();
                avisoSinDatos.SetActive(false);
                scroll.SetActive(true);

                //poner en el scroll las sesiones del paciente
                for (int i=0; i < paciente.sesiones.Count; i++)
                {
                    GameObject filaLista = Instantiate(prefabSesion, spawnLista);
                    filaLista.SetActive(true);
                    filaLista.GetComponent<SesionUI>().setData(sesionModal, paciente.sesiones[i], i+1);
                    sesionesUI.Add(filaLista);
                }
            }

        }

        private void createNewPaciente()
        {
            bool apellidosCorrect = false;
            bool datosCorrect = false;

            if (inputPacienteNombre.text.Length > 0 && inputPacienteApellidos.text.Length > 0)
            {
                datosCorrect = true;
                avisoDatos.SetActive(false);
            }
            else
            {
                avisoDatos.SetActive(true);
            }

            if (datosCorrect)
            {
                //comprobamos que el campo 'Apellidos' tenga 2 apellidos, ni más ni menos
                string[] apellidosAux = inputPacienteApellidos.text.Split(' ');

                if (apellidosAux.Length == 2)
                {
                    apellidosCorrect = true;
                    avisoApellidos.SetActive(false);
                }
                else
                {
                    avisoApellidos.SetActive(true);
                }

            }


            if (datosCorrect && apellidosCorrect)
            {
                string nombre = inputPacienteNombre.text;
                string apellidos = inputPacienteApellidos.text;

                //CREAMOS EL NICKNAME/ID A PARTIR DE LAS INICIALES DEL NOMBRE COMPLETO
                //ELISA ALONSO SÁEZ --> EAS

                string[] apellidosAux = apellidos.Split(' ');
                string firstApellido = apellidosAux[0];
                string secondApellido = apellidosAux[1];

                char letterOne = nombre[0];
                char letterTwo = firstApellido[0];
                char letterThree = secondApellido[0];

                string nickname = $"{letterOne}{letterTwo}{letterThree}";

                Debug.Log("ID: " + nickname);

                Debug.Log("Creando nuevo paciente...");
                ConectToDatabase.Instance.CreatePaciente(nickname, nombre, apellidos);

                //ir a la pantalla de lista de pacientes
                listaPacientes.SetActive(true);
                scriptListaPacientes.resetLists();
                scriptListaPacientes.startGetPacientes();
                ingresoDatosPaciente.SetActive(false);
            }
        }

        private void goToListaPacientes()
        {
            resetInputs();
            listaPacientes.SetActive(true);
            ingresoDatosPaciente.SetActive(false);
        }

        private void destroyUIComponents()
        {
            for (int i = 0; i < sesionesUI.Count; i++)
            {
                Destroy(sesionesUI[i]);
            }

            sesionesUI = new List<GameObject>();
        }

        public void resetInputs()
        {
            nicknamePaciente.text = "";
            inputPacienteNombre.text = "";
            inputPacienteApellidos.text = "";

            labelPacienteNombre.text = "";
            labelPacienteApellidos.text = "";
            medicoAsignado.text = "";

            paciente = new Paciente();
            destroyUIComponents();
        }
    }
}
