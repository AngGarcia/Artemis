using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace General
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject infoPanel;
        [SerializeField] private GameObject seleccionUsuario;
        [SerializeField] private GameObject login;
        [SerializeField] private GameObject createUser;
        [SerializeField] private GameObject mapa;

        [Header("BOTONES")]
        [SerializeField] Button btnPaciente;
        [SerializeField] Button btnMedico;
        [SerializeField] Button btnAyuda;
        [SerializeField] private Button btnLogin;
        [SerializeField] private Button btnCreateUser;

        [Header("TEXTOS")]
        [SerializeField]
        private TMP_Text msgErrorCrearUsuario;
        [SerializeField]
        private TMP_Text msgNuevoUsuario;
        [SerializeField]
        private TMP_Text msgTituloCrearUsuario;

        //INPUTS DE LOS LOGINS
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
        private TMP_InputField createInputNombre;
        [SerializeField]
        private TMP_InputField createInputApellidos;

        [Header("ScriptMAPA")]
        [SerializeField]
        private MapaPrincipal scriptMapa;

        private bool activeInfoPanel;
        private bool esMedico;

        void Start()
        {
            //si hay un paciente loggeado, vamos al mapa
            if (ConectToDatabase.Instance.isLogged())
            {
                Debug.Log("HAY UN USUARIO LOGGEADO");
                establecerUsuario();
            }
            else
            {
                Debug.Log("NOOOOO HAY USUARIO");
                mapa.SetActive(false);
                seleccionUsuario.SetActive(true);
            }

            infoPanel.SetActive(false);
            activeInfoPanel = false;
            mapa.SetActive(false);

            btnPaciente.onClick.AddListener(delegate { marcarEsMedico(false); });
            btnMedico.onClick.AddListener(delegate { marcarEsMedico(true); });
            btnAyuda.onClick.AddListener(toggleHelp);
            btnLogin.onClick.AddListener(awaitLogin);
            btnCreateUser.onClick.AddListener(createNewUser);
        }

        private async void establecerUsuario()
        {
            await ConectToDatabase.Instance.obtenerUsuario();
            mapa.SetActive(true);
            seleccionUsuario.SetActive(false);
            if (ConectToDatabase.Instance.getEsMedico())
            {
                ConectToDatabase.Instance.getCurrentMedico().printValues();

            }
            else
            {
                ConectToDatabase.Instance.getCurrentPaciente().printValues();
            }
        }

        public void toggleHelp()
        {
            activeInfoPanel = !activeInfoPanel;

            infoPanel.SetActive(activeInfoPanel);
        }

        public void marcarEsMedico(bool opcion)
        {
            esMedico = opcion;
            Debug.Log("esMedico: " + esMedico);
            //cambiamos los textos que correpondan
            if (esMedico)
            {
                msgNuevoUsuario.text = "¿Nuevo psicólogo?";
                msgTituloCrearUsuario.text = "Crear psicólogo";
            }
            else
            {
                msgNuevoUsuario.text = "¿Nuevo paciente?";
                msgTituloCrearUsuario.text = "Crear paciente";
            }

            seleccionUsuario.SetActive(false);
            login.SetActive(true);
        }

        private void OnDestroy()
        {
            btnMedico.onClick.RemoveAllListeners();
            btnPaciente.onClick.RemoveAllListeners();
            btnAyuda.onClick.RemoveListener(toggleHelp);
            btnLogin.onClick.RemoveListener(awaitLogin);
            btnCreateUser.onClick.RemoveListener(createNewUser);
        }

        public async void awaitLogin()
        {
            string email = loginInputEmail.text;
            string password = loginInputPwd.text;
            
            ConectToDatabase.Instance.LogOut();

            btnLogin.interactable = false;
            //comprobar si el usuario actual está
            if(esMedico)
            {
                await ConectToDatabase.Instance.LoginMedico(email, password);
            }
            else
            {
                await ConectToDatabase.Instance.LoginPaciente(email, password);
            }
            btnLogin.interactable = true;

            if (ConectToDatabase.Instance.getEsMedico())
            {
                Debug.Log("COMO ES MÉDICO NO VA A IR AL MAPA, TENDRÍA QUE IR A OTRA PANTALLA");
            }
            else
            {
                //ES FALSO, NO ESPERA A QUE HAGA EL AWAIT
                if (ConectToDatabase.Instance.isLogged())
                {
                    login.SetActive(false);
                    mapa.SetActive(true);
                    scriptMapa.establecerNombrePaciente(ConectToDatabase.Instance.getCurrentPaciente().nombre);
                }
            }
        }

        public async void createNewUser()
        {
            bool emailCorrect = false;
            bool pwdCorrect = false;

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

                Debug.Log("Creando nuevo usuario...");
                await ConectToDatabase.Instance.CreateUser(email, password, nombre, apellidos, esMedico);

                //después de registrarnos, nos loggeamos
                ConectToDatabase.Instance.LogOut();
                btnCreateUser.interactable = false;
                //comprobar si el usuario actual está
                if (esMedico)
                {
                    await ConectToDatabase.Instance.LoginMedico(email, password);
                }
                else
                {
                    await ConectToDatabase.Instance.LoginPaciente(email, password);
                }
                btnCreateUser.interactable = true;

                if (ConectToDatabase.Instance.isLogged())
                {
                    createUser.SetActive(false);
                    mapa.SetActive(true);
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

            createInputEmail.text = "";
            createInputPwd.text = "";
            createInputNombre.text = "";
            createInputApellidos.text = "";
        }

}
}
