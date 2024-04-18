using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    private void Start()
    {
        Grid grid = new Grid(20, 20, 5);
    }
}
