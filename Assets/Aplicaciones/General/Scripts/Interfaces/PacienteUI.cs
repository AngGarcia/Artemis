using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class PacienteUI : MonoBehaviour
{
    [Header("COMPONENTES UI")]
    [SerializeField] private TMP_Text ID;
    [SerializeField] private Button btnVerDatos;
    [SerializeField] private Button btnJuego;

    public void setID(string idPaciente)
    {
        ID.text = idPaciente;
    }
}
