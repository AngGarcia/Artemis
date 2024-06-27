using General;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class BookTextBoxes : MonoBehaviour
{
    [SerializeField]
    private OptionDataList PageBoxes = new OptionDataList();

    public List<OptionData> options
    {
        get { return PageBoxes.options; }
        set { PageBoxes.options = value; }
    }

    [Serializable]
    public class OptionData
    {
        [SerializeField]
        private int _page;
        [SerializeField]
        private GameObject _box;

        public int page { get { return _page; } set { _page = value; } }

        public GameObject box { get { return _box; } set { _box = value; } }

        public OptionData(int page, GameObject box)
        {
            this._page = page;
            this._box = box;
        }
    }

    [Serializable]
    public class OptionDataList
    {
        [SerializeField]
        private List<OptionData> _options;

        public List<OptionData> options { get { return _options; } set { _options = value; } }

        public OptionDataList()
        {
            options = new List<OptionData>();
        }
    }

    private void Awake()
    {
        string name = ConectToDatabase.Instance.getCurrentPaciente().nombre;
        TMP_Text textBox;
        foreach (var op in options)
        {
            textBox = op.box.GetComponentInChildren<TMP_Text>();
            if (textBox != null)
            {
                textBox.text = textBox.text.Replace("%n", name);
            }
        }
    }

    public void ActivateBox(int page)
    {
        foreach (var op in options)
        {
            if (op.page == page)
            {
                op.box.SetActive(true);
            }
            else
            {
                op.box.SetActive(false);
            }
        }
    }
}
