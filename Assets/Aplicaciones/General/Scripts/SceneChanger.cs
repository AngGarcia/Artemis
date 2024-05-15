using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : PersistentSingleton<SceneChanger>
{
    public enum Scenes
    {
        Database,
        LibroInteractivo,
        Respiracion,
        HarmonyHeaven,
        RitmoVejetal,
        PaisajeSonoro,
        MelodiaFloral
    }

    public static Scenes actualScene;
    public static Scenes prevScene = Scenes.LibroInteractivo;

    public void NextScene()
    {
        prevScene = actualScene;
        actualScene++;

        SceneManager.LoadScene((int) actualScene);
    }

    public void GoToScene(Scenes s)
    {
        actualScene = s;
        Debug.Log(actualScene);
        prevScene = actualScene - 1;

        Debug.Log(actualScene);

        SceneManager.LoadScene((int)actualScene);
    }
}
