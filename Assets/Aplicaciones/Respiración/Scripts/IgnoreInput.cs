using UnityEngine;
using UnityEngine.EventSystems;

public class IgnoreInput : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector] public bool Interactable = true;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Interactable = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Interactable = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Interactable = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Interactable = true;
    }
}
