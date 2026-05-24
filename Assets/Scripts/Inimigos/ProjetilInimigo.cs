using UnityEngine;

public class ProjetilInimigo : MonoBehaviour
{
    private Vector2 direcao;
    private float velocidade;
    private int dano;

    public void Inicializar(Vector2 direcaoInicial, float velocidadeProjetil, int danoProjetil)
    {
        direcao = direcaoInicial.normalized;
        velocidade = velocidadeProjetil;
        dano = danoProjetil;

        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        transform.Translate(direcao * velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D alvo)
    {
        PlayerVida vida = alvo.GetComponent<PlayerVida>();

        if (vida != null)
        {
            vida.DarDanoPlayer(dano);
        }

        if (!alvo.CompareTag("inimigo") && !alvo.CompareTag("Chão"))
        {
            Destroy(gameObject);
        }
    }
}