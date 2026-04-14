using UnityEngine;
using System.Collections.Generic;

public class DungeonGeneratortest : MonoBehaviour
{
    [Header("Configuração")]
    public int qtdminSalas = 5;
    public int qtdmaxSalas = 10;
    public int andar = 0;

    [Header("Prefab")]
    public GameObject prefabSalaTeste;
    public Transform DungeonParent;
    public float distanciaEntreSalas = 10f;

    private List<SalaNode> salas = new List<SalaNode>();

    private void Start()
    {
        GerarDungeon();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // futura lógica para trocar de andar
        }
    }

    private void GerarDungeon()
    {
        if (!ValidarParametros())
            return;

        salas.Clear();
        andar++;

        Debug.Log("Andar atual: " + andar);

        GerarLayout();
        DefinirSalaFinal();
        DefinirSalasEspeciais();

        DebugSalas();

        InstanciarSalas();
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
            if (sala.tipo == TipoSala.Inicial)
                continue;

            if (sala.tipo == TipoSala.SalaProxLayer)
                continue;

            salasValidas.Add(sala);
        }

        int qtdTesouros = salas.Count > 6 ? 2 : 1;

        for (int i = 0; i < qtdTesouros; i++)
        {
            if (salasValidas.Count == 0)
                break;

            int index = Random.Range(0, salasValidas.Count);

            salasValidas[index].tipo = TipoSala.Tesouro;

            salasValidas.RemoveAt(index);
        }
    }

    private void InstanciarSalas()
    {
        foreach (SalaNode sala in salas)
        {
            Vector3 posicaoMundo = new Vector3(
                sala.Posicao.x * distanciaEntreSalas,
                sala.Posicao.y * distanciaEntreSalas,
                0
            );

            GameObject salaGO = Instantiate(
                prefabSalaTeste,
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
            }
        }
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

    private void DebugSalas()
    {
        foreach (SalaNode sala in salas)
        {
            Debug.Log($"{sala.Posicao} - {sala.tipo}");
        }
    }
}