using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : PersistentSingleton<SceneChanger>
{
    public enum Scenes
    {
        MainMenu,
        LibroInteractivo,
        Respiracion,
        HarmonyHeaven,
        RitmoVegetal,
        PaisajeSonoro,
        MelodiaFloral
    }

    public Scenes actualScene;
    public Scenes prevScene = Scenes.LibroInteractivo;

    public void NextScene()
    {
        prevScene = actualScene;
        actualScene++;

        SceneManager.LoadScene((int) actualScene);
    }

    public void GoToScene(Scenes s)
    {
        prevScene = actualScene;
        actualScene = s;

        Debug.Log(actualScene);
        Debug.Log(prevScene);

        SceneManager.LoadScene((int)actualScene);
    }
}
