using UnityEngine;

public class InimigoAtirador : InimigoBase
{
[Header("Ataque")]
public GameObject projetilPrefab;
public Transform pontoDisparo;
public float tempoEntreTiros = 1.5f;
public int dano;
public float velocidadeProjetil;
private float proximoTiro;
public float distanciaMinima = 1.5f;
public float distanciaAtaque = 6f;
[Header("Visão")]
public LayerMask layerObstaculos;

protected override void Comportamento()
    {
        if (player == null) return;

        if (PodeAtirar())
        {
            Atirar();
            proximoTiro = Time.time + tempoEntreTiros;
        }
    }

bool PodeAtirar()
    {
        return distanciaPlayer <= distanciaAtaque && 
        Time.time >= proximoTiro && 
        TemLinhaDeVisao();//só atira se o player estiver dentro da distância de ataque, o tempo entre tiros tiver passado e houver linha de visão
    }

void Atirar()
    {
        if (projetilPrefab == null || pontoDisparo == null || vida.vidaAtual <= 0) return;

        GameObject tiro = Instantiate(projetilPrefab, pontoDisparo.position, Quaternion.identity);

        Vector2 direcao = (player.position - pontoDisparo.position).normalized;

        ProjetilInimigo proj = tiro.GetComponent<ProjetilInimigo>();

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

        return hit.collider == null;
    }//cria uma linha de visão entre o inimigo e o player, se houver um obstáculo no caminho, o inimigo não atira

protected override Vector2 DirecaoBase()
    {

        if (distanciaPlayer < distanciaMinima)
            return (transform.position - player.position).normalized;// se o player estiver muito perto, o inimigo se afasta

        if (distanciaPlayer > distanciaAtaque)
            return (player.position - transform.position).normalized;// se o player estiver longe, o inimigo se move em direção a ele

        return Vector2.zero;
    }
}