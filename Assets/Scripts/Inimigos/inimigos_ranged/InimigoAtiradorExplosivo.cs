using UnityEngine;

public class InimigoAtiradorExplosivo : InimigoBase
{
[Header("Ataque")]
public GameObject projetilPrefab;
public Transform pontoDisparo;
public float tempoEntreTiros = 1.5f;
public int dano;
public float velocidadeProjetil;
private float proximoTiro;
public float distanciaMinima = 1.5f;
public float distanciaAtaque = 8f;
private float direcaoStrafe = 1f;
private float tempoTrocaStrafe = 0f;


protected override void Comportamento()
{
    if (player == null) return;

    if (Time.time >= proximoTiro && TemLinhaDeVisao())
    {
        Atirar();
        proximoTiro = Time.time + tempoEntreTiros;
    }
}
void Atirar()
    {
        if (projetilPrefab == null || pontoDisparo == null || vida.vidaAtual <= 0) return;

        Vector2 direcao = (player.position - pontoDisparo.position).normalized;

        Vector2 spawnPos = (Vector2)pontoDisparo.position + direcao * 0.6f;

        GameObject tiro = Instantiate(projetilPrefab, spawnPos, Quaternion.identity);

        Collider2D colProjetil = tiro.GetComponent<Collider2D>();
        Collider2D colInimigo = GetComponent<Collider2D>();

        if (colProjetil != null && colInimigo != null)
        {
            Physics2D.IgnoreCollision(colProjetil, colInimigo);
        }

        ProjetilInimgioExplosivo proj = tiro.GetComponent<ProjetilInimgioExplosivo>();

        if (proj != null)
        {
            proj.Inicializar(direcao, velocidadeProjetil, dano);
        }
    }
bool TemLinhaDeVisao()
    {
        Vector2 origem = pontoDisparo.position;
        Vector2 direcao = (player.position - pontoDisparo.position).normalized;

        float distancia = Vector2.Distance(origem, player.position);

        RaycastHit2D hit = Physics2D.Raycast(
            origem,
            direcao,
            distancia,
            layerObstaculos
        );

        return hit.collider == null || hit.collider.CompareTag("Player");
    }//cria uma linha de visão entre o inimigo e o player, se houver um obstáculo no caminho, o inimigo não atira
protected override bool DeveParar()
    {
        return false;// o atirador nunca para de se mover, ele pode atirar mesmo se estiver se movendo
    }
protected override Vector2 DirecaoBase()
    {
        Vector2 direcaoPlayer = (player.position - transform.position).normalized;

        // Muito perto ele foge
        if (distanciaPlayer < distanciaMinima)
        {
            return -direcaoPlayer;
        }

        // Muito longe ele se aproxima
        if (distanciaPlayer > distanciaAtaque)
        {
            return direcaoPlayer;
        }

        if (Time.time > tempoTrocaStrafe)
        {
            direcaoStrafe *= -1f;
            tempoTrocaStrafe = Time.time + Random.Range(1f, 2.5f);
        //  Distância ideal para strafe (movimento lateral)
        // troca direção a cada X segundos
        }

        // vetor perpendicular
        Vector2 perpendicular = new Vector2(-direcaoPlayer.y, direcaoPlayer.x);

        Vector2 ajuste = direcaoPlayer * 0.3f;// ajuste para manter distância ideal

        return (perpendicular * direcaoStrafe + ajuste).normalized;
    }
}