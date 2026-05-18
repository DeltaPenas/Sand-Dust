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
private Vector2 posicaoAnterior;

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

protected Animator anim;
protected Vector2 ultimaDirecao;

protected virtual void Start()
    {
        anim = GetComponent<Animator>();
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
        if (DeveParar())
        {
            rb.linearVelocity = Vector2.zero;

            if (anim != null)
            {
                anim.SetFloat("Speed", 0);
            }

            return;
        }
        Vector2 direcao = DirecaoBase();

        //  SEMPRE desvia
        direcao = ObterDirecaoComDesvio(direcao);

        if (direcao == Vector2.zero)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        float deslocamento = Vector2.Distance(rb.position, posicaoAnterior);

        if (deslocamento < 0.001f)
        {
            direcao = Random.insideUnitCircle.normalized;
        }
        
        rb.linearVelocity = direcao * velocidade;

        posicaoAnterior = rb.position;

        if (direcao != Vector2.zero)
        {
            ultimaDirecao = direcao;
        }

        if (anim != null)
        {
            anim.SetFloat("Horizontal", ultimaDirecao.x);
            anim.SetFloat("Vertical", ultimaDirecao.y);
            anim.SetFloat("Speed", direcao.magnitude);
        }
    }
protected virtual bool DeveParar()
    {
        return distanciaPlayer <= distanciaParada;
    }// se o player estiver dentro da distância de parada, o inimigo para de se mover
    // Pode ser sobrescrito (ex: ranged foge, melee aproxima)
protected virtual Vector2 DirecaoBase()
    {
        return (player.position - transform.position).normalized;
    }
protected Vector2 ObterDirecaoComDesvio(Vector2 direcaoBase)
    {
        if (direcaoBase == Vector2.zero)
            return Vector2.zero;

        float raio = 0.3f;

        RaycastHit2D hit = Physics2D.CircleCast(
            rb.position,
            raio,
            direcaoBase,
            distanciaDeteccaoObstaculo,
            layerObstaculos
        );

        if (hit.collider == null)
            return direcaoBase;

        // mistura fugir da parede + deslizar
        Vector2 normal = hit.normal;

        Vector2 fugir = normal; // empurra pra fora da parede
        Vector2 deslizar = new Vector2(normal.y, -normal.x);

        if (Vector2.Dot(deslizar, direcaoBase) < 0)
            deslizar *= -1;

        // mistura os dois
        return (deslizar + fugir * 0.5f).normalized;
    }
}