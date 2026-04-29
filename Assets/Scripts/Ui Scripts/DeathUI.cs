using UnityEngine;
using TMPro;
using UnityEngine.UI; 


public class DeathUI : MonoBehaviour
{
    [SerializeField] private TextMeshPro xpRunText;
    [SerializeField] private TextMeshPro qtdSalasConcluidas;
    [SerializeField] private TextMeshPro qtdInimigosDerrotados;
    [SerializeField] private TextMeshPro tempo;
    [SerializeField] private GameObject painel;
    
     void OnEnable()
    {
        Time.timeScale = 0f;
        Setup();
    }

    void Setup()
    {
        xpRunText.text = "Experiência coletada: " + RunManager.Instance.currentRun.xpColetado;
        qtdInimigosDerrotados.text = "Inimigos derrotados:" + RunManager.Instance.currentRun.inimigosMortos;
        qtdSalasConcluidas.text = "Salas concluidas: "+ RunManager.Instance.currentRun.salasConcluidas;
        tempo.text = "Tempo: " + RunManager.Instance.currentRun.tempoDeRun;
    }

    void Retry()
    {
      Time.timeScale = 1f;
      RunManager.Instance.RestartRun();  
    }
    void ReturnMenu()
    {
      Time.timeScale = 1f;
      RunManager.Instance.VoltarProMenu();  
    }

}
