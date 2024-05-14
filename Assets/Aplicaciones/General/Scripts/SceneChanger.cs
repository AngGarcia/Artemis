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

    public void GoToScene(int s)
    {
        actualScene = (Scenes) s;
        prevScene = actualScene--;

        SceneManager.LoadScene((int)actualScene);
    }
}
