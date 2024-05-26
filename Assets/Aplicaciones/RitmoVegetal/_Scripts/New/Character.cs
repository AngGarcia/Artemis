using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D cmpRb;

    private void Awake() { cmpRb = GetComponent<Rigidbody2D>(); }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, cmpRb.velocity);
    }
}
