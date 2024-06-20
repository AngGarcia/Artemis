using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static SceneChanger;

namespace General
{
    public class MapaPrincipal : MonoBehaviour
    {
        [SerializeField] private GameObject infoPanel;
        [SerializeField] private GameObject fondoPausa;

        [SerializeField] private TMP_Text nombrePaciente;
        [SerializeField] private GameObject seleccionUser;
        [SerializeField] private GameObject mapa;
        [SerializeField] private MainMenuManager menuManager;
        [Header("BOTONES")]
        [SerializeField] private Button btnAyuda;
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

        private bool activeInfoPanel;
        private bool pausar;
        private Sprite spriteBtnAyudaPressed;
        private Sprite spriteBtnAyudaNormal;

        //En Awake, comprobar datos de guardado y activar los botones de nivel segun cuales esten desbloqueados

        private void OnEnable()
        {
            nombrePaciente.text = ConectToDatabase.Instance.getCurrentPaciente().nombre;
        }

        void Start()
        {
            pausar = false;
            activeInfoPanel = false;
            infoPanel.SetActive(false);
            fondoPausa.SetActive(false);
            spriteBtnAyudaNormal = btnAyuda.GetComponent<Image>().sprite;
            spriteBtnAyudaPressed = btnAyuda.GetComponent<Button>().spriteState.disabledSprite;


            btnStart.onClick.AddListener(delegate { SceneChanger.Instance.GoToScene(Scenes.LibroInteractivo); });
            btnRespiracion.onClick.AddListener(delegate { SceneChanger.Instance.GoToScene(Scenes.Respiracion); });
            btnHarmonyHeaven.onClick.AddListener(goToHarmonyHeaven);
            btnRitmoVegetal.onClick.AddListener(delegate { SceneChanger.Instance.GoToScene(Scenes.RitmoVegetal); });
            btnPaisajeSonoro.onClick.AddListener(goToPaisajeSonoro);
            btnMelodiaFloral.onClick.AddListener(goToMelodiaFloral);
            btnPopUpLogOut.onClick.AddListener(LogOut);
            btnPopUpSave.onClick.AddListener(SaveData);
            btnAyuda.onClick.AddListener(toggleHelp);
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
            btnAyuda.onClick.RemoveListener(toggleHelp);
        }

        public void pausarJuego()
        {
            pausar = !pausar;
            fondoPausa.SetActive(pausar);

            if (pausar)
            {
                Time.timeScale = 0;
            }
            else{

                Time.timeScale = 1;
            }
        }

        public void toggleHelp()
        {
            activeInfoPanel = !activeInfoPanel;
            infoPanel.SetActive(activeInfoPanel);

            if (activeInfoPanel)
            {
                btnAyuda.GetComponent<Image>().sprite = spriteBtnAyudaPressed;
            }
            else
            {
                btnAyuda.GetComponent<Image>().sprite = spriteBtnAyudaNormal;
            }
        }

        private void LogOut()
        {
            //reanudamos el juego
            pausarJuego();

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