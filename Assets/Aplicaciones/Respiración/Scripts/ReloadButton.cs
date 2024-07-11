using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ReloadButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _button.onClick.AddListener(SceneChanger.Instance.ReloadCurrentScene);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(SceneChanger.Instance.ReloadCurrentScene);
    }
}
