using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public int scoreValue = 0;

    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Incrementa el contador de puntos o realiza cualquier otra acción necesaria
            GameManager2.instance.AddScore(scoreValue);


            // Destruye el objeto pick-up
            Destroy(gameObject);
        }
    }
}
