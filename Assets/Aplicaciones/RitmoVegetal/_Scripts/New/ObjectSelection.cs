using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectSelection : MouseHovering
{
    private void Update()
    {
        if (Input.GetMouseButtonUp(0)) { gameObject.GetComponent<InteractableObject>().followMovement = false; }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) { gameObject.GetComponent<InteractableObject>().followMovement = false; }
    }

    public override void OnMouseOver()
    {
        if (gameObject.GetComponent<InteractableObject>() != null && Input.GetMouseButtonDown(0)) { gameObject.GetComponent<InteractableObject>().followMovement = true; }
        if (gameObject.GetComponent<InteractableObject>() != null && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) { gameObject.GetComponent<InteractableObject>().followMovement = true; }
    }
}
