using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cadenced : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject character;

    [Header("Spawner Settings")]
    [SerializeField] private float spawnTime  = 0;
    [SerializeField] private float startDelay = 0;
                     private bool  delayDone  = false;
                     private float spawnTimer = 0;

    private void Awake()
    {
        spawnTimer = spawnTime;
    }

    private void Update()
    {
        // if (GameManager.Instance.gameStarted)
        // {
        //     if (!delayDone) startDelay -= Time.deltaTime;
        // 
        //     if (startDelay <= 0) 
        //     {
        //         if (!delayDone) Spawn();
        //         delayDone = true;
        // 
        //         spawnTime -= Time.deltaTime;
        //         if (spawnTime <= 0)
        //         {
        //             Spawn();
        //             spawnTime = spawnTimer;
        //         }
        //     }
        // }
    }

    private void Spawn()
    {
        Instantiate(character, gameObject.transform.position, Quaternion.identity);
    }
}
