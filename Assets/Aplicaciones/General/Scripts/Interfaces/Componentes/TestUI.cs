using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace General {
    public class TestUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text momento;
        [SerializeField] private TMP_Text estado;
        [SerializeField] private TMP_Text tiempo;

        public void setInfo(string momento, EstadoPaciente estado, string tiempo)
        {
            this.momento.text = momento;
            this.estado.text = estado.ToString();
            this.tiempo.text = tiempo;
        }
    }
}
