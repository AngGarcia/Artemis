using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static SceneChanger;

namespace General
{
    public class MapaPrincipal : MonoBehaviour
    {

        [Header("INTERFACES")]
        [SerializeField] private GameObject inicioPaciente;
        [SerializeField] private GameObject mapa;

        [Header("BOTONES")]
        [SerializeField] private Button btnBack;
        [SerializeField] private Button btnStart;
        [SerializeField] private Button btnRespiracion;
        [SerializeField] private Button btnHarmonyHeaven;
        [SerializeField] private Button btnRitmoVegetal;
        [SerializeField] private Button btnPaisajeSonoro;
        [SerializeField] private Button btnMelodiaFloral;


        void Start()
        {
            btnBack.onClick.AddListener(goBack);
            btnStart.onClick.AddListener(delegate { SceneChanger.Instance.GoToScene(Scenes.LibroInteractivo); });
            btnRespiracion.onClick.AddListener(delegate { SceneChanger.Instance.GoToScene(Scenes.Respiracion); });
            btnHarmonyHeaven.onClick.AddListener(delegate { SceneChanger.Instance.GoToScene(Scenes.HarmonyHeaven); });
            btnRitmoVegetal.onClick.AddListener(delegate { SceneChanger.Instance.GoToScene(Scenes.RitmoVegetal); });
            btnPaisajeSonoro.onClick.AddListener(delegate { SceneChanger.Instance.GoToScene(Scenes.PaisajeSonoro); });
            btnMelodiaFloral.onClick.AddListener(delegate { SceneChanger.Instance.GoToScene(Scenes.MelodiaFloral); });
        }

        private void OnDestroy()
        {
            btnBack.onClick.RemoveListener(goBack);
            btnStart.onClick.RemoveListener(delegate { SceneChanger.Instance.GoToScene(Scenes.LibroInteractivo); });
            btnRespiracion.onClick.RemoveListener(delegate { SceneChanger.Instance.GoToScene(Scenes.Respiracion); });
            btnHarmonyHeaven.onClick.RemoveListener(delegate { SceneChanger.Instance.GoToScene(Scenes.HarmonyHeaven); });
            btnRitmoVegetal.onClick.RemoveListener(delegate { SceneChanger.Instance.GoToScene(Scenes.RitmoVegetal); });
            btnPaisajeSonoro.onClick.RemoveListener(delegate { SceneChanger.Instance.GoToScene(Scenes.PaisajeSonoro); });
            btnMelodiaFloral.onClick.RemoveListener(delegate { SceneChanger.Instance.GoToScene(Scenes.MelodiaFloral); });
        }
        
        private void goBack()
        {
            inicioPaciente.SetActive(true);
            ConectToDatabase.Instance.stopTimeSesion();
            //await ConectToDatabase.Instance.SaveDataPaciente();
            mapa.SetActive(false);
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
    }
}