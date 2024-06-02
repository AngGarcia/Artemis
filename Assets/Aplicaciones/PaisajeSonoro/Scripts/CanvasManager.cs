using UnityEngine;
using UnityEngine.Events;

namespace PaisajeSonoro
{
    public class CanvasManager : MonoBehaviour
    {
        public Camera camara;
        public GameObject ClassicEmitter, LatinEmitter, PopRockEmitter, ElectronicEmitter;

        public UnityEvent<bool> OnClassical;
        public UnityEvent<bool> OnLatin;
        public UnityEvent<bool> OnPopRock;
        public UnityEvent<bool> OnElectronic;

        public void OnBackButtonPressed()
        {
            camara.transform.position = new Vector3(0f, 0f, -10f);
            camara.backgroundColor = new Color(89f / 255f, 56f / 255f, 149f / 255f, 1);

            ClassicEmitter.SetActive(false);
            LatinEmitter.SetActive(false);
            PopRockEmitter.SetActive(false);
            ElectronicEmitter.SetActive(false);

            OnClassical.Invoke(false);
            OnLatin.Invoke(false);
            OnPopRock.Invoke(false);
            OnElectronic.Invoke(false);
        }

        public void OnClassicalSelected()
        {
            camara.transform.position = new Vector3(20, 0f, -10);
            camara.backgroundColor = new Color(201f / 255f, 159f / 255f, 78f / 255f, 1);

            ClassicEmitter.SetActive(true);
            OnClassical.Invoke(true);
        }

        public void OnLatinSelected()
        {
            camara.transform.position = new Vector3(40, 0f, -10);
            camara.backgroundColor = new Color(203f / 255f, 127f / 255f, 74f / 255f, 1);

            LatinEmitter.SetActive(true);
            OnLatin.Invoke(true);
        }

        public void OnPopRockSelected()
        {
            camara.transform.position = new Vector3(60, 0f, -10);
            camara.backgroundColor = new Color(183f / 255f, 86f / 255f, 159f / 255f, 1);

            PopRockEmitter.SetActive(true);
            OnPopRock.Invoke(true);
        }

        public void OnElectronicSelected()
        {
            camara.transform.position = new Vector3(80, 0f, -10);
            camara.backgroundColor = new Color(81f / 255f, 81f / 255f, 163f / 255f, 1);

            ElectronicEmitter.SetActive(true);
            OnElectronic.Invoke(true);
        }
    }
}

