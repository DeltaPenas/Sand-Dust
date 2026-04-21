using UnityEditor.Rendering;
using UnityEngine;

public class InimigoAtirador : InimigoRanged
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
        float distancia = Vector2.Distance(transform.position, player.position);
        
        // Só atira se estiver na distância certa
        if (distancia <= 4f && Time.time >= proximoTiro)
        {
            Atirar();
            proximoTiro = Time.time + tempoEntreTiros;
        }
    }

    void Atirar()
    {
        if (projetilPrefab == null || pontoDisparo == null || inimigoController.vidaInimigo.vidaAtual <= 0) return;

        GameObject tiro = Instantiate(projetilPrefab, pontoDisparo.position, Quaternion.identity);

        Vector2 direcao = (player.position - pontoDisparo.position).normalized;

        Rigidbody2D rbTiro = tiro.GetComponent<Rigidbody2D>();
        if (rbTiro != null)
        {
            rbTiro.linearVelocity = direcao * 5f;
        }
    }
}