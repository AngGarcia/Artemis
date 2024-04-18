using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : TemporalSingleton<GameManager>
{
    public bool gameStarted;
    public bool gameMode;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameStarted)
        {
            gameStarted = true;
        }
    }
}
