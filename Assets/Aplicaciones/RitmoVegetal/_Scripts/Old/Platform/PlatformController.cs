using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private float bounceForce;
    
    public bool cooldown = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            if (!cooldown)
            {
                cooldown = true;
                StartCoroutine(CooldownTimer());

                if (collision.gameObject.GetComponentInChildren<ParticleSystem>() != null) { collision.gameObject.GetComponentInChildren<ParticleSystem>().Play(); }

                if (collision.gameObject.transform.position.y < transform.position.y) { collision.gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up * bounceForce); }
                else { collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * bounceForce); }
            }
        }
    }

    IEnumerator CooldownTimer() {
        yield return new WaitForSeconds(0.3f);
        cooldown = false;
    }
}
