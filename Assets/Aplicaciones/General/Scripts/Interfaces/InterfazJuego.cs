using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SceneChanger;

public class InterfazJuego : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Botones")]
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject botonMapa;
    [SerializeField] private Button botonPopUpIrMapa;
    [SerializeField] private Button botonSiguiente;
    
    [Header("Elementos")]
    [SerializeField] private GameObject popMapa;
    [SerializeField] private GameObject juegoPausa;

    [Space]
    [SerializeField] private Scenes nextScene; 

    private bool btnPausaClicked;
    private bool btnMapaClicked;

    private Sprite spriteMapaPressed;
    private Sprite spriteMapaNormal;
    private Sprite spritePausaPressed;
    private Sprite spritePausaNormal;
    void Start()
    {
        juegoPausa.SetActive(false);
        popMapa.SetActive(false);
        btnMapaClicked = false;
        btnPausaClicked = false;

        spriteMapaNormal = botonMapa.GetComponent<Image>().sprite;
        spriteMapaPressed = botonMapa.GetComponent<Button>().spriteState.pressedSprite;

        spritePausaNormal = botonPausa.GetComponent<Image>().sprite;
        spritePausaPressed = botonPausa.GetComponent<Button>().spriteState.disabledSprite;

        botonPopUpIrMapa.onClick.AddListener(delegate { SceneChanger.Instance.GoToScene(Scenes.MainMenu); });

        botonSiguiente.onClick.AddListener(delegate { SceneChanger.Instance.GoToScene(nextScene); });
    }

    public void pausarJuego()
    {
        if (btnPausaClicked)
        {
            Time.timeScale = 1;
            botonPausa.GetComponent<Image>().sprite = spritePausaNormal;
            juegoPausa.SetActive(false);
            btnPausaClicked = false;
        }
        else
        {
            Time.timeScale = 0;
            botonPausa.GetComponent<Image>().sprite = spritePausaPressed;
            juegoPausa.SetActive(true);
            btnPausaClicked = true;
        }
    }

    public void pulsarMapa()
    {
        if (btnMapaClicked)
        {
            botonMapa.GetComponent<Image>().sprite = spriteMapaNormal;
            popMapa.SetActive(false);
            btnMapaClicked = false;
        }
        else
        {
            botonMapa.GetComponent<Image>().sprite = spriteMapaPressed;
            popMapa.SetActive(true);
            btnMapaClicked = true;
        }
    }



}
