using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 10f;

    private Vector2 direcao;
    private PlayerController pc;

    private float vidaProjetil;
    private int ricochetesRestantes;

    public bool podeGerarRicocheteExtra = true;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Destroy(gameObject, 10f);
    }

    public void Inicializar(Vector2 dir, PlayerController player)
    {
        pc = player;

        direcao = dir.normalized;

        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angulo);

        vidaProjetil = pc.currentStatus.danoRanged;
        ricochetesRestantes = pc.currentStatus.ricochetes;

        rb.linearVelocity = direcao * velocidade;
    }

    void OnTriggerEnter2D(Collider2D alvo)
    {
        if (alvo.CompareTag("inimigo"))
        {
            Vida vida = alvo.GetComponent<Vida>();

            if (vida != null)
            {
                float dano = pc.currentStatus.danoRanged;

                vida.receberDano(dano);

                pc.DispararOnHit(alvo.gameObject);

                vidaProjetil -= dano;
            }

            if (vidaProjetil <= 0)
            {
                Destroy(gameObject);
                return;
            }

            TentarRicochete(alvo);
        }
        else if (alvo.CompareTag("Parede"))
        {
            TentarRicochete(alvo);
        }
    }

    void TentarRicochete(Collider2D alvo)
    {
        if (podeGerarRicocheteExtra)
        {
            podeGerarRicocheteExtra = false;
            pc.DispararOnRicochete(gameObject);
        }

        if (ricochetesRestantes <= 0)
        {
            Destroy(gameObject);
            return;
        }

        ricochetesRestantes--;

        Vector2 ponto = alvo.ClosestPoint(transform.position);
        Vector2 normal = (transform.position - (Vector3)ponto).normalized;

        direcao = Vector2.Reflect(direcao, normal);

        rb.linearVelocity = direcao * velocidade;
    }
}