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
    public bool emTransicao;
    private SalaController salaAtual;
    private DungeonGeneratortest dungeon;
    private PlayerController player;
    private TriggerDeTransicao tt;
    

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
    if (!podeTeleportar) return;
    if (!other.CompareTag("Player")) return;
    if (emTransicao) return;
    if (player.emTeleporte) return;
    

    Vector2Int direcaoGrid = ObterDirecaoGrid();
    Vector2Int proximaSala = salaAtual.posicaoGrid + direcaoGrid;

    if (!dungeon.ExisteSalaNessaDirecao(proximaSala))
    {
        Debug.Log("Não existe sala nessa direção");
        return;
    }

    podeTeleportar = false;
    emTransicao = true;

    StartCoroutine(SequenciaTeleporte(other.transform));
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
        Vector3 movimento = Vector3.zero;

        switch (direcao)
        {
            case DirecaoPorta.Cima:
                movimento = Vector3.up;

                Vector3 deslocamentoUp =
                movimento * distanciaEntreSalasVertical +
                movimento * offsetPlayer;

                player.position += deslocamentoUp;

                Camera.main.transform.position +=
                movimento * distanciaEntreSalasVertical;

                break;

            case DirecaoPorta.Baixo:
                movimento = Vector3.down;

                Vector3 deslocamentoDown =
                movimento * distanciaEntreSalasVertical +
                movimento * offsetPlayer;

                player.position += deslocamentoDown;

                Camera.main.transform.position +=
                movimento * distanciaEntreSalasVertical;

                break;

            case DirecaoPorta.Esquerda:
                movimento = Vector3.left;

                Vector3 deslocamentoLeft =
                movimento * distanciaEntreSalas +
                movimento * offsetPlayer;

                player.position += deslocamentoLeft;

                Camera.main.transform.position +=
                movimento * distanciaEntreSalas;

                break;

            case DirecaoPorta.Direita:
                movimento = Vector3.right;
                Vector3 deslocamentoRight =
                movimento * distanciaEntreSalas +
                movimento * offsetPlayer;

                player.position += deslocamentoRight;

                Camera.main.transform.position +=
                movimento * distanciaEntreSalas;

                break;
        }
        

    }
    IEnumerator SequenciaTeleporte(Transform alvo)
    {
    player.emTeleporte = true;
    player.iframeAtivo = true;
    player.podeMover = false;

    tt.FadeOut();

    yield return new WaitForSeconds(0.8f);

    Teleportar(alvo);

    yield return new WaitForSeconds(0.5f);

    player.iframeAtivo = false;
    player.podeMover = true;
    player.emTeleporte = false;

    emTransicao = false;
    }

    private void ReativarTrigger()
    {
        podeTeleportar = true;
    }
}