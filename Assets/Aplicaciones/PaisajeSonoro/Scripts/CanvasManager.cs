using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaisajeSonoro
{
    public class CanvasManager : MonoBehaviour
    {
        public Camera camara;
        //public MusicManager manager;

        public void OnBackButtonPressed()
        {
            //manager.StopBackgroundMusic();
            camara.transform.position = new Vector3(0f, 0f, -10f);
            camara.backgroundColor = new Color(62f / 255f, 16f / 255f, 144f / 255f, 1);
        }

        public void OnClassicalSelected()
        {
            camara.transform.position = new Vector3(20, 0f, -10);
            camara.backgroundColor = new Color(234f / 255f, 150f / 255f, 67f / 255f, 1);
        }

        public void OnLatinSelected()
        {
            camara.transform.position = new Vector3(40, 0f, -10);
            camara.backgroundColor = new Color(190f / 255f, 92f / 255f, 89f / 255f, 1);
        }

        public void OnPopRockSelected()
        {
            camara.transform.position = new Vector3(60, 0f, -10);
            camara.backgroundColor = new Color(127f / 255f, 46f / 255f, 116f / 255f, 1);
        }

        public void OnElectronicSelected()
        {
            camara.transform.position = new Vector3(80, 0f, -10);
            camara.backgroundColor = new Color(88f / 255f, 86f / 255f, 218f / 255f, 1);
        }
    }
}

