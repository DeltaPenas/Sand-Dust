using UnityEngine;
using UnityEngine.SceneManagement;


public class RunManager : MonoBehaviour
{
    
    public static RunManager Instance;
    private RunInfos runInfos;

    public RunState currentState = RunState.None;
    public RunData currentRun = new RunData();
    [SerializeField] private GameObject telaDeMorte;

    private float tempoInicioRun;

    //Rapaziada, Quando formos criar as cenas, seguir os nomes daqui, os nomes estão em SceneManager.LoadScene
    // Quando inimigo morre: RunManager.Instance.currentRun.inimigosMortos++;
    // Quando pegarXp: RunManager.Instance.currentRun.xpColetado += valor;
    // Quando o Player for de chapeu: RunManager.Instance.EndRun();
    // Implementar algumas informações no RunManager do RunInfos
    

    void Awake()
{
    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    else
    {
        Destroy(gameObject);
    }
    }
    public void StartRun()
    {
        currentState = RunState.Starting; // deixa o estado atual  pra iniciando 
        currentRun.reset(); //reseta a run, caso o player use o RestartRun();

        tempoInicioRun = Time.time; // Começa contar o tempo da run

        SceneManager.LoadScene("DungeonScene"); //Cena das dungeons

        currentState = RunState.Running; // deixa o estado atual pra rodando

    }

    

    void EndRun()
    {
        if(currentState != RunState.Running) return;

        currentState = RunState.PlayerDead;
        ProgressionManager.Instance.AddXPTotal(currentRun.xpColetado); //Parte do samuel, receber o xp do player
        telaDeMorte.SetActive(true);

        

    }

    public void RestartRun()
    {
        
        StartRun();
        
    }

    public void VoltarProMenu()
    {
        currentState = RunState.None;
        
        SceneManager.LoadScene("MenuDeIniciarFase");
    }

    void Update()
    {
        if (currentState == RunState.Running)
        {
            currentRun.tempoDeRun = Time.time - tempoInicioRun;
        }
    }

    public void AddXp(int xp)
    {
        currentRun.xpColetado +=xp;
    }
    public void AddInimigoCount()
    {
        currentRun.inimigosMortos++;
    }
    public void AddSala()
    {
        currentRun.salasConcluidas++;
    }

}