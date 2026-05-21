using System.Collections;
using UnityEngine;

public class MiniBossSaltador : InimigoBase
{
    [Header("Referencias")]
    [SerializeField] private Transform spriteVisual;
    [SerializeField] private GameObject sombra;

    [Header("Salto")]
    [SerializeField] private float alturaSalto = 2f;
    [SerializeField] private float duracaoSalto = 0.8f;
    [SerializeField] private float velocidadeSalto = 12f;

    [SerializeField] private float distanciaAtaqueSalto = 6f;

    [Header("Impacto")]
    [SerializeField] private float raioImpacto = 3f;
    [SerializeField] private int danoImpacto = 1;
    [SerializeField] private LayerMask layerPlayer;

    [Header("Dash")]
    [SerializeField] private float velocidadeDash = 18f;
    [SerializeField] private float duracaoDash = 0.25f;
    [SerializeField] private int danoDash = 1;
    [SerializeField] private float distanciaDash = 5f;

    [Header("Cooldown")]
    [SerializeField] private float cooldownAtaques = 2f;

    private bool ocupado;
    private float proximoAtaque;

    protected override void Comportamento()
{
    if (player == null || ocupado)
        return;

    if (Time.time < proximoAtaque)
        return;

    if (distanciaPlayer <= distanciaAtaqueSalto)
    {
        int ataque = Random.Range(0, 2);

        if (ataque == 0)
            StartCoroutine(SaltoAtaque());
        else
            StartCoroutine(DashAtaque());
            Debug.Log("Dash");
    }
}

    protected override bool DeveParar()
    {
        return ocupado;
    }

    IEnumerator SaltoAtaque()
    {
        ocupado = true;
        proximoAtaque = Time.time + cooldownAtaques;

        rb.linearVelocity = Vector2.zero;

        Vector2 origem = transform.position;
        Vector2 destino = player.position;

        float tempo = 0;

        while (tempo < duracaoSalto)
        {
            tempo += Time.deltaTime;

            float progresso = tempo / duracaoSalto;

            // movimenta no chão
            Vector2 novaPos = Vector2.Lerp(origem, destino, progresso);
            rb.MovePosition(novaPos);

            // arco visual
            float altura = Mathf.Sin(progresso * Mathf.PI) * alturaSalto;

            spriteVisual.localPosition = new Vector3(0, altura, 0);

            yield return null;
        }

        spriteVisual.localPosition = Vector3.zero;

        ImpactoArea();

        ocupado = false;
    }

    void ImpactoArea()
    {
        Collider2D hit = Physics2D.OverlapCircle(
            transform.position,
            raioImpacto,
            layerPlayer
        );

        if (hit != null)
        {
            PlayerVida vidaPlayer = hit.GetComponent<PlayerVida>();

            if (vidaPlayer != null)
            {
                vidaPlayer.DarDanoPlayer(danoImpacto);
            }
        }
    }

    IEnumerator DashAtaque()
{
    ocupado = true;
    proximoAtaque = Time.time + cooldownAtaques;

    Vector2 direcao = (player.position - transform.position).normalized;

    float tempo = 0;

    while (tempo < duracaoDash)
    {
        tempo += Time.deltaTime;

        Vector2 novaPos =
            rb.position +
            direcao * velocidadeDash * Time.deltaTime;

        rb.MovePosition(novaPos);

        yield return new WaitForFixedUpdate();
    }

    rb.linearVelocity = Vector2.zero;

    Collider2D hit = Physics2D.OverlapCircle(
        transform.position,
        1f,
        layerPlayer
    );

    if (hit != null)
    {
        PlayerVida vidaPlayer = hit.GetComponent<PlayerVida>();

        if (vidaPlayer != null)
        {
            vidaPlayer.DarDanoPlayer(danoDash);
        }
    }

    ocupado = false;
}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raioImpacto);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distanciaDash);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaAtaqueSalto);
    }
}