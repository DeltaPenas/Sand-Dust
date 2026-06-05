using Unity.VisualScripting;
using UnityEngine;

public class NPController : MonoBehaviour
{
    private NPCData npcData;
    private CaixaDeDialogoUI dialogoUI;
    private ShopManager shopManager;
    private bool jaGerouLoja = false;
    [SerializeField]private FirstBossController firstBossController;

    [Header("caso seja o boss")]



    public bool playerDentro;

    void Awake()
    {
        npcData = GetComponent<NPCData>();
        dialogoUI = FindAnyObjectByType<CaixaDeDialogoUI>();
        shopManager = FindAnyObjectByType<ShopManager>();
        firstBossController = FindAnyObjectByType<FirstBossController>();

        if (npcData.tipoNPC == NPCtype.boss)
        {
            firstBossController = GetComponent<FirstBossController>();
            Debug.Log("Achou o fistBossController");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerDentro)
        {
            AbrirDialogo();

        }
    }

    void AbrirDialogo()
    {
        Time.timeScale = 0f;

        dialogoUI.IniciarDialogo(npcData, this);
    }

    public void FecharDialogo()
{
    Time.timeScale = 1f;

    dialogoUI.FecharDialogoUI();

    if(npcData.tipoNPC == NPCtype.boss)
    {
        Debug.Log("Chamando IniciarBoss");

        firstBossController.IniciarBoss();

        Debug.Log("Retornou do IniciarBoss");

        Destroy(this);
    }
}

    public void AbrirLoja()
    {
        FecharDialogo();

        shopManager.AbrirLoja();
        if (!jaGerouLoja)
        {
            shopManager.GerarLoja();
            jaGerouLoja = true;
        }
    }

   
}