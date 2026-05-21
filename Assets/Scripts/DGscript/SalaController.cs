using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class SalaController : MonoBehaviour
{
   

    [Header("Portas")]

    public GameObject portaCima;
    public GameObject portaBaixo;
    public GameObject portaEsquerda;
    public GameObject portaDireita;

    public Transform spawnCima;
    public Transform spawnBaixo;
    public Transform spawnEsquerda;
    public Transform spawnDireita;
    public GameObject[] portasIluminaçao;



    [Header("Configs")]

    public TipoSala tipoSala;
    public Vector2Int posicaoGrid;
    public bool entrou = false;
    public bool salaLimpa = false;
    public int qtdInimigosVivos;
    private SpawnerController spawner;
    private SoundController soundController;
    private PortaTrigger[] portas;
    
    
    private PropSpawner[] props;
    public DungeonGeneratortest dg;

    [Header("Boss")]

    public bool bossFightIniciada = false;
    public bool bossDerrotado = false;
    [SerializeField] private FirstBossController boss;
   
    
    
    
    


    private void Awake()
    {

        
        dg = GetComponentInParent<DungeonGeneratortest>();
        props = GetComponentsInChildren<PropSpawner>();
        spawner = GetComponentInChildren<SpawnerController>();
        portas = GetComponentsInChildren<PortaTrigger>();
        soundController = FindAnyObjectByType<SoundController>();
        boss = GetComponentInChildren<FirstBossController>();

    }
    

    public void ConfigurarSala(SalaNode sala)
{
    tipoSala = sala.tipo;
    posicaoGrid = sala.Posicao; 

    if(tipoSala == TipoSala.Inicial || tipoSala == TipoSala.Loja || 
   tipoSala == TipoSala.Tesouro || tipoSala == TipoSala.SalaProxLayer || 
   tipoSala == TipoSala.SalaAntesDoBoss)
    {
        salaLimpa = true;
        LiberarPortas();
    }
    else if(tipoSala == TipoSala.SalaBoss)
    {
    salaLimpa = false;

    foreach (PortaTrigger porta in portas)
    {
        porta.podeTeleportar = false;
        AlterarTransparenciaPortal(porta, 0.2f);
    }
    }
    else
    {
        // deixa os portais transparentes
        foreach (PortaTrigger porta in portas)
        {
            AlterarTransparenciaPortal(porta, 0.2f);
        }
    }
}

    public void ConfigurarPortas(
        bool cima,
        bool baixo,
        bool esquerda,
        bool direita
    )
    {
        portaCima.SetActive(cima);
        portaBaixo.SetActive(baixo);
        portaEsquerda.SetActive(esquerda);
        portaDireita.SetActive(direita);
    }
    public void AtivarSala()
    {
    if (entrou || salaLimpa)
        return;

        entrou = true;
        qtdInimigosVivos = spawner.SpawnarInimigos();

        foreach (PropSpawner prop in props)
            {
                prop.SpawnarProp();
            }

        Debug.Log("Inimigos na sala: " + qtdInimigosVivos);
    }
    public void InimigoDerrotado()
    {
        if (salaLimpa) return;

        qtdInimigosVivos--;
        Debug.Log("Inimigo derrotado. Restam: " + qtdInimigosVivos);

    if (qtdInimigosVivos <= 0)
    {
        Debug.Log("Sala limpa! Liberando portas.");
        salaLimpa = true;
        soundController.TocarSom(dg.efeitoSonoroDeAbrirPorta);


        if (RunManager.Instance != null)
        {
            RunManager.Instance.AddSala();
        }

        foreach (PortaTrigger porta in portas)
        {
            LiberarPortas();
        }
    }
    }

    public void LiberarPortas()
    {
        foreach (PortaTrigger porta in portas)
        {
            porta.podeTeleportar = true;

            AlterarTransparenciaPortal(porta, 1f);
        }

        foreach (GameObject porta in portasIluminaçao)
        {
            porta.SetActive(true);
        }

        Debug.Log("Portas reativadas");
    }

    public Transform ObterSpawnEntrada(DirecaoPorta direcao)
{
    switch (direcao)
    {
        case DirecaoPorta.Cima:
            return spawnBaixo;

        case DirecaoPorta.Baixo:
            return spawnCima;

        case DirecaoPorta.Esquerda:
            return spawnDireita;

        case DirecaoPorta.Direita:
            return spawnEsquerda;
    }

    return null;
}
    private void AlterarTransparenciaPortal(PortaTrigger porta, float alpha)
    {
        SpriteRenderer spriteRender = porta.GetComponent<SpriteRenderer>();

        if (spriteRender == null) return;

        Color cor = spriteRender.color;
        cor.a = alpha;
        spriteRender.color = cor;
    }

    public void IniciarBossFight()
    {
        if (bossFightIniciada) return;

        bossFightIniciada = true;

        foreach (PortaTrigger porta in portas)
        {
            porta.podeTeleportar = false;
            AlterarTransparenciaPortal(porta, 0.2f);
        }

        boss.IniciarBoss();

        Debug.Log("Boss fight iniciada");
    }
    public void BossDerrotado()
    {
        bossDerrotado = true;
        salaLimpa = true;

        LiberarPortas();

        Debug.Log("Boss derrotado");
    }

}