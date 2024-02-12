using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputManagerEntry;

public class seedMovement : MonoBehaviour
{

    private Vector2 direction;
    private Vector2 mousePosDown;
    private Vector2 mousePosUp;

    [SerializeField]
    private ConstantForce2D force;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosDown = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mousePosUp = Input.mousePosition;

            direction = mousePosUp - mousePosDown;
            Debug.Log("Direction: " + direction);

            Vector2 variation = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-50.0f, 50.0f));
            Debug.Log("Variation: " + variation);

            Vector2 finalDirection = direction + variation;

            force.enabled = true;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().AddForce(finalDirection * 50 * Time.deltaTime);
        }
    }
}
