using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(StudioEventEmitter))]
[RequireComponent (typeof(Button))]
public class StartEmitter : MonoBehaviour
{
    private StudioEventEmitter _emitter;
    private EventInstance _eventInstance;
    private Button _button;

    private void Awake()
    {
        _emitter = GetComponent<StudioEventEmitter>();
        _button = GetComponent<Button>();

        _eventInstance = _emitter.EventInstance;

        if (!IsPlaying(_eventInstance))
        {
            _emitter.Play();
            StudioEventEmitter.UpdateActiveEmitters();
        }

        _button.onClick.AddListener(StopEmitter);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(StopEmitter);
    }

    private void StopEmitter()
    {
        _emitter.Stop();
    }

    private bool IsPlaying(EventInstance instance)
    {
        PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        Debug.Log(state);

        if (state == PLAYBACK_STATE.STOPPED || state == PLAYBACK_STATE.STOPPING)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
