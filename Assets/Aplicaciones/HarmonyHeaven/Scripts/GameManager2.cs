using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 instance;
    public int score;
    public TextMeshProUGUI scoreText;
    public GameObject Tronco;
    private PickUp[] pickUps;
    private int pickUpsCount;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    
    private void Start()
    {
        pickUps = FindObjectsOfType<PickUp>();
        pickUpsCount = pickUps.Length;
    }

    private void QuitarTronco()
    {
        pickUpsCount--;
        if(pickUps != null && pickUpsCount == 0)
        {
            Tronco.SetActive(false);
        }
        
    }

    public void AddScore(int value)
    {
        score += value;
        // Actualiza el texto del contador en pantalla
        scoreText.text = "Semillas recogidas: " + score;
        QuitarTronco(); 
    }

    
}
