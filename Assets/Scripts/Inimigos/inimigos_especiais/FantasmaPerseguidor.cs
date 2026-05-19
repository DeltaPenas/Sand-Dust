using System.Collections;
using UnityEngine;

public class FantasmaPerseguidor : InimigoBase
{
    private bool estaMorto = false;
    private Coroutine cicloFantasmaCoroutine;
    private enum EstadoFantasma
    {
        Intangivel,
        Vulneravel,
        Invisivel
    }

    [Header("Fantasma - Estados")]
    [SerializeField] private float tempoIntangivel = 3f;
    [SerializeField] private float tempoVulneravel = 1.2f;
    [SerializeField] private float tempoInvisivel = 0.8f;

    [Header("Ataque")]
    [SerializeField] private int dano = 1;
    [SerializeField] private float alcanceAtaque = 1.1f;
    [SerializeField] private float intervaloAtaque = 2f;

    [Header("Reposicionamento")]
    [SerializeField] private float distanciaReaparecerPertoPlayer = 2f;
    [SerializeField] private BoxCollider2D limitesDaSala;
    [SerializeField] private LayerMask layerBloqueioReposicionamento;

    [Header("Visual / Colisão")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D colliderFantasma;
    [SerializeField] private Animator anim;

    private EstadoFantasma estadoAtual;
    private float proximoAtaque;

    protected override void Start()
    {
        base.Start();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (colliderFantasma == null)
            colliderFantasma = GetComponent<Collider2D>();

        if (anim == null)
            anim = GetComponent<Animator>();

        cicloFantasmaCoroutine = StartCoroutine(CicloFantasma());
    }

    protected override void Comportamento()
    {
        if (estaMorto || vida == null || vida.morreu)
    {
        DesativarFantasmaMorto();
        return;
    }
        if (player == null) return;

        if (estadoAtual == EstadoFantasma.Invisivel)
            return;

        if (distanciaPlayer <= alcanceAtaque && Time.time >= proximoAtaque)
        {
            AtacarPlayer();
            proximoAtaque = Time.time + intervaloAtaque;
        }
    }

    private void DesativarFantasmaMorto()
    {
        if (estaMorto) return;

        estaMorto = true;

        if (cicloFantasmaCoroutine != null)
        {
            StopCoroutine(cicloFantasmaCoroutine);
        }

        rb.linearVelocity = Vector2.zero;

        if (colliderFantasma != null)
        {
            colliderFantasma.enabled = false;
        }

        enabled = false;
    }

    protected override bool DeveParar()
    {
        return distanciaPlayer <= alcanceAtaque;
    }

    private IEnumerator CicloFantasma()
    {
        while (true)
        {
            EntrarIntangivel();
            yield return new WaitForSeconds(tempoIntangivel);
            Debug.Log("INTANGIVEL");

            EntrarVulneravel();
            yield return new WaitForSeconds(tempoVulneravel);
            Debug.Log("VULNERAVEL");

            EntrarInvisivel();
            yield return new WaitForSeconds(tempoInvisivel);
            Debug.Log("INVISIVEL");

            ReaparecerPertoDoPlayer();
        }
    }

    private void EntrarIntangivel()
    {
        estadoAtual = EstadoFantasma.Intangivel;
        gameObject.tag = "Untagged";

        if (spriteRenderer != null)
        {
            Color cor = spriteRenderer.color;
            cor.a = 0.45f;
            spriteRenderer.color = cor;
        }

        if (vida != null)
        {
            vida.invuneravel = true;
        }
    }

    private void EntrarVulneravel()
    {
        estadoAtual = EstadoFantasma.Vulneravel;
        Debug.Log("ENTROU VULNERAVEL");
        Debug.Log(vida.invuneravel);
        gameObject.tag = "inimigo";

        if (spriteRenderer != null)
        {
            Color cor = spriteRenderer.color;
            cor.a = 1f;
            spriteRenderer.color = cor;
        }

        if (vida != null)
        {
            vida.invuneravel = false;
        }
    }

    private void EntrarInvisivel()
    {
        estadoAtual = EstadoFantasma.Invisivel;
        gameObject.tag = "Untagged";
        rb.linearVelocity = Vector2.zero;

        if (spriteRenderer != null)
            spriteRenderer.enabled = false;

        if (colliderFantasma != null)
            colliderFantasma.enabled = false;
    }

    private void ReaparecerPertoDoPlayer()
    {
        if (estaMorto || vida == null || vida.morreu)
            return;
        if (player == null) return;

        Vector2 direcaoAleatoria = Random.insideUnitCircle.normalized;
        Vector2 novaPosicao = (Vector2)player.position + direcaoAleatoria * distanciaReaparecerPertoPlayer;

        transform.position = novaPosicao;

        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

        if (colliderFantasma != null)
            colliderFantasma.enabled = true;

        EntrarIntangivel();
    }

    private void AtacarPlayer()
    {
        if (estaMorto || vida == null || vida.morreu)
            return;
        if (vidaDoPlayer == null) return;

        anim.SetTrigger("Ataque");
        vidaDoPlayer.DarDanoPlayer(dano);

        Debug.Log("Fantasma atingiu o player!");

        RecuarParaPosicaoAleatoria();
    }

    private void RecuarParaPosicaoAleatoria()
    {
        if (estaMorto || vida == null || vida.morreu)
            return;

        Vector2 novaPosicao = ObterPosicaoAleatoriaNaSala();
        transform.position = novaPosicao;
    }

    private Vector2 ObterPosicaoAleatoriaNaSala()
    {
        if (limitesDaSala == null)
        {
            return rb.position + Random.insideUnitCircle.normalized * 3f;
        }

        Bounds bounds = limitesDaSala.bounds;

        for (int i = 0; i < 20; i++)
        {
            Vector2 ponto = new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y)
            );

            bool estaBloqueado = Physics2D.OverlapCircle(
                ponto,
                0.4f,
                layerBloqueioReposicionamento
            );

            if (!estaBloqueado)
                return ponto;
        }

        return rb.position;
    }
}