using FMODUnity;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PaisajeSonoro
{

    [RequireComponent(typeof(Image))]
    public class SelectInstrumento : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, 
        IPointerExitHandler, IDeselectHandler
    {
        public enum Generos
        {
            Classical, Latin, PopRock, Electronic
        }

        [SerializeField] private CanvasManager canvasManager;
        [SerializeField] private GameObject panel;
        [SerializeField] private Button closeButton;
        [SerializeField] private GameObject item;
        [Space]
        [SerializeField] private Generos genero;
        [SerializeField] private string paramName;
        [SerializeField] private StudioEventEmitter studioEventEmitter;
        [Space]
        [SerializeField] private List<Sprite> ImageList;

        private Transform spawnParent;
        private Image image;

        private bool hover = false;
        private bool visible = false;

        private List<Toggle> toggles = new List<Toggle>();
        private List<string> options = new List<string>();

        void Start()
        {
            image = GetComponent<Image>();
            spawnParent = item.transform.parent;

            int i = 0;
            foreach (var img in ImageList)
            {
                i++;
                var newItem = Instantiate(item);
                newItem.transform.SetParent(spawnParent, false);

                var text = newItem.GetComponentInChildren<TMP_Text>();
                if (text != null)
                {
                    text.text = "Opcion " + i;
                    options.Add("Option " + i);
                }

                var toggle = newItem.GetComponentInChildren<Toggle>();
                if (toggle != null)
                {
                    toggle.onValueChanged.AddListener(ToggleValueChanged);
                    toggles.Add(toggle);
                }
            }

            image.sprite = ImageList[0];
            item.SetActive(false);

            panel.SetActive(false);
            visible = false;

            switch (genero)
            {
                case Generos.Classical:
                    canvasManager.OnClassical.AddListener(OnSelected);
                    break;
                case Generos.Latin:
                    canvasManager.OnLatin.AddListener(OnSelected);
                    break;
                case Generos.PopRock:
                    canvasManager.OnPopRock.AddListener(OnSelected);
                    break;
                case Generos.Electronic:
                    canvasManager.OnElectronic.AddListener(OnSelected);
                    break;
            }

            closeButton.onClick.AddListener(ClosePanel);
        }

        private void OnDestroy()
        {
            foreach (var t in toggles)
            {
                t.onValueChanged.RemoveListener(ToggleValueChanged);
            }

            switch (genero)
            {
                case Generos.Classical:
                    canvasManager.OnClassical.RemoveListener(OnSelected);
                    break;
                case Generos.Latin:
                    canvasManager.OnLatin.RemoveListener(OnSelected);
                    break;
                case Generos.PopRock:
                    canvasManager.OnPopRock.RemoveListener(OnSelected);
                    break;
                case Generos.Electronic:
                    canvasManager.OnElectronic.RemoveListener(OnSelected);
                    break;
            }

            closeButton.onClick.RemoveListener(ClosePanel);
        }

        private void OnSelected(bool active)
        {
            if (active)
            {
                studioEventEmitter.EventInstance.setParameterByNameWithLabel(paramName, options[0]);
            }
            else
            {
                studioEventEmitter.EventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }

        private void ToggleValueChanged(bool _)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);

            for (int i = 0; i < toggles.Count; i++)
            {
                if (toggles[i].isOn)
                {
                    image.sprite = ImageList[i];

                    studioEventEmitter.EventInstance.setParameterByNameWithLabel(paramName, options[i]);
                }
            }
        }

        private void ClosePanel()
        {
            panel.SetActive(false);
            visible = false;
        }

        private void TogglePanelVisibility()
        {
            visible = !visible;

            panel.SetActive(visible);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            TogglePanelVisibility();

            if (visible)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            hover = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            hover = false;
        }

        public void OnDeselect(BaseEventData eventData)
        {
            if (!hover)
            {
                panel.SetActive(false);
                visible = false;
            }
        }
    }
}

