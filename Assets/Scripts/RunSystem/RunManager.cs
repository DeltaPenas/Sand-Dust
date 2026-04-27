using UnityEngine;
using UnityEngine.SceneManagement;


public class RunManager : MonoBehaviour
{
    
    public static RunManager Instance;

    public RunState currentState = RunState.None;
    public RunData currentRun = new RunData();

    private float tempoInicioRun;

    //Rapaziada, Quando formos criar as cenas, seguir o Nome Daqui, os nomes estão em SceneManager.LoadScene

    void Awake()
    {
        if(Instance == null)
        {
            Instance = null;
            DontDestroyOnLoad(gameObject); // manter salvo durante as cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void StartRun()
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
        //ProgressionManager.Instance.AddXP(currentRun.xpColetado); //Parte do samuel, receber o xp do player

        SceneManager.LoadScene("TelaMorte");

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


}