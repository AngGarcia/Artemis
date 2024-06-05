using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private float bounceForce;
    
    public bool placed = true;

    private void Update()
    {
        if (GUIManager.Instance.followMouse && !placed)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 positionSnap = new Vector3(Mathf.Floor(position.x), Mathf.Floor(position.y));
            transform.position = positionSnap;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            if (collision.gameObject.GetComponentInChildren<ParticleSystem>() != null) { collision.gameObject.GetComponentInChildren<ParticleSystem>().Play(); }

            if (collision.gameObject.transform.position.y < transform.position.y) { collision.gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up * bounceForce); }
            else { collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * bounceForce); }
        }
    }

    private void OnMouseOver()
    {
        // if (placed && !GameManager.Instance.gameMode)
        // {
        //     GUIManager.Instance.canPick = true;
        //     GUIManager.Instance.platform = gameObject;
        //     GUIManager.Instance.PlacePlatform(false);
        // }
    }
}
