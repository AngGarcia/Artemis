using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace General
{
    public class SesionUI : MonoBehaviour
    {
        [SerializeField] private Button btnOpenModal;
        [SerializeField] private GameObject modal;

        [SerializeField] private TMP_Text numSesion;
        [SerializeField] private TMP_Text fecha;

        void Start()
        {
            btnOpenModal.onClick.AddListener(openModal);
        }

        private void OnDestroy()
        {
            btnOpenModal.onClick.RemoveListener(openModal);
        }

        public void setData(GameObject modalSesion, string fecha, int numSesion)
        {
            modal = modalSesion;
            this.fecha.text = fecha;
            this.numSesion.text = numSesion.ToString();
        }

        private void openModal()
        {
            modal.SetActive(true);
        }
    }
}
