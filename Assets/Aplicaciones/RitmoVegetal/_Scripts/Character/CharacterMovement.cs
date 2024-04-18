using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D cmpRb;
    private SpriteRenderer cmpSR;

    [Header("Character")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private bool orientRight;
    
    private void Start()
    {
        cmpRb = GetComponent<Rigidbody2D>();
        cmpSR = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Movement();
    }

    private void Movement()
    {
        if (orientRight)
        {
            cmpSR.flipX = false;
            cmpRb.velocity = new Vector2(movementSpeed, cmpRb.velocity.y);
        }
        else
        {
            cmpSR.flipX = true;
            cmpRb.velocity = new Vector2(-movementSpeed, cmpRb.velocity.y);
        }
    }

    private void SwapOrientation()
    {
        orientRight = !orientRight;
    }
}
