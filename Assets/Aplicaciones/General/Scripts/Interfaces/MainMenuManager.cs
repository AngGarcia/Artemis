using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace General
{
    public class MainMenuManager : MonoBehaviour
    {
        [Header("INTERFACES")]
        [SerializeField] private GameObject login;
        [SerializeField] private GameObject listaPacientes;
        [SerializeField] private GameObject datosPaciente;
        [SerializeField] private GameObject ingresarTerapeuta;
        [SerializeField] private GameObject mapa;

        [Header("BOTONES")]
        [SerializeField] private Button btnLogin;
        [SerializeField] private Button btnCreateTerapeuta;
        [SerializeField] private Button btnNuevoTerapeuta;
        [SerializeField] private Button btnNuevoPaciente;

        //INPUTS DEL LOGIN
        [Header("INPUTS DEL LOGIN")]
        [SerializeField] private TMP_InputField loginInputEmail;
        [SerializeField] private TMP_InputField loginInputPwd;

        //INPUTS DE CREAR TERAPEUTA
        [Header("INGRESAR TERAPEUTA")]
        [SerializeField] private TMP_InputField inputEmail;
        [SerializeField] private TMP_InputField inputPwd;
        [SerializeField] private TMP_InputField inputTerapeutaNombre;
        [SerializeField] private TMP_InputField inputTerapeutaApellidos;
        [SerializeField] private GameObject avisoEmail;
        [SerializeField] private GameObject avisoContraseña;

        [Header("SCRIPTS")]
        [SerializeField] private ListaPacientes scriptListaPacientes;
        [SerializeField] private IngresoDatosPaciente scriptIngresoDatosPaciente;

        void Start()
        {

            /*if (ConectToDatabase.Instance.isLogged()) //da errores porque aparentemente, auth es Null, pero en el script antiguo NO ES NULO
            {
                Debug.Log("HAY UN USUARIO LOGGEADO");
                establecerUsuario();
            }
            else
            {
                Debug.Log("NOOOOO HAY USUARIO");
                login.SetActive(true);
                mapa.SetActive(false);
            }*/

            login.SetActive(true);
            mapa.SetActive(false);
            avisoEmail.SetActive(false);
            avisoContraseña.SetActive(false);

            btnLogin.onClick.AddListener(awaitLogin);
            btnNuevoTerapeuta.onClick.AddListener(addNewTerapeuta);
            btnNuevoPaciente.onClick.AddListener(addNewPaciente);
            btnCreateTerapeuta.onClick.AddListener(createNewTerapeuta);
        }

        private void OnDestroy()
        {
            btnLogin.onClick.RemoveListener(awaitLogin);
            btnNuevoPaciente.onClick.RemoveAllListeners();
            btnNuevoTerapeuta.onClick.RemoveAllListeners();
            btnCreateTerapeuta.onClick.RemoveAllListeners();
        }

        public async void awaitLogin()
        {
            string email = loginInputEmail.text;
            string password = loginInputPwd.text;

            //GESTIONAR SI NO SE HAN RELLENADO X CAMPOS

            ConectToDatabase.Instance.LogOut();

            btnLogin.interactable = false;
            await ConectToDatabase.Instance.LoginMedico(email, password); //este await es falso, porque en realidad no espera
            await ConectToDatabase.Instance.getActualUser(); //obtenemos el terapeuta loggeado
            btnLogin.interactable = true;

            if (ConectToDatabase.Instance.isLogged())
            {
                login.SetActive(false);
                listaPacientes.SetActive(true);
                //llamar aquí a la función que cree la lista de pacientes
                //no se debe de hacer antes, ya que también hay que coger la lista de pacientes asignador al terapeuta loggeado
                scriptListaPacientes.startGetPacientes();
            }
        }

        private async void establecerUsuario()
        {
            await ConectToDatabase.Instance.obtenerUsuario();
            listaPacientes.SetActive(true);
            login.SetActive(false);
            ConectToDatabase.Instance.getCurrentMedico().printValues();
        }

        public void addNewTerapeuta()
        {
            login.SetActive(false);
            ingresarTerapeuta.SetActive(true);
        }
        public void addNewPaciente()
        {
            listaPacientes.SetActive(false);
            datosPaciente.SetActive(true);
            scriptIngresoDatosPaciente.establecerNuevoPaciente();
        }

        public async void createNewTerapeuta()
        {
            bool emailCorrect = false;
            bool pwdCorrect = false;

            //primero vemos que sea, efectivamente, un email
            if (inputEmail.text.Contains("@") && ((inputEmail.text.Contains(".com") || inputEmail.text.Contains(".es"))))
            {
                emailCorrect = true;
                avisoEmail.SetActive(false);
            }
            else
            {
                Debug.Log("ERROR: email incorrecto");
                avisoEmail.SetActive(true);
            }

            //después comprobamos que la contraseña sea correcta
            if (inputPwd.text.Length >= 6)
            {
                pwdCorrect = true;
                avisoContraseña.SetActive(false);
            }
            else
            {
                Debug.Log("ERROR: la contraseña debe tener mínimo 6 caracteres.");
                avisoContraseña.SetActive(true);
            }

            if (emailCorrect && pwdCorrect)
            {
                string email = inputEmail.text;
                string password = inputPwd.text;
                string nombre = inputTerapeutaNombre.text;
                string apellidos = inputTerapeutaApellidos.text;

                Debug.Log("Creando nuevo terapeuta...");
                await ConectToDatabase.Instance.CreateUser(email, password, nombre, apellidos, true);
                ConectToDatabase.Instance.LogOut();
                btnCreateTerapeuta.interactable = false;
                //comprobar si el usuario actual está
                await ConectToDatabase.Instance.LoginMedico(email, password);

                btnCreateTerapeuta.interactable = true;

                if (ConectToDatabase.Instance.isLogged())
                {
                    resetInputs();
                    ingresarTerapeuta.SetActive(false);
                    listaPacientes.SetActive(true);
                }
            }
        }

        public void LogOut()
        {
            ConectToDatabase.Instance.LogOut();
        }

        public void resetInputs()
        {
            loginInputEmail.text = "";
            loginInputPwd.text = "";

            //createInputNickname.text = "";
            inputEmail.text = "";
            inputPwd.text = "";
            inputTerapeutaNombre.text = "";
            inputTerapeutaApellidos.text = "";

            avisoEmail.SetActive(false);
            avisoContraseña.SetActive(false);
        }
    }
}
