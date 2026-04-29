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

    protected override void Comportamento()
    {
        if (player == null) return;

        float distancia = Vector2.Distance(transform.position, player.position);

        if (PodeAtirar(distancia))
        {
            Atirar();
            proximoTiro = Time.time + tempoEntreTiros;
        }
    }

    bool PodeAtirar(float distancia)
    {
        return distancia <= 4f && Time.time >= proximoTiro;
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

    protected override Vector2 DirecaoBase()
    {
        float distancia = Vector2.Distance(transform.position, player.position);

        if (distancia < 2.5f)
            return (transform.position - player.position).normalized;

        if (distancia > 4f)
            return (player.position - transform.position).normalized;

        return Vector2.zero;
    }
}