using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DungeonGeneratortest : MonoBehaviour
{
    [Header("Configuração")]
    public int qtdminSalas = 5;
    public int qtdmaxSalas = 10;
    public int totalSalasCombate;
    public int andar = 0;
    [SerializeField] private GameObject bg;
    [SerializeField] public AudioClip efeitoSonoroDeAbrirPorta;
    

    [Header("Prefab")]
    public Transform DungeonParent;
    public float distanciaEntreSalas = 10f;

    public List<SalaNode> salas = new List<SalaNode>();
    public CatalogoSalas catalogoSalas;
    [Header("Objetos das salas")]
    public CatalogoInimigos catalogoInimigos;
    public CatalogoProps catalogoProps;
    public int qtdInimigos;
    public RunInfos runInfos;
    public PlayerController pc;
    private SalaController salaInicial;
    private TriggerDeTransicao tt;


    [Header("Prefabs da geração de salas")]

    public CatalogoSalas catalogoSalasFloresta;
    public CatalogoSalas catalogoSalasSertao;

    public CatalogoInimigos catalogoInimigosFloresta;
    public CatalogoInimigos catalogoInimigosSertao;

    public CatalogoProps catalogoPropsFloresta;
    public CatalogoProps catalogoPropsSertao;
   
    


    private void Start()
    {
        
        tt = FindAnyObjectByType<TriggerDeTransicao>();
        runInfos = GetComponent<RunInfos>();
        pc = FindAnyObjectByType<PlayerController>();
        GerarDungeon();
        GameObject background = Instantiate(bg, transform.position, Quaternion.identity);
    }


    private void GerarDungeon()
    {
        AtualizarTemaDungeon();

        if (RunManager.Instance.currentRun.layer >= 2)
        {
            Debug.Log("gerando sala do boss");

            salas.Clear();

            GerarMiniDungeonBoss();

            InstanciarSalas();

            DebugSalas();

            TeleportarPlayerSalaInicial();

            CalcularSalasCombate();

    return;
        }
        else
        {
        if (!ValidarParametros())
        return;

        
        salas.Clear();
       
        andar++;

        Debug.Log("Andar atual: " + andar);

        GerarLayout();
        DefinirSalaFinal();
        DefinirSalasEspeciais();
        InstanciarSalas();
        DebugSalas();
        TeleportarPlayerSalaInicial();
        CalcularSalasCombate(); 
        }
        
    }

    public void LimparDungeon()
{
    foreach (Transform filho in DungeonParent)
    {
        Destroy(filho.gameObject);
    }

    salas.Clear();
    GerarDungeon();

}

    private void AtualizarTemaDungeon()
{
    if (andar <= 1)
    {
        catalogoSalas = catalogoSalasFloresta;
        catalogoInimigos = catalogoInimigosFloresta;
        catalogoProps = catalogoPropsFloresta;

        Debug.Log("Tema atual: Floresta");
    }
    else
    {
        catalogoSalas = catalogoSalasSertao;
        catalogoInimigos = catalogoInimigosSertao;
        catalogoProps = catalogoPropsSertao;

        Debug.Log("Tema atual: Sertão");
    }
}

    private bool ValidarParametros()
    {
        if (qtdmaxSalas <= 4 || qtdminSalas <= 4 || qtdminSalas > qtdmaxSalas)
        {
            Debug.Log("Quantidade de salas inválida");
            return false;
        }

        return true;
    }

    private void GerarLayout()
    {
        int qtdSalas = Random.Range(qtdminSalas, qtdmaxSalas + 1);

        Vector2Int[] direcoes =
        {
            Vector2Int.right,
            Vector2Int.left,
            Vector2Int.up,
            Vector2Int.down
        };

        SalaNode salaInicial = new SalaNode(Vector2Int.zero);
        salaInicial.tipo = TipoSala.Inicial;
        salas.Add(salaInicial);

        while (salas.Count < qtdSalas)
        {
            SalaNode salaOrigem = salas[Random.Range(0, salas.Count)];
            Vector2Int direcao = direcoes[Random.Range(0, direcoes.Length)];
            Vector2Int novaPosicao = salaOrigem.Posicao + direcao;

            if (!ExisteSala(novaPosicao))
            {
                salas.Add(new SalaNode(novaPosicao));
            }
        }
    }

    private void DefinirSalaFinal()
    {
        int maiorDistancia = 0;
        SalaNode salaFinal = null;

        foreach (SalaNode sala in salas)
        {
            if (sala.tipo == TipoSala.Inicial)
                continue;

            int distancia = Mathf.Abs(sala.Posicao.x) + Mathf.Abs(sala.Posicao.y);

            if (distancia > maiorDistancia)
            {
                maiorDistancia = distancia;
                salaFinal = sala;
            }
        }

        if (salaFinal != null)
        {
            salaFinal.tipo = TipoSala.SalaProxLayer;
        }
    }

    private void DefinirSalasEspeciais()
{
    List<SalaNode> salasValidas = new List<SalaNode>();

    foreach (SalaNode sala in salas)
    {
        if (sala.tipo == TipoSala.Inicial) continue;
        if (sala.tipo == TipoSala.SalaProxLayer) continue;

        salasValidas.Add(sala);
    }

    if (salasValidas.Count == 0) return;

    //embaralha
    for (int i = 0; i < salasValidas.Count; i++)
    {
        int rand = Random.Range(i, salasValidas.Count);
        (salasValidas[i], salasValidas[rand]) = (salasValidas[rand], salasValidas[i]);
    }

    int qtdTesouros = salas.Count > 8 ? 2 : 1;
    int indexAtual = 0;

    
    for (int i = 0; i < qtdTesouros && indexAtual < salasValidas.Count; i++)
    {
        salasValidas[indexAtual].tipo = TipoSala.Tesouro;
        indexAtual++;
    }

    
    if (indexAtual < salasValidas.Count)
    {
        salasValidas[indexAtual].tipo = TipoSala.Loja;
        indexAtual++;
    }

        if (indexAtual < salasValidas.Count  && RunManager.Instance.currentRun.layer > 1)
        {
            salasValidas[indexAtual].tipo = TipoSala.SalasMiniBoss;   
        }

}

    private void InstanciarSalas()
    {
        foreach (SalaNode sala in salas)
        {
            Vector3 posicaoMundo = ObterPosicaoMundo(sala);

            GameObject prefabEscolhido = EscolherPrefabSala(sala.tipo);
            if (prefabEscolhido == null) continue;

            GameObject salaGO = Instantiate(
                prefabEscolhido,
                posicaoMundo,
                Quaternion.identity,
                DungeonParent
            );

            salaGO.name = $"Sala_{sala.tipo}_{sala.Posicao}";

            SalaController controller = salaGO.GetComponent<SalaController>();

            if (controller != null) 
            {
                controller.ConfigurarSala(sala);
                //definr as portas
                bool cima = ExisteSala(sala.Posicao + Vector2Int.up);
                bool baixo = ExisteSala(sala.Posicao + Vector2Int.down);
                bool esquerda = ExisteSala(sala.Posicao + Vector2Int.left);
                bool direita = ExisteSala(sala.Posicao + Vector2Int.right);

                controller.ConfigurarPortas(
                    cima,
                    baixo,
                    esquerda,
                    direita
                );
                if (sala.tipo == TipoSala.Inicial)
                {
                    salaInicial = controller;
                }
            }
        }
    }
        private void GerarMiniDungeonBoss(){
            salas.Clear();

            andar++;

            SalaNode salasIniciaisAntesDoBoss = new SalaNode(Vector2Int.zero);
            salasIniciaisAntesDoBoss.tipo = TipoSala.SalaAntesDoBoss;
            salas.Add(salasIniciaisAntesDoBoss);

            Vector2Int posBoss = new Vector2Int(1, 0);

            SalaNode salaBoss = new SalaNode(posBoss);
            salaBoss.tipo = TipoSala.SalaBoss;

            salas.Add(salaBoss);
        }
    
        private GameObject EscolherPrefabSala(TipoSala tipo)
        {
            List<GameObject> lista = null;

            switch (tipo)
            {
            case TipoSala.Inicial:
                lista = catalogoSalas.salasIniciais;
                break;

            case TipoSala.Normal:
                lista = catalogoSalas.salasNormais;
                break;

            case TipoSala.Tesouro:
                lista = catalogoSalas.salasTesouro;
                break;

            case TipoSala.SalaProxLayer:
                lista = catalogoSalas.salasProxLayer;
                break;

            case TipoSala.Loja:
                lista = catalogoSalas.salaLojas;
                break;

            case TipoSala.Secreta:
                lista = catalogoSalas.salasSecretas;
                break;

            case TipoSala.SalaBoss:
                lista = catalogoSalas.salasBoss;
                break;

            case TipoSala.SalasMiniBoss:
                lista = catalogoSalas.salasMiniBoss;
                break;
            case TipoSala.SalaAntesDoBoss:
                lista = catalogoSalas.salasIniciaisAntesDoBoss;
                break;
            }
            if (lista == null || lista.Count == 0)
            {
                Debug.LogError("Lista de salas vazia para: " + tipo);
                return null;
            }

        int indice = Random.Range(0, lista.Count);
        return lista[indice];
        }

    private bool ExisteSala(Vector2Int posicao)
    {
        foreach (SalaNode sala in salas)
        {
            if (sala.Posicao == posicao)
                return true;
        }

        return false;
    }

    public bool ExisteSalaNessaDirecao(Vector2Int posicao)
    {
        foreach (SalaNode sala in salas)
        {
            if(sala.Posicao == posicao) return true;
        }
        return false;
    }

    public SalaController BuscarSalaPorPosicao(Vector2Int posicao)
    {
    SalaController[] salas = FindObjectsOfType<SalaController>();

    foreach (SalaController sala in salas)
    {
        if (sala.posicaoGrid == posicao)
            return sala;
    }

    return null;
    }
    private void DebugSalas()
    {
        foreach (SalaNode sala in salas)
        {
            Debug.Log($"{sala.Posicao} - {sala.tipo}");
        }
    }

    private void TeleportarPlayerSalaInicial()
    {
        pc.transform.position = salaInicial.transform.position;
    }
    public bool EhSalaDeCombate(TipoSala tipo)
    {
    return tipo != TipoSala.Inicial &&
           tipo != TipoSala.Loja &&
           tipo != TipoSala.Tesouro &&
           tipo != TipoSala.SalaProxLayer;
    }
    private void CalcularSalasCombate()
    {
    totalSalasCombate = 0;

    foreach (SalaNode sala in salas)
    {
        if (EhSalaDeCombate(sala.tipo))
        {
            totalSalasCombate++;
        }
    }

    Debug.Log("Total salas combate: " + totalSalasCombate);
    }
    public void LimparInimigos()
    {
        GameObject[] inimigos = GameObject.FindGameObjectsWithTag("inimigo");
        GameObject[] items = GameObject.FindGameObjectsWithTag("Items");

        foreach (GameObject inimigo in inimigos)
        {
            Destroy(inimigo);
        }
        foreach (GameObject item in items)
        {
            Destroy(item);
        }
    }
    private Vector3 ObterPosicaoMundo(SalaNode sala)
    {
        float distancia = distanciaEntreSalas;

        if (sala.tipo == TipoSala.SalaBoss)
        {
            distancia = distanciaEntreSalas * 2.5f;
        }

        return new Vector3(
            sala.Posicao.x * distancia,
            sala.Posicao.y * distancia,
            0
        );
    }
    

}