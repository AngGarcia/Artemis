using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectSelection : MouseHovering
{
    private void Update()
    {
        if (Input.GetMouseButtonUp(0)) { gameObject.GetComponent<InteractableObject>().followMovement = false; }
    }

    public override void OnMouseOver()
    {
        if (gameObject.GetComponent<InteractableObject>() != null && Input.GetMouseButtonDown(0)) { gameObject.GetComponent<InteractableObject>().followMovement = true; }
    }
}
