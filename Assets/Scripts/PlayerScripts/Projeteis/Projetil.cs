using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 150f;

    private Vector2 direcao;
    private WepAtaque wp;

    private float vidaProjetil;
    private int ricochetesRestantes;
    public bool podeGerarRicocheteExtra = true;

    private bool jaDisparouRicochete = false; // 🔥 trava de evento

    void Start()
    {
        wp = FindAnyObjectByType<WepAtaque>();

        float dano = wp.pc.currentStatus.danoRanged;

        vidaProjetil = dano; // ✔ define UMA VEZ
        ricochetesRestantes = wp.pc.currentStatus.ricochetes;

        Destroy(gameObject, 10f);
    }

    public void definirDireção(Vector2 novaDireção)
    {
        direcao = novaDireção.normalized;
    }

    void Update()
    {
        transform.Translate(direcao * velocidade * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D alvo)
    {
        // 🔥 INIMIGO
        if (alvo.CompareTag("inimigo"))
        {
            Vida vida = alvo.GetComponent<Vida>();

            if (vida != null)
            {
                float dano = wp.pc.currentStatus.danoRanged;

                vida.receberDano(dano);
                wp.pc.DispararOnHit(alvo.gameObject);

                vidaProjetil -= dano; // ✔ CORRETO
            }

            if (vidaProjetil <= 0)
            {
                Destroy(gameObject);
                return;
            }

            TentarRicochete(alvo);
        }

        // 🔥 PAREDE
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
        wp.pc.DispararOnRicochete(gameObject);
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
}
}