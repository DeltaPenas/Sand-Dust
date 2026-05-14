using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaTrigger : MonoBehaviour
{
    public DirecaoPorta direcao;
    public float distanciaEntreSalas;
    public float distanciaEntreSalasVertical;
    public float offsetPlayer = 1.5f;

    public bool podeTeleportar;
    private SalaController salaAtual;
    private DungeonGeneratortest dungeon;
    private PlayerController player;
    private TriggerDeTransicao tt;
    private bool playerDentro;
    

    [System.Obsolete]
    private void Start()
    {
        tt = FindAnyObjectByType<TriggerDeTransicao>();
        salaAtual = GetComponentInParent<SalaController>();
        dungeon = FindObjectOfType<DungeonGeneratortest>();
        player = FindAnyObjectByType<PlayerController>();
        Debug.Log("Sala atual: " + salaAtual);
        Debug.Log("Dungeon: " + dungeon);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (playerDentro) return;

        playerDentro = true;

        if (!salaAtual.salaLimpa) return;

        Vector2Int direcaoGrid = ObterDirecaoGrid();
        Vector2Int proximaSala = salaAtual.posicaoGrid + direcaoGrid;

        if (!dungeon.ExisteSalaNessaDirecao(proximaSala))
        {
            Debug.Log("Não existe sala nessa direção");
            return;
        }

        StartCoroutine(SequenciaTeleporte(other.transform));
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerDentro = false;
    }

    private Vector2Int ObterDirecaoGrid()
    {
        switch (direcao)
        {
            case DirecaoPorta.Cima:
                return Vector2Int.up;

            case DirecaoPorta.Baixo:
                return Vector2Int.down;

            case DirecaoPorta.Esquerda:
                return Vector2Int.left;

            case DirecaoPorta.Direita:
                return Vector2Int.right;
        }

        return Vector2Int.zero;
    }

    private void Teleportar(Transform player)
    {
         SalaController proximaSala = BuscarProximaSala();

    if (proximaSala == null)
    {
        Debug.LogWarning("Próxima sala não encontrada");
        return;
    }

    Transform pontoSpawn =
        proximaSala.ObterSpawnEntrada(direcao);

    if (pontoSpawn == null)
    {
        Debug.LogWarning("Spawn não encontrado");
        return;
    }

    player.position = pontoSpawn.position;

    Camera.main.transform.position =
        new Vector3(
            proximaSala.transform.position.x,
            proximaSala.transform.position.y,
            Camera.main.transform.position.z
        );
        

    }
    private SalaController BuscarProximaSala()
    {
        Vector2Int direcaoGrid = ObterDirecaoGrid();

        Vector2Int posicaoDestino = salaAtual.posicaoGrid + direcaoGrid;

        return dungeon.BuscarSalaPorPosicao(posicaoDestino);
    }
   IEnumerator SequenciaTeleporte(Transform alvo)
    {
        player.iframeAtivo = true;
        player.podeMover = false;

        player.rig.linearVelocity = Vector2.zero;

        tt.FadeOut();

        yield return new WaitForSeconds(1f);

        Teleportar(alvo);

        yield return new WaitForSeconds(0.1f);

        player.iframeAtivo = false;
        player.podeMover = true;
    }

    private void ReativarTrigger()
    {
        podeTeleportar = true;
    }
}