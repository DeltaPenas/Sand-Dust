using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaTrigger : MonoBehaviour
{
    public DirecaoPorta direcao;
    public bool podeTeleportar;
    private SalaController salaAtual;
    private DungeonGeneratortest dungeon;
    private PlayerController player;
    private TriggerDeTransicao tt;
    private bool playerDentro;
    private CaixaDeDialogoUI caixaDeDialogoUI;
    

    [System.Obsolete]

    private void Update()
{
    if (!playerDentro) return;

    if (!podeTeleportar) return;

    if (!salaAtual.salaLimpa) return;

    if (Input.GetKeyDown(KeyCode.F))
    {
        Vector2Int direcaoGrid = ObterDirecaoGrid();
        Vector2Int proximaSala = salaAtual.posicaoGrid + direcaoGrid;

        if (!dungeon.ExisteSalaNessaDirecao(proximaSala))
        {
            Debug.Log("Não existe sala nessa direção");
            return;
        }

        caixaDeDialogoUI.interactText.SetActive(false);

        StartCoroutine(SequenciaTeleporte(player.transform));
    }
}

    private void Start()
    {
        caixaDeDialogoUI = FindAnyObjectByType<CaixaDeDialogoUI>();
        tt = FindAnyObjectByType<TriggerDeTransicao>();
        salaAtual = GetComponentInParent<SalaController>();
        dungeon =  FindAnyObjectByType<DungeonGeneratortest>();
        player = FindAnyObjectByType<PlayerController>();
        Debug.Log("Sala atual: " + salaAtual);
        Debug.Log("Dungeon: " + dungeon);
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    if (!other.CompareTag("Player")) return;

    if (!podeTeleportar) return;

    if (!salaAtual.salaLimpa) return;

    playerDentro = true;

    caixaDeDialogoUI.interactText.SetActive(true);
}
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerDentro = false;
        caixaDeDialogoUI.interactText.SetActive(false);
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
        player.podeMover = true;

        yield return new WaitForSeconds(3f);

        player.iframeAtivo = false;
        
    }

    private void OnDestroy()
    {
        Debug.Log("Porta destruída: " + gameObject.name);
    }
}