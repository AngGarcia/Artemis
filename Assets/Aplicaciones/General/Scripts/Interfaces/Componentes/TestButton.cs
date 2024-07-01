using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace General
{
    public class TestButton : MonoBehaviour
    {
        [SerializeField] private GameObject interfazTest;
        [SerializeField] private DisplayTest testPsicometrico;
        [SerializeField] private TMP_Text nombrePacienteTest;

        [Space]

        [Header("BOTONES")]
        [SerializeField] private Button btnOpenTest;
        [SerializeField] private Button btnSaveTest;
        [SerializeField] private Button btnCloseTest;
        [Space]
        [SerializeField] private GameObject btnNextScene;

        void Start()
        {
            btnOpenTest.onClick.AddListener(openInterfazTest);
            btnCloseTest.onClick.AddListener(closeInterfazTest);
            btnSaveTest.onClick.AddListener(saveTestData);
        }

        private void OnDestroy()
        {
            btnOpenTest.onClick.RemoveListener(openInterfazTest);
            btnCloseTest.onClick.RemoveListener(closeInterfazTest);
            btnSaveTest.onClick.RemoveListener(saveTestData);
        }

        private void openInterfazTest()
        {
            nombrePacienteTest.text = ConectToDatabase.Instance.getCurrentPaciente().nombre + "?";
            Time.timeScale = 0;

            if (btnNextScene != null)
            {
                btnNextScene.SetActive(false);
            }
            interfazTest.SetActive(true);
        }

        private void closeInterfazTest()
        {
            if (btnNextScene != null)
            {
                btnNextScene.SetActive(true);
            }
            interfazTest.SetActive(false);
            Time.timeScale = 1;
        }

        private void saveTestData()
        {
            int valorTest = (int)testPsicometrico.getValue();
            int tiempoActualSesion = (int)ConectToDatabase.Instance.getCurrentTimeSesion();
            //Debug.Log("tiempoActualSesion: " + tiempoActualSesion);
            SceneChanger.Scenes escena = SceneChanger.Instance.actualScene;
            string momento;

            if (escena == SceneChanger.Scenes.MainMenu)
            {
                momento = "Mapa";
            }
            else
            {
                momento = escena.ToString();
            }

            //"momento" es de prueba, hay que detectar en qué escena está
            ConectToDatabase.Instance.getCurrentSesion().addNuevoEstado(momento, (EstadoPaciente)valorTest, tiempoActualSesion);
            ConectToDatabase.Instance.getCurrentSesion().printSesionValues();
        }
    }
}
