using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TraslationalPlatform : Platform, ITranslational
{
    [SerializeField] int axis;

    private void Update()
    {
        if (followMovement) {
            if (axis == 0) { TranslateX(); } 
            else if (axis == 1) { TranslateY(); } 
            else if (axis == 2) { Translate(); } 
        }
    }

    public void Translate() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePosition.x, mousePosition.y);
    }
    public void TranslateX() { 
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePosition.x, transform.position.y);
    }
    public void TranslateY() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(transform.position.x, mousePosition.y);
    }
}