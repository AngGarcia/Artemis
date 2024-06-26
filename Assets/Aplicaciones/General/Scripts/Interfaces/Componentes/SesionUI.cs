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

        private Sesion sesion;

        void Start()
        {
            btnOpenModal.onClick.AddListener(openModal);
        }

        private void OnDestroy()
        {
            btnOpenModal.onClick.RemoveListener(openModal);
        }

        public void setData(GameObject modalSesion, Sesion sesionActual, int numSesion)
        {
            modal = modalSesion;
            sesion = sesionActual;
            fecha.text = sesion.getFecha();
            this.numSesion.text = numSesion.ToString();
            ConectToDatabase.Instance.setCurrentSesion(sesion);
        }

        private void openModal()
        {
            modal.GetComponent<SesionModal>().openModal(sesion, numSesion.text);
        }
    }
}
