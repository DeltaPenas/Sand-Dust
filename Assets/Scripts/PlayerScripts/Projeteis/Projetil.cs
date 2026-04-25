using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 150f;

    private Vector2 direcao;
    private WepAtaque wp;

    public float vidaProjetil;
    private int ricochetesRestantes;

    private void Start()
    {
        wp = FindAnyObjectByType<WepAtaque>();

        

        // pega do player 
        ricochetesRestantes = wp.pc.currentStatus.ricochetes;

        Destroy(gameObject, 10f);
    }

    public void definirDireção(Vector2 novaDireção)
    {
        direcao = novaDireção.normalized;
    }

    private void Update()
    {
        transform.Translate(direcao * velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D alvo)
    {
        vidaProjetil = wp.pc.currentStatus.danoRanged;
        // acertou o inimigo
        if (alvo.CompareTag("inimigo"))
        {
            Vida vida = alvo.GetComponent<Vida>();

            if (vida != null)
            {
                float dano = wp.pc.currentStatus.danoRanged;

                vida.receberDano(dano);
                wp.pc.DispararOnHit(alvo.gameObject);

                vidaProjetil -= vida.vidaAtual;
            }

            if (vidaProjetil <= 0)
            {
                Destroy(gameObject);
                return;
            }

            TentarRicochete(alvo);
        }

        // bateu na parede
        else if (alvo.CompareTag("Parede"))
        {
            TentarRicochete(alvo);
        }
    }

    void TentarRicochete(Collider2D alvo)
{
    wp.pc.DispararOnRicochete(gameObject);

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