using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static SceneChanger;

namespace General
{
    public class MapaPrincipal : MonoBehaviour
    {
        [SerializeField] private TMP_Text nombrePaciente;
        [SerializeField] private GameObject seleccionUser;
        [SerializeField] private GameObject mapa;
        [SerializeField] private MainMenuManager menuManager;
        [Header("BOTONES")]
        [SerializeField] private Button btnPopUpLogOut;
        [SerializeField] private Button btnPopUpSave;
        [SerializeField] private Button btnStart;
        [SerializeField] private Button btnRespiracion;
        [SerializeField] private Button btnHarmonyHeaven;
        [SerializeField] private Button btnRitmoVegetal;
        [SerializeField] private Button btnPaisajeSonoro;
        [SerializeField] private Button btnMelodiaFloral;
        [Header("POP-UPS")]
        [SerializeField] private GameObject popLogOut;
        [SerializeField] private GameObject popSaveData;
        [SerializeField] private GameObject popDataSaved;


        //En Awake, comprobar datos de guardado y activar los botones de nivel segun cuales esten desbloqueados
        //Faltarian asignar las funciones de logout y save

        private void OnEnable()
        {
            nombrePaciente.text = ConectToDatabase.Instance.getCurrentPaciente().nombre;
        }

        void Start()
        {
            btnStart.onClick.AddListener(delegate { SceneChanger.Instance.GoToScene(Scenes.LibroInteractivo); });
            btnRespiracion.onClick.AddListener(delegate { SceneChanger.Instance.GoToScene(Scenes.Respiracion); });
            btnHarmonyHeaven.onClick.AddListener(goToHarmonyHeaven);
            btnRitmoVegetal.onClick.AddListener(delegate { SceneChanger.Instance.GoToScene(Scenes.RitmoVegetal); });
            btnPaisajeSonoro.onClick.AddListener(goToPaisajeSonoro);
            btnMelodiaFloral.onClick.AddListener(goToMelodiaFloral);
            btnPopUpLogOut.onClick.AddListener(LogOut);
            btnPopUpSave.onClick.AddListener(SaveData);
        }

        private void OnDestroy()
        {
            btnStart.onClick.RemoveListener(delegate { SceneChanger.Instance.GoToScene(Scenes.LibroInteractivo); });
            btnRespiracion.onClick.RemoveListener(delegate { SceneChanger.Instance.GoToScene(Scenes.Respiracion); });
            btnHarmonyHeaven.onClick.RemoveListener(goToHarmonyHeaven);
            btnRitmoVegetal.onClick.RemoveListener(delegate { SceneChanger.Instance.GoToScene(Scenes.RitmoVegetal); });
            btnPaisajeSonoro.onClick.RemoveListener(goToPaisajeSonoro);
            btnMelodiaFloral.onClick.RemoveListener(goToMelodiaFloral);
            btnPopUpLogOut.onClick.RemoveListener(LogOut);
            btnPopUpSave.onClick.RemoveListener(SaveData);
        }

        private void LogOut()
        {
            ConectToDatabase.Instance.LogOut();
            menuManager.resetInputs();
            seleccionUser.SetActive(true);
            mapa.SetActive(false);
            popLogOut.SetActive(false);
        }
        
        private async void SaveData()
        {
            //guardamos los datos
            await ConectToDatabase.Instance.SaveData();
            popSaveData.SetActive(false);
            popDataSaved.SetActive(true);

        }

        private void goToHarmonyHeaven()
        {
            SceneChanger.Instance.actualScene = Scenes.Respiracion;
            SceneChanger.Instance.GoToScene(Scenes.LibroInteractivo);
        }

        private void goToPaisajeSonoro()
        {
            SceneChanger.Instance.actualScene = Scenes.RitmoVegetal;
            SceneChanger.Instance.GoToScene(Scenes.LibroInteractivo);
        }

        private void goToMelodiaFloral()
        {
            SceneChanger.Instance.actualScene = Scenes.PaisajeSonoro;
            SceneChanger.Instance.GoToScene(Scenes.LibroInteractivo);
        }

        public void establecerNombrePaciente(string nombre)
        {
            Debug.Log("PONEMOS EL NOMBRE DEL PACIENTE: " + nombre);
            nombrePaciente.text = nombre;
        }
    }
}