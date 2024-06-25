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
        [SerializeField] private Button btnStart;
        [SerializeField] private Button btnRespiracion;
        [SerializeField] private Button btnHarmonyHeaven;
        [SerializeField] private Button btnRitmoVegetal;
        [SerializeField] private Button btnPaisajeSonoro;
        [SerializeField] private Button btnMelodiaFloral;


        //En Awake, comprobar datos de guardado y activar los botones de nivel segun cuales esten desbloqueados

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
        }

        private void OnDestroy()
        {
            btnStart.onClick.RemoveListener(delegate { SceneChanger.Instance.GoToScene(Scenes.LibroInteractivo); });
            btnRespiracion.onClick.RemoveListener(delegate { SceneChanger.Instance.GoToScene(Scenes.Respiracion); });
            btnHarmonyHeaven.onClick.RemoveListener(goToHarmonyHeaven);
            btnRitmoVegetal.onClick.RemoveListener(delegate { SceneChanger.Instance.GoToScene(Scenes.RitmoVegetal); });
            btnPaisajeSonoro.onClick.RemoveListener(goToPaisajeSonoro);
            btnMelodiaFloral.onClick.RemoveListener(goToMelodiaFloral);
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