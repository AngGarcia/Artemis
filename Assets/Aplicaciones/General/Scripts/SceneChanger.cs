using FMODUnity;
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

        //Debug.Log("Actual scene: " + actualScene + ", Prev scene: " + prevScene);

        SceneManager.LoadScene((int) actualScene);
    }

    public void GoToScene(Scenes s)
    {
        prevScene = actualScene;
        actualScene = s;

        //Debug.Log("Actual scene: " + actualScene + ", Prev scene: " + prevScene);

        SceneManager.LoadScene((int)actualScene);
    }

    public void ReloadCurrentScene()
    {
        var emitter = FindObjectOfType<StudioEventEmitter>();

        if (emitter != null)
        {
            emitter.EventInstance.setPaused(true);
        }

        SceneManager.LoadScene((int)actualScene);

        if (emitter != null)
        {
            emitter.EventInstance.setPaused(false);
        }
    }
}
