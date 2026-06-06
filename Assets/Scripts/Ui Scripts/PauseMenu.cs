using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    
    [SerializeField] private GameObject Painel;
    [SerializeField] private GameObject Conclusao;
    [SerializeField] private GameObject TelaArtefatos;
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject minimapa;

    private ShopManager shop;
    private CaixaDeDialogoUI caixaDeDialogoUI;
    


    void Start()
    {
        shop = FindAnyObjectByType<ShopManager>();
        caixaDeDialogoUI = FindAnyObjectByType<CaixaDeDialogoUI>();

    }

    void Update()
    {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
        ToggleMenu();
    }
    }

    void ToggleMenu()
    {
        if (RunManager.Instance.currentState == RunState.Running)
        {
            Pausar();
        }
        else if (RunManager.Instance.currentState == RunState.Paused)
        {
            Despausar();
        }
    }

    public void ChamarConclusao()
    {
        RunManager.Instance.currentState = RunState.Finished;
        Time.timeScale = 0f;
        Conclusao.SetActive(true);
    }




    public void Pausar()
    {
        RunManager.Instance.currentState = RunState.Paused;
        Painel.SetActive(true);
        caixaDeDialogoUI.FecharDialogoUI();
        shop.FecharLoja();
        caixaDeDialogoUI.interactText.SetActive(false);
        RemoverHud();
        Time.timeScale = 0f;
    }

    public void Despausar()
    {
        RunManager.Instance.currentState = RunState.Running;
        Painel.SetActive(false);
        TelaArtefatos.SetActive(false);
        AtivarHud();
        Time.timeScale = 1f;
    }

    public void Reiniciar()
    {
        Despausar();
        RunManager.Instance.RestartRun();
    }
    public void VoltarMenu()
    {
        Despausar();
        RunManager.Instance.VoltarProMenu();
    }
    public void ChamarTelaDosArtefatos()
    {
        TelaArtefatos.SetActive(true);
    }
    public void TirarTelaDosArtefatos()
    {
        TelaArtefatos.SetActive(false);
    }
    public void RemoverHud(){
        hud.SetActive(false);
        minimapa.SetActive(false);
    }
    public void AtivarHud()
    {
        hud.SetActive(true);
        minimapa.SetActive(true);
    }
    




}