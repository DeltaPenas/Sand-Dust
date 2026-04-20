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

    [Header("Configs")]

    public TipoSala tipoSala;
    public Vector2Int posicaoGrid;
    public bool entrou = false;
    public bool salaLimpa = false;
    public int qtdInimigosVivos;
    private SpawnerController spawner;
    private PortaTrigger[] portas;
    private PropSpawner[] props;
    public DungeonGeneratortest dg;
    private RunInfos runInfos;
    
    
    


    private void Awake()
    {
        runInfos = FindAnyObjectByType<RunInfos>();
        dg = GetComponentInParent<DungeonGeneratortest>();
        props = GetComponentsInChildren<PropSpawner>();
        spawner = GetComponentInChildren<SpawnerController>();
        portas = GetComponentsInChildren<PortaTrigger>();
        

    }
    

    public void ConfigurarSala(SalaNode sala)
    {
        tipoSala = sala.tipo;
        posicaoGrid = sala.Posicao; 

        if(tipoSala == TipoSala.Inicial || tipoSala == TipoSala.Loja || tipoSala == TipoSala.Tesouro || tipoSala == TipoSala.SalaProxLayer)
        {
            salaLimpa = true;
            LiberarPortas();
            
            

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
        runInfos.playerScore +=1;
        runInfos.salasConcluidas+=1;

        foreach (PortaTrigger porta in portas)
        {
            Debug.Log("Liberando porta: " + porta.name);
            porta.podeTeleportar = true;
        }
            
    }
    }

    public void LiberarPortas()
    {
        foreach (PortaTrigger porta in portas)
        {
        porta.podeTeleportar = true;
        }

        Debug.Log("Portas reativadas");
    }

}