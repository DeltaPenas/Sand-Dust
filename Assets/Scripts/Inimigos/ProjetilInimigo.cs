using UnityEngine;

public class ProjetilInimigo : MonoBehaviour // fiz mudanças para o projetil evitar depender do inimigo e 
// evitar algum bug
{
private Vector2 direcao;
private float velocidade;
private int dano;
private float tempoIgnorarColisao = 0.1f; 
private float tempoSpawn;

private void Start()
    {
        tempoSpawn = Time.time;
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
        if (Time.time < tempoSpawn + tempoIgnorarColisao)
        return;
        
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