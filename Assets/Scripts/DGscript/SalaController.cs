using Unity.VisualScripting;
using UnityEngine;
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
    public int qtdInimigos;
    private SpawnerController spawner;
    
    


    private void Awake()
    {
        spawner = GetComponentInChildren<SpawnerController>();
    }

    
    
    public void ConfigurarSala(SalaNode sala)
    {
        tipoSala = sala.tipo;
        posicaoGrid = sala.Posicao; 
        
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
        spawner.SpawnarInimigos();
        spawner.SpawnarProps();

        entrou = true;

        
    }
    public void SalaConcluida()
    {
        
    }

}