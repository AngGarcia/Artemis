using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlatformList : MouseHovering, IPointerEnterHandler, IPointerExitHandler   
{
    public override void OnPointerEnter(PointerEventData eventData) 
    { 
        base.OnPointerEnter(eventData);

        GUIManager.Instance.OpenMenu(true);
    }

    public override void OnPointerExit(PointerEventData eventData) 
    {
        base.OnPointerEnter(eventData);

        GUIManager.Instance.OpenMenu(false);
    }
}
