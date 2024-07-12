using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ReloadButton : MonoBehaviour
{
    private Button _button;
    private List<seedMovement> _seedList;

    private void Awake()
    {
        _button = GetComponent<Button>();

        _seedList = new List<seedMovement>();
    }

    private void Start()
    {
        _seedList = GetComponents<seedMovement>().ToList();

        _button.onClick.AddListener(SceneChanger.Instance.ReloadCurrentScene);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(SceneChanger.Instance.ReloadCurrentScene);
    }

    private void ReloadTransforms()
    {
        for (int i = 0; i < _seedList.Count; i++)
        {
            _seedList[i].Reload();
        }
    }
}
