using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace General
{
    public class MainMenuManagerV2 : MonoBehaviour
    {
        [SerializeField] private GameObject fondo;

        [Header("INTERFACES")]
        [SerializeField] private GameObject login;
        [SerializeField] private GameObject listaPacientes;
        [SerializeField] private GameObject datosPaciente;
        [SerializeField] private GameObject createUsuario;
        [SerializeField] private GameObject mapa;


        [Header("BOTONES")]
        [SerializeField] private Button btnLogin;
        [SerializeField] private Button btnCreateUser;
        [SerializeField] private Button btnNuevoTerapeuta;
        [SerializeField] private Button btnNuevoPaciente;

        [Header("TEXTOS")]
        [SerializeField]
        private TMP_Text msgErrorCrearUsuario;
        [SerializeField]
        private TMP_Text msgTituloCrearUsuario;

        //INPUTS DEL LOGIN
        [Header("INPUTS DEL LOGIN")]
        [SerializeField]
        private TMP_InputField loginInputEmail;
        [SerializeField]
        private TMP_InputField loginInputPwd;

        //INPUTS DE LOS CREATES
        [Header("INPUTS DEL CREATE")]
        [SerializeField]
        private TMP_InputField createInputEmail;
        [SerializeField]
        private TMP_InputField createInputPwd;
        [SerializeField]
        private TMP_InputField createInputNickname;
        [SerializeField]
        private TMP_InputField createInputNombre;
        [SerializeField]
        private TMP_InputField createInputApellidos;

        [Header("PANELES DEL CREATE")]
        [SerializeField]
        private GameObject inputsTerapeuta;
        [SerializeField]
        private GameObject inputsPaciente;

        private bool creatingPaciente;
        void Start()
        {

            /*if (ConectToDatabase.Instance.isLogged()) //da errores idk why
            {
                Debug.Log("HAY UN USUARIO LOGGEADO");
                establecerUsuario();
            }
            else
            {
                Debug.Log("NOOOOO HAY USUARIO");
                login.SetActive(true);
                mapa.SetActive(false);
                fondo.SetActive(true);
            }*/

            mapa.SetActive(false);
            fondo.SetActive(true);

            btnLogin.onClick.AddListener(awaitLogin);
            btnNuevoTerapeuta.onClick.AddListener(addNewTerapeuta);
            btnNuevoPaciente.onClick.AddListener(addNewPaciente);
            btnCreateUser.onClick.AddListener(createNewUser);
        }

        private void OnDestroy()
        {
            btnLogin.onClick.RemoveListener(awaitLogin);
            btnNuevoPaciente.onClick.RemoveAllListeners();
            btnNuevoTerapeuta.onClick.RemoveAllListeners();
            btnCreateUser.onClick.RemoveListener(createNewUser);
        }

        public async void awaitLogin()
        {
            string email = loginInputEmail.text;
            string password = loginInputPwd.text;

            ConectToDatabase.Instance.LogOut();

            btnLogin.interactable = false;
            await ConectToDatabase.Instance.LoginMedico(email, password);
            btnLogin.interactable = true;

            if (ConectToDatabase.Instance.isLogged())
            {
                login.SetActive(false);
                listaPacientes.SetActive(true);
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
            createUsuario.SetActive(true);
            msgTituloCrearUsuario.text = "Crear terapeuta";
            inputsPaciente.SetActive(false);
            inputsTerapeuta.SetActive(true);
            creatingPaciente = false;
        }
        public void addNewPaciente()
        {
            listaPacientes.SetActive(false);
            createUsuario.SetActive(true);
            msgTituloCrearUsuario.text = "Crear paciente";
            inputsPaciente.SetActive(true);
            inputsTerapeuta.SetActive(false);
            creatingPaciente = true;
        }

        public async void createNewUser()
        {
            bool emailCorrect = false;
            bool pwdCorrect = false;
            bool nickNameCorrect = false;

            // se está creando un paciente que no necesitará email ni contraseña
            if (creatingPaciente)
            {
                if (createInputNickname.text.Length > 0)
                {
                    nickNameCorrect = true;
                }
                else
                {
                    Debug.Log("ERROR: nickname incorrecto");
                    msgErrorCrearUsuario.text = "ERROR: nickname incorrecto.";
                }

                if (nickNameCorrect)
                {
                    string nickname = createInputNickname.text;
                    string nombre = createInputNombre.text;
                    string apellidos = createInputApellidos.text;

                    Debug.Log("Creando nuevo paciente...");
                    ConectToDatabase.Instance.CreatePaciente(nickname, nombre, apellidos);
                }
            }
            else
            {
                //primero vemos que sea, efectivamente, un email
                if (createInputEmail.text.Contains("@") && ((createInputEmail.text.Contains(".com") || createInputEmail.text.Contains(".es"))))
                {
                    emailCorrect = true;
                }
                else
                {
                    Debug.Log("ERROR: email incorrecto");
                    msgErrorCrearUsuario.text = "ERROR: email incorrecto.";
                }

                //después comprobamos que la contraseña sea correcta
                if (createInputPwd.text.Length >= 6)
                {
                    pwdCorrect = true;
                }
                else
                {
                    Debug.Log("ERROR: la contraseña debe tener mínimo 6 caracteres.");
                    msgErrorCrearUsuario.text = "ERROR: la contraseña debe tener mínimo 6 caracteres.";
                }

                if (emailCorrect && pwdCorrect)
                {
                    string email = createInputEmail.text;
                    string password = createInputPwd.text;
                    string nombre = createInputNombre.text;
                    string apellidos = createInputApellidos.text;

                    Debug.Log("Creando nuevo terapeuta...");
                    await ConectToDatabase.Instance.CreateUser(email, password, nombre, apellidos, true);
                    ConectToDatabase.Instance.LogOut();
                    btnCreateUser.interactable = false;
                    //comprobar si el usuario actual está
                    await ConectToDatabase.Instance.LoginMedico(email, password);

                    btnCreateUser.interactable = true;

                    if (ConectToDatabase.Instance.isLogged())
                    {
                        resetInputs();
                        createUsuario.SetActive(false);
                        listaPacientes.SetActive(true);
                    }
                }
            }
        }

        public void exitCreateUser()
        {
            resetInputs();

            if (creatingPaciente)
            {
                createUsuario.SetActive(false);
                listaPacientes.SetActive(true);
            }
            else
            {
                createUsuario.SetActive(false);
                login.SetActive(true);
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

            createInputNickname.text = "";
            createInputEmail.text = "";
            createInputPwd.text = "";
            createInputNombre.text = "";
            createInputApellidos.text = "";
        }
    }
}
