using UnityEngine;

public class ProjetilInimigoBoomerang : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidadeIda = 8f;
    public float rotacaoInterna = 600f;
    public float velocidadeVolta = 10f;
    public float tempoIda = 1f;
    public int dano = 1;
    private Vector2 direcao;
    private Transform dono;

    private bool voltando;

    private Rigidbody2D rb;

    public void Inicializar(Vector2 dir, Transform donoTransform)
    {
        direcao = dir.normalized;
        dono = donoTransform;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Invoke(nameof(IniciarRetorno), tempoIda);
    }

    private void Update()
    {
        transform.Rotate(0, 0, rotacaoInterna * Time.deltaTime);
        if (!voltando)
        {
            rb.linearVelocity = direcao * velocidadeIda;
        }
        else
        {
            if (dono == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector2 dirVolta =
                ((Vector2)dono.position - rb.position).normalized;

            rb.linearVelocity = dirVolta * velocidadeVolta;
        }
    }

    void IniciarRetorno()
    {
        voltando = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerVida vida = other.GetComponent<PlayerVida>();

        if (vida != null)
        {
            vida.DarDanoPlayer((int)(dano+RunManager.Instance.currentRun.inimigoBuffDano));
        }


        if (voltando && other.transform == dono)
        {
            Destroy(gameObject);
        }
    }
}