using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : TemporalSingleton<GameManager>
{
    [Header("Cannon")]
    [SerializeField] private Transform cannonTransform;
    [SerializeField] private float cannonForce;

    public void ResetTransform(GameObject gameObject) {
        gameObject.transform.position = cannonTransform.position;
        gameObject.transform.rotation = cannonTransform.rotation;

        if (gameObject.GetComponent<Rigidbody2D>() != null) { Launch(gameObject.GetComponent<Rigidbody2D>()); }
    }

    public void Launch(Rigidbody2D cmpRb) {
        Vector2 velocity2D = cmpRb.velocity;
        float angle = Mathf.Atan2(velocity2D.y, velocity2D.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        cmpRb.AddForce(cannonTransform.forward * cannonForce, ForceMode2D.Impulse);
    }
}