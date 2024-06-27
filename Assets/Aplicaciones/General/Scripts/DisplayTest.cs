using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTest : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _faceImage;
    [SerializeField] private Image _fillImage;

    [SerializeField] private List<Sprite> _faces = new List<Sprite>();
    [SerializeField] private List<Color> _colors = new List<Color>();

    private void Start()
    {
        _slider.onValueChanged.AddListener(UpdateUI);

        _slider.value = 0;
    }

    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveListener(UpdateUI);
    }

    private void UpdateUI(float value)
    {
        int v = (int)value; 

        _faceImage.sprite = _faces[v];
        _fillImage.color = _colors[v];
    }
}
