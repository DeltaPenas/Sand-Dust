using UnityEngine;
using TMPro;

public class DeathUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI xpRunText;
    [SerializeField] private TextMeshProUGUI qtdSalasConcluidas;
    [SerializeField] private TextMeshProUGUI qtdInimigosDerrotados;
    [SerializeField] private TextMeshProUGUI tempo;
    [SerializeField] private GameObject painel;

    void Start()
    {
    RunManager.Instance.RegisterDeathUI(this);
    }
    public void Show()
    {
        painel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        Setup();
    }

    void Setup()
    {
        var run = RunManager.Instance.currentRun;

        xpRunText.text = "XP: " + run.xpColetado;
        qtdInimigosDerrotados.text = "Inimigos: " + run.inimigosMortos;
        qtdSalasConcluidas.text = "Salas: " + run.salasConcluidas;
        tempo.text = "Tempo: " + run.tempoDeRun.ToString("F1");
    }

    public void Retry()
    {
        Debug.Log("Testando"); //esse debug também não funcionou, então acho que o problema é o botão
        Time.timeScale = 1f;
        painel.SetActive(false);
        RunManager.Instance.RestartRun();
    }

    public void ReturnMenu()
    {
        Time.timeScale = 1f;
        painel.SetActive(false);
        RunManager.Instance.VoltarProMenu();
    }
}