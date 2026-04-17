using UnityEngine;

public class InimigoPerseguidor : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;
    private PlayerVida vidaDoPlayer;
    private Rigidbody2D rb;
    private SalaController salaOrigem;
    private InimigoController ic;
    private Vida vida;

    [Header("Movimento")]
    [SerializeField] private float forçaDashInimigo;
    public bool capazDash;
    public float velocidade = 1f;
    public float distanciaParada = 0.9f; // distância necessária para que o inimigo fique parado perto do player

    [Header("Desvio Local")]
    public float distanciaDeteccaoObstaculo = 1.5f;
    public LayerMask layerObstaculos;

    [Header("Persistência do Desvio")]
    public float tempoDesvio = 0.7f;
    private Vector2 direcaoDesvioAtual;
    private float fimDesvio;

    [Header("Ataque")]
    public int dano = 1;
    public float alcanceDano = 1.5f;
    public float intervaloDano = 3f;

    private float proximoAtaque;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vida = GetComponent<Vida>();

        if (player == null)
        {
            GameObject alvo = GameObject.FindGameObjectWithTag("Player");

            if (alvo != null)
            {
                player = alvo.transform;
            }
        }

        if (player != null)
        {
            vidaDoPlayer = player.GetComponent<PlayerVida>();
        }
        else
        {
            Debug.LogWarning("Player não encontrado. Verifique a tag 'Player'.");
        }
    }

    private void Update()
    {
        if (player == null) return;

        float distancia = Vector2.Distance(transform.position, player.position);


        if (distancia <= alcanceDano && Time.time >= proximoAtaque)
        {
            Atacar();
            DashInimigo();
            proximoAtaque = Time.time + intervaloDano;
        }
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        float distancia = Vector2.Distance(rb.position, player.position);

        if (distancia > alcanceDano){
            Vector2 direcaoEscolhida = ObterDirecaoDeMovimento();
            Vector2 novaPosicao = rb.position + direcaoEscolhida * velocidade * Time.fixedDeltaTime;
            rb.MovePosition(novaPosicao);
        }
    }


    private Vector2 ObterDirecaoDeMovimento()
    {
        
        // Se ainda estiver em modo de desvio, mantém a direção escolhida
        if (Time.time < fimDesvio)
        {
            return direcaoDesvioAtual;
        }

        // Direção principal: do inimigo até o player
        Vector2 direcaoParaPlayer = ((Vector2)player.position - rb.position).normalized;

        // Testa se há obstáculo exatamente na frente
        float raio = 0.3f; // tamanho do "corpo" do inimigo
        RaycastHit2D hitFrente = Physics2D.CircleCast(
            rb.position,
            raio,
            direcaoParaPlayer,
            distanciaDeteccaoObstaculo,
            layerObstaculos
        );

        // Se não bateu em nada, segue reto
        if (hitFrente.collider == null)
        {
            return direcaoParaPlayer;
        }

        // Cria duas direções perpendiculares
        Vector2 direcaoEsquerda = new Vector2(-direcaoParaPlayer.y, direcaoParaPlayer.x).normalized;
        Vector2 direcaoDireita = new Vector2(direcaoParaPlayer.y, -direcaoParaPlayer.x).normalized;

        Vector2 origemEsquerda = rb.position + direcaoEsquerda * 0.2f;
        Vector2 origemDireita = rb.position + direcaoDireita * 0.2f;

        RaycastHit2D hitEsquerda = Physics2D.CircleCast(
            origemEsquerda,
            raio,
            direcaoEsquerda,
            distanciaDeteccaoObstaculo,
            layerObstaculos
        );

        RaycastHit2D hitDireita = Physics2D.CircleCast(
            origemDireita,
            raio,
            direcaoDireita,
            distanciaDeteccaoObstaculo,
            layerObstaculos
        );

        bool esquerdaLivre = hitEsquerda.collider == null;
        bool direitaLivre = hitDireita.collider == null;

        // Caso 1: só esquerda está livre
        if (esquerdaLivre && !direitaLivre)
        {
            direcaoDesvioAtual = direcaoEsquerda;
            fimDesvio = Time.time + tempoDesvio;
            return direcaoDesvioAtual;
        }

        // Caso 2: só direita está livre
        if (direitaLivre && !esquerdaLivre)
        {
            direcaoDesvioAtual = direcaoDireita;
            fimDesvio = Time.time + tempoDesvio;
            return direcaoDesvioAtual;
        }

        // Caso 3: as duas estão livres
        if (esquerdaLivre && direitaLivre)
        {
            float distanciaSeForEsquerda = Vector2.Distance(rb.position + direcaoEsquerda, player.position);
            float distanciaSeForDireita = Vector2.Distance(rb.position + direcaoDireita, player.position);

            direcaoDesvioAtual = distanciaSeForEsquerda < distanciaSeForDireita
                ? direcaoEsquerda
                : direcaoDireita;

            fimDesvio = Time.time + tempoDesvio;
            return direcaoDesvioAtual;
        }
        Debug.Log("Frente: " + (hitFrente.collider != null));
        Debug.Log("Esquerda livre: " + esquerdaLivre);
        Debug.Log("Direita livre: " + direitaLivre);
        Debug.Log("Tudo bloqueado. Inimigo parado.");

        // tenta continuar no último desvio antes de desistir
        if (direcaoDesvioAtual != Vector2.zero)
        {
            return direcaoDesvioAtual;
        }

        return Vector2.zero;
    }

    private void Atacar()
    {
        if (vidaDoPlayer != null)
        {
            vidaDoPlayer.DarDanoPlayer(dano);
            Debug.Log("Inimigo atacou o player! Dano causado: " + dano);
        }
    }
    private void DashInimigo()
    {
        Vector2 direcaoParaPlayerDash = ((Vector2)player.position - rb.position).normalized;
        if (capazDash)
        {
            rb.linearVelocity = direcaoParaPlayerDash * forçaDashInimigo;
        }
        
        
    }

    private void OnDrawGizmosSelected()
    {
        if (player == null) return;

        Vector2 origem = Application.isPlaying && rb != null
            ? rb.position
            : (Vector2)transform.position;

        Vector2 direcaoParaPlayer = ((Vector2)player.position - origem).normalized;
        Vector2 esquerda = new Vector2(-direcaoParaPlayer.y, direcaoParaPlayer.x).normalized;
        Vector2 direita = new Vector2(direcaoParaPlayer.y, -direcaoParaPlayer.x).normalized;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(origem, 0.3f); // mesmo raio do CircleCast

        Gizmos.color = Color.red;
        Gizmos.DrawLine(origem, origem + direcaoParaPlayer * distanciaDeteccaoObstaculo);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(origem, origem + esquerda * distanciaDeteccaoObstaculo);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(origem, origem + direita * distanciaDeteccaoObstaculo);
    }
}