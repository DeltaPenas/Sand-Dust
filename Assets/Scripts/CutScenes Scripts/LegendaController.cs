using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
public class LegendaController : MonoBehaviour
{
    public List<string> legendas; 
    public float tempoEntreLegendas = 4f;
    [SerializeField] private TextMeshProUGUI textoLegenda;
    private int indiceLegenda = 0;
    private float cronometro = 0f;

    void Start()
    {
        if (legendas.Count > 0)
        {
            textoLegenda.text = legendas[indiceLegenda];
        }
    }

    void Update()
    {
        cronometro += Time.deltaTime;

        if (cronometro >= tempoEntreLegendas)
        {
            indiceLegenda++;
            cronometro = 0f;

            if (indiceLegenda < legendas.Count)
            {
                textoLegenda.text = legendas[indiceLegenda];
            }
        }
    }
}
