using UnityEngine;

public abstract class InimigoBase : MonoBehaviour // isso é a minha tentativa de criar uma classe pai unica tanto para mele quanto para ranged 
// pq achei meio redundante criar uma classe pai para cada tipo de inimigo, mas se for necessário eu posso separar tranquilo
{
[Header("Referências")]
[HideInInspector] public InimigoInvocador invocador;
protected Transform player;
protected PlayerVida vidaDoPlayer;
protected Rigidbody2D rb;
protected Vida vida;
protected float distanciaPlayer;

[Header("Movimento")]
public float velocidade = 1f;

[Header("Distâncias")]
public float distanciaParada = 1.5f;

[Header("Desvio")]  
public float distanciaDeteccaoObstaculo = 1.5f;
public LayerMask layerObstaculos;

[Header("Persistência Desvio")]
public float tempoDesvio = 0.7f;
protected Vector2 direcaoDesvioAtual;
protected float fimDesvio;

protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vida = GetComponent<Vida>();

        GameObject alvo = GameObject.FindGameObjectWithTag("Player");
        if (alvo != null)
        {
            player = alvo.transform;
            vidaDoPlayer = player.GetComponent<PlayerVida>();
        }

        if (rb == null)
        {
            Debug.LogError($"{name} está sem Rigidbody2D!");
            enabled = false;
            return;
        }//caso o rigidbody2D não seja encontrado, desativa o inimigo e mostra um erro no console
    }

protected virtual void Update()
    {
        if (player == null) return;

        distanciaPlayer = Vector2.Distance(transform.position, player.position);

        Comportamento();
    }
protected virtual void FixedUpdate()
    {
        if (player == null) return;
        Movimento();
    }// Cada inimigo decide o que fazer
protected abstract void Comportamento();

protected virtual void Movimento()
{
    if (distanciaPlayer < distanciaParada)
    {
        rb.linearVelocity = Vector2.zero;
        return;
    }

    Vector2 direcao = DirecaoBase();
    direcao = ObterDirecaoComDesvio(direcao);

    if (direcao == Vector2.zero)
    {
        rb.linearVelocity = Vector2.zero;
        return;
    }

    rb.MovePosition(rb.position + direcao * velocidade * Time.fixedDeltaTime);
}
// Pode ser sobrescrito (ex: ranged foge, melee aproxima)
protected virtual Vector2 DirecaoBase()
    {
        return (player.position - transform.position).normalized;
    }

protected Vector2 ObterDirecaoComDesvio(Vector2 direcaoBase)   
    {
        if (direcaoBase == Vector2.zero)
        return Vector2.zero;

        if (Time.time < fimDesvio)
            return direcaoDesvioAtual;

        float raio = 0.3f;

        RaycastHit2D hitFrente = Physics2D.CircleCast(
            rb.position,
            raio,
            direcaoBase,
            distanciaDeteccaoObstaculo,
            layerObstaculos
        );

        if (hitFrente.collider == null)
            return direcaoBase;

        Vector2 esquerda = new Vector2(-direcaoBase.y, direcaoBase.x).normalized;
        Vector2 direita = new Vector2(direcaoBase.y, -direcaoBase.x).normalized;

        bool esquerdaLivre = !Physics2D.CircleCast(rb.position, raio, esquerda, distanciaDeteccaoObstaculo, layerObstaculos);
        bool direitaLivre = !Physics2D.CircleCast(rb.position, raio, direita, distanciaDeteccaoObstaculo, layerObstaculos);

        if (esquerdaLivre)
            direcaoDesvioAtual = esquerda;
        else if (direitaLivre)
            direcaoDesvioAtual = direita;
        else
            return Vector2.zero;

        fimDesvio = Time.time + tempoDesvio;
        return direcaoDesvioAtual;
    }
}