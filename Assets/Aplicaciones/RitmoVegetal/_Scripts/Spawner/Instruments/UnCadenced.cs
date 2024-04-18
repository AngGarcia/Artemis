using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnCadenced : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject character;

    [Header("Spawner Settings")]
    [SerializeField] private float spawnTime  = 0;
    // [SerializeField] private float startDelay = 0;
    [SerializeField] private float innerDelay = 0;
                     // private bool  delayDone  = false;
                     private bool  canSpawn   = true;
                     private float spawnTimer = 0;
                     private float innerTimer = 0;

    private void Awake()
    {
        spawnTimer = spawnTime;
        innerTimer = innerDelay;
    }

    private void Update()
    {
        if (GameManager.Instance.gameStarted)
        {
            spawnTime -= Time.deltaTime;
            if (spawnTime <= 0)
            {
                if (canSpawn)
                {
                    Spawn();
                    canSpawn = false;
                }
            
                innerDelay -= Time.deltaTime;
                if (innerDelay <= 0)
                {
                    Spawn();
            
                    spawnTime = spawnTimer;
                    innerDelay = innerTimer;
                    canSpawn = true;
                }
            }
        }
    }

    private void Spawn()
    {
        Instantiate(character, gameObject.transform.position, Quaternion.identity);
    }
}
