using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace General
{
    public class LogInMenuManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject infoPanel;
        [SerializeField] private GameObject seleccionUsuario;
        [SerializeField] private GameObject login;
        [SerializeField] private GameObject mapa;

        [Header("BOTONES")]
        [SerializeField] Button btnPaciente;
        [SerializeField] Button btnMedico;
        [SerializeField] Button btnAyuda;
        [SerializeField]
        private Button btnLogin;

        [Header("MSG ERROR CREAR")]
        [SerializeField]
        private TMP_Text msgErrorPaciente;
        [SerializeField]
        private TMP_Text msgErrorMedico;

        //INPUTS DE LOS LOGINS
        [Header("INPUTS DE LOS LOGINS")]
       /* [SerializeField]
        private TMP_InputField loginInputEmailPaciente;
        [SerializeField]
        private TMP_InputField loginInputPwdPaciente;
        [SerializeField]
        private TMP_InputField loginInputEmailMedico;
        [SerializeField]
        private TMP_InputField loginInputPwdMedico;*/
        [SerializeField]
        private TMP_InputField loginInputEmail;
        [SerializeField]
        private TMP_InputField loginInputPwd;

        //INPUTS DE LOS CREATES
        [Header("INPUTS DE LOS CREATES")]
        [SerializeField]
        private TMP_InputField createInputEmailPaciente;
        [SerializeField]
        private TMP_InputField createInputPwdPaciente;
        [SerializeField]
        private TMP_InputField createInputNombrePaciente;
        [SerializeField]
        private TMP_InputField createInputApellidosPaciente;

        [SerializeField]
        private TMP_InputField createInputEmailMedico;
        [SerializeField]
        private TMP_InputField createInputPwdMedico;
        [SerializeField]
        private TMP_InputField createInputNombreMedico;
        [SerializeField]
        private TMP_InputField createInputApellidosMedico;


        private bool activeInfoPanel;
        private bool esMedico;

        void Start()
        {
            infoPanel.SetActive(false);
            activeInfoPanel = false;
            mapa.SetActive(false);

            btnPaciente.onClick.AddListener(delegate { marcarEsMedico(false); });
            btnMedico.onClick.AddListener(delegate { marcarEsMedico(true); });
            btnAyuda.onClick.AddListener(toggleHelp);
            btnLogin.onClick.AddListener(awaitLogin);
        }
        public void toggleHelp()
        {
            activeInfoPanel = !activeInfoPanel;

            infoPanel.SetActive(activeInfoPanel);
        }

        public void marcarEsMedico(bool opcion)
        {
            seleccionUsuario.SetActive(false);
            login.SetActive(true);

            esMedico = opcion;
            Debug.Log("esMedico: " + esMedico);
        }

        private void OnDestroy()
        {
            btnMedico.onClick.RemoveAllListeners();
            btnPaciente.onClick.RemoveAllListeners();
            btnAyuda.onClick.RemoveListener(toggleHelp);
            btnLogin.onClick.RemoveListener(awaitLogin);
        }

        //public void LogInPaciente()
        //{
        //    /*string email = loginInputEmailPaciente.text;
        //    string password = loginInputPwdPaciente.text;*/
            

        //    await 
        //}

        //public void LogInMedico()
        //{
        //    /*string email = loginInputEmailMedico.text;
        //    string password = loginInputPwdMedico.text;*/

        //    string email = loginInputEmail.text;
        //    string password = loginInputPwd.text;

        //    awaitLogin();
        //}

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

            if (ConectToDatabase.Instance.isLogged())
            {
                login.SetActive(false);
                mapa.SetActive(true);
            }
        }


        public void CreateNewPaciente()
        {
            bool emailCorrect = false;
            bool pwdCorrect = false;

            //primero vemos que sea, efectivamente, un email
            if (createInputEmailPaciente.text.Contains("@") && ((createInputEmailPaciente.text.Contains(".com") || createInputEmailPaciente.text.Contains(".es"))))
            {
                emailCorrect = true;
            }
            else
            {
                Debug.Log("ERROR: email incorrecto");
                msgErrorPaciente.text = "ERROR: email incorrecto.";
            }

            //después comprobamos que la contraseña sea correcta
            if (createInputPwdPaciente.text.Length >= 6)
            {
                pwdCorrect = true;
            }
            else
            {
                Debug.Log("ERROR: la contraseña debe tener mínimo 6 caracteres.");
                msgErrorPaciente.text = "ERROR: la contraseña debe tener mínimo 6 caracteres.";
            }

            if (emailCorrect && pwdCorrect)
            {
                string email = createInputEmailPaciente.text;
                string password = createInputPwdPaciente.text;
                string nombre = createInputNombrePaciente.text;
                string apellidos = createInputApellidosPaciente.text;

                Debug.Log("Creando nuevo paciente...");
                ConectToDatabase.Instance.CreateUser(email, password, nombre, apellidos, false);
            }

        }

        public void CreateNewMedico()
        {
            bool emailCorrect = false;
            bool pwdCorrect = false;

            //primero vemos que sea, efectivamente, un email
            if (createInputEmailMedico.text.Contains("@") && ((createInputEmailMedico.text.Contains(".com") || createInputEmailMedico.text.Contains(".es"))))
            {
                emailCorrect = true;
            }
            else
            {
                Debug.Log("ERROR: email incorrecto");
                msgErrorMedico.text = "ERROR: email incorrecto.";
            }

            //después comprobamos que la contraseña sea correcta
            if (createInputPwdMedico.text.Length >= 6)
            {
                pwdCorrect = true;
            }
            else
            {
                Debug.Log("ERROR: la contraseña debe tener mínimo 6 caracteres.");
                msgErrorMedico.text = "ERROR: la contraseña debe tener mínimo 6 caracteres.";
            }

            if (emailCorrect && pwdCorrect)
            {
                string email = createInputEmailMedico.text;
                string password = createInputPwdMedico.text;
                string nombre = createInputNombreMedico.text;
                string apellidos = createInputApellidosMedico.text;

                Debug.Log("Creando nuevo psicólogo...");
                ConectToDatabase.Instance.CreateUser(email, password, nombre, apellidos, true);
            }
           
        }

    }
}
