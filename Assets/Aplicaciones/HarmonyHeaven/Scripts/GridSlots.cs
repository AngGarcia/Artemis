using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridSlots : MonoBehaviour, IDropHandler
{
    private BooleanDienteLeon boolean;

    private void Start()
    {
        boolean = GetComponent<BooleanDienteLeon>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            draggableItem.parentAfterDrag = transform;
            boolean.ComprobarCorrecto();
        }
    }
}
