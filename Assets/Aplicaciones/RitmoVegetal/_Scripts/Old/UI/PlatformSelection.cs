using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlatformSelection : MouseHovering, IPointerEnterHandler, IPointerExitHandler
{
    public override void OnPointerEnter(PointerEventData eventData)
    {
        GUIManager.Instance.platformID = gameObject.tag;
        GUIManager.Instance.canPick = true;
    }

    public override void OnPointerExit(PointerEventData eventData) 
    {
        GUIManager.Instance.platformID = null;
        GUIManager.Instance.canPick = false;
    }
}
