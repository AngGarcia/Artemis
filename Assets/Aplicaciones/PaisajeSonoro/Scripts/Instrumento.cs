using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PaisajeSonoro
{
    public class Instrumento : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        public float maxTiempo = 1f;
        public GameObject panel;
        public AudioClip[] melodias;

        private float segundos;
        private bool isPressed;
        private AudioSource melodia;


        public void Start()
        {
            melodia = GetComponent<AudioSource>();
            isPressed = false;
            segundos = 0;
        }

        public void Update()
        {
            segundos += Time.deltaTime;

            if (segundos >= maxTiempo && isPressed)
            {
                panel.SetActive(true);
            }
        }

        public void OnPointerClick(PointerEventData eventData) { }

        public void OnPointerDown(PointerEventData eventData)
        {
            segundos = 0;
            isPressed = true;

            if (melodia.isPlaying)
            {
                melodia.Stop();
            }
            else
            {
                melodia.Play();
            }
        }

        public void OnPointerEnter(PointerEventData eventData) { }

        public void OnPointerExit(PointerEventData eventData) { }

        public void OnPointerUp(PointerEventData eventData)
        {
            isPressed = false;
        }

        public void establecerMelodia(int num)
        {
            melodia.clip = melodias[num];
            melodia.Play();
        }

    }
}
