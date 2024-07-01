using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : TemporalSingleton<GameManager>
{
    [Header("Cannon")]
    [SerializeField] private Transform cannonTransform;
    [SerializeField] private float cannonForce;

    [SerializeField] GameObject temporalCharacter;

    private FMOD.Studio.EventInstance instance;

    public void Start()
    {
        ResetTransform(temporalCharacter);

        instance = FMODUnity.RuntimeManager.CreateInstance("event:/02RitmoVegetalApp3/MelodiaRelajante");
        instance.start();
    }

    public void ResetTransform(GameObject gameObject) {
        instance.setParameterByName("RitmoVegetalGameStates", Random.Range(0, 3));

        gameObject.transform.position = cannonTransform.position;
        gameObject.transform.rotation = cannonTransform.rotation;

        if (gameObject.GetComponent<Rigidbody2D>() != null) { 
            ResetGravity(gameObject.GetComponent<Rigidbody2D>());
            Launch(gameObject.GetComponent<Rigidbody2D>()); 
        }
    }
        
    private void ResetGravity(Rigidbody2D cmpRb) { cmpRb.velocity = Vector3.zero; }

    private void Launch(Rigidbody2D cmpRb) { 
        cmpRb.AddForce(cannonTransform.up * cannonForce, ForceMode2D.Impulse);
        FMODUnity.RuntimeManager.PlayOneShot("event:/02RitmoVegetalApp3/CannonShoot");
    }
}