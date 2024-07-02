using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : InteractableObject, IRotational
{
    private void Update()
    {
        if (followMovement) { Rotate(); }
    }

    public void Rotate() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDirection = mousePosition - transform.position;
        float mouseAngle = Vector2.SignedAngle(Vector2.up, mouseDirection);

        transform.eulerAngles = new Vector3(0, 0, mouseAngle);

        Touch t = Input.GetTouch(0);
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(t.position);
        Vector2 touchDirection = touchPos - transform.position;
        float touchAngle = Vector2.SignedAngle(Vector2.up, touchDirection);

        transform.eulerAngles = new Vector3(0, 0, touchAngle);
    }
}