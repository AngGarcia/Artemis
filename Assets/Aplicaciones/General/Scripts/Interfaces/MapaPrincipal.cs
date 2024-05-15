using UnityEngine;
using UnityEngine.UI;
using static SceneChanger;

namespace General
{
    public class MapaPrincipal : MonoBehaviour
    {
        [Header("BOTONES")]
        [SerializeField] private Button btnPopUpLogOut;
        [SerializeField] private Button btnPopUpSave;
        [SerializeField] private Button btnStart;
        [SerializeField] private Button btnRespiracion;
        [SerializeField] private Button btnHarmonyHeaven;
        [SerializeField] private Button btnRitmoVegetal;
        [SerializeField] private Button btnPaisajeSonoro;
        [SerializeField] private Button btnMelodiaFloral;

        //En Awake, comprobar datos de guardado y activar los botones de nivel segun cuales esten desbloqueados
        //Faltarian asignar las funciones de logout y save

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
    }
}