using UnityEngine;

public class ProjetilInimigo : MonoBehaviour // fiz mudanças para o projetil evitar depender do inimigo e 
// evitar algum bug
{
private Vector2 direcao;
private float velocidade;
private int dano;

private void Start()
    {
        Destroy(gameObject, 3f);
    }

public void Inicializar(Vector2 direcaoInicial, float velocidadeProjetil, int danoProjetil)
    {
        direcao = direcaoInicial.normalized;
        velocidade = velocidadeProjetil;
        dano = danoProjetil;
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