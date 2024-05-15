using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prueba : MonoBehaviour
{
    // Start is called before the first frame update
    public Button boton;

    [ContextMenu("botonClick")]
    public void botonClick()
    {
        boton.onClick.Invoke();
    }
}
