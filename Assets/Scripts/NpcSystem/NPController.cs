using UnityEngine;

public class NPController : MonoBehaviour
{
    private NPCData npcData;

    private CaixaDeDialogoUI dialogoUI;
    private ShopManager shopManager;
    private bool jaGerouLoja = false;


    public bool playerDentro;

    void Awake()
    {
        npcData = GetComponent<NPCData>();

        dialogoUI = FindAnyObjectByType<CaixaDeDialogoUI>();

        shopManager = FindAnyObjectByType<ShopManager>();
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