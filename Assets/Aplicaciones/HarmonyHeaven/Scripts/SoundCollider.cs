using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCollider : MonoBehaviour
{
    public AudioSource audioSource; // Referencia al componente AudioSource que contiene el sonido
    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Reproduce el sonido
            audioSource.Play();
        }
    }
}
