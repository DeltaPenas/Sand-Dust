using UnityEngine;

public class InimigoAtiradorCurtaDistancia : InimigoBase //eu fiz ele atirando em cone para dar uma diferenciada para simular uma escopeta  
{
[Header("Ataque")]
public GameObject projetilPrefab;
public Transform pontoDisparo;

public int quantidadeTiros = 5;
public float anguloTotal = 60f;
public float tempoEntreAtaques = 2f;

public float distanciaAtaque = 5f;
public float velocidadeProjetil;
public int dano;

private float tempoProximoAtaque;

protected override void Comportamento()
{
    if (player == null) return;

    if (distanciaPlayer <= distanciaAtaque && Time.time >= tempoProximoAtaque)
    {
        AtirarEmCone();
        tempoProximoAtaque = Time.time + tempoEntreAtaques; 
    }
}
protected override Vector2 DirecaoBase()
    {
        if (distanciaPlayer > distanciaAtaque)
            return (player.position - transform.position).normalized;// se o player estiver longe, o inimigo se move em direção a ele

        return Vector2.zero; // isso diz que o iniigo tem que parar para atirar no player 
    }
protected override bool DeveParar()
    {
        return distanciaPlayer <= distanciaAtaque;
    }
void AtirarEmCone()
    {
        if (projetilPrefab == null || pontoDisparo == null) return;

        Vector2 direcaoBase = (player.position - pontoDisparo.position).normalized;

        float anguloInicial = -anguloTotal / 2f;
        float incremento = (quantidadeTiros > 1) ? anguloTotal / (quantidadeTiros - 1) : 0;

        for (int i = 0; i < quantidadeTiros; i++)
        {
            float angulo = anguloInicial + (incremento * i);
                   
            Vector2 direcaoRotacionada = Quaternion.Euler(0, 0, angulo) * direcaoBase;

            Vector2 spawnPos = (Vector2)pontoDisparo.position + direcaoRotacionada * 0.5f;

            GameObject proj = Instantiate(projetilPrefab, spawnPos, Quaternion.identity);

            ProjetilInimigo p = proj.GetComponent<ProjetilInimigo>();

            if (p != null)
            {
                p.Inicializar(direcaoRotacionada, velocidadeProjetil, dano);
            }
        }
        Debug.Log("ATIROU");
    }
}