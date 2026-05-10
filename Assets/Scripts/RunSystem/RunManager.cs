using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditor;


public class RunManager : MonoBehaviour
{
    
    public static RunManager Instance;
    private RunInfos runInfos;

    public RunState currentState = RunState.None;
    public RunData currentRun = new RunData();
    private DeathUI deathUI;
    private PlayerVida playerVida;

    private float tempoInicioRun;

    private PontoDeDecidaDeSala pontoDescida;
    private DungeonGeneratortest dungeonGenerator;

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
        currentState = RunState.Running;
        tempoInicioRun = Time.time;
    }
    else
    {
        Destroy(gameObject);
    }
    }
    public void StartRun()
    {
        deathUI = FindAnyObjectByType<DeathUI>(FindObjectsInactive.Include);
        currentState = RunState.Starting;

        SaveSystem.DeleteRunSave(); // aqui
        currentRun.reset();

        tempoInicioRun = Time.time;
        currentState = RunState.Running;

        Debug.Log("Iniciando run");

        SceneManager.LoadScene("DungeonScene");
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "DungeonScene")
        {
            playerVida = FindAnyObjectByType<PlayerVida>();
            deathUI = FindAnyObjectByType<DeathUI>(FindObjectsInactive.Include);

            if (playerVida != null)
            {
                SaveSystem.LoadRun(currentRun, playerVida);
                foreach (string cardId in currentRun.cartasColetadasIds)
                {
                    CardEffectManager.ApplyEffect(cardId);
                }
                StartCoroutine(AtualizarUIAposLoadComDelay());
            }
            PlayerController pc = FindAnyObjectByType<PlayerController>();
            if (pc != null) pc.gems = currentRun.moedasRun;
            pontoDescida = FindAnyObjectByType<PontoDeDecidaDeSala>();
            dungeonGenerator = FindAnyObjectByType<DungeonGeneratortest>();

            Debug.Log("Referências da dungeon carregadas");

            
        }
    }
    

    public void EndRun()
    {
    if(currentState != RunState.Running) return;

    currentState = RunState.PlayerDead;

    // XP permanente já foi somado quando os inimigos morreram.

    if (deathUI != null)
        deathUI.Show();
    else
        Debug.LogError("DeathUI não encontrada!");
    }

    public void RestartRun()
    {
        
        StartRun();
        
        
    }

    public void VoltarProMenu()
    {
        currentState = RunState.None;
        
        SceneManager.LoadScene("MenuInicial");
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
        Debug.Log("XP recebido: " + xp);
        currentRun.xpColetado += xp;

        HeartUi ui = FindAnyObjectByType<HeartUi>();
        if (ui != null)
            ui.AtualizarXP(currentRun.xpColetado);

        SaveCurrentRun();
    }
    public void AddInimigoCount()
    {
        Debug.Log("Inimigo contado!");
        currentRun.inimigosMortos++;
        SaveCurrentRun();
    }
    public void AddSala()
    {
        currentRun.salasConcluidas++;

        BuscarReferenciasDungeon();

        if (pontoDescida != null &&
            dungeonGenerator != null &&
            currentRun.salasConcluidas >= dungeonGenerator.totalSalasCombate)
        {
            pontoDescida.AtivarPortal();
        }

        SaveCurrentRun();
    }

    public void RegisterDeathUI(DeathUI ui)
    {
        deathUI = ui;
    }

    public void SaveCurrentRun()
    {
        Debug.Log("Tentando salvar run...");

        if (playerVida == null)
            playerVida = FindAnyObjectByType<PlayerVida>();

        if (playerVida == null)
        {
            Debug.LogWarning("Save ignorado: PlayerVida não encontrado.");
            return;
        }

        SaveSystem.SaveRun(currentRun, playerVida);
    }

    private IEnumerator AtualizarUIAposLoadComDelay()
    {
        yield return null; // espera 1 frame para UI/Start() inicializarem

        HeartUi heartUi = FindAnyObjectByType<HeartUi>();

        if (heartUi != null)
        {
            heartUi.AtualizarTudo();
            Debug.Log("UI atualizada após LoadRun.");
        }
        else
        {
            Debug.LogWarning("HeartUi não encontrado ao carregar a run.");
        }
    }

    public void AddMoedasRun(int valor)
    {
        currentRun.moedasRun += valor;
        AtualizarMoedasUI();
        SaveCurrentRun();
    }
    public void RemoveMoedasRun(int valor)
    {
        currentRun.moedasRun -= valor;
        AtualizarMoedasUI();
        SaveCurrentRun();   
    }

    public bool GastarMoedasRun(int valor)
    {
        if (currentRun.moedasRun < valor)
            return false;

        currentRun.moedasRun -= valor;
        AtualizarMoedasUI();
        SaveCurrentRun();
        return true;
    }

    private void AtualizarMoedasUI()
    {
        HeartUi ui = FindAnyObjectByType<HeartUi>();
        if (ui != null)
            ui.AtualizarGemas();
    }

    public void AddCard(string cardId)
    {
        if (!currentRun.cartasColetadasIds.Contains(cardId))
            currentRun.cartasColetadasIds.Add(cardId);

        SaveCurrentRun();
    }

    private void BuscarReferenciasDungeon()
    {
    if (pontoDescida == null)
        pontoDescida = FindAnyObjectByType<PontoDeDecidaDeSala>();

    if (dungeonGenerator == null)
        dungeonGenerator = FindAnyObjectByType<DungeonGeneratortest>();
    }

}