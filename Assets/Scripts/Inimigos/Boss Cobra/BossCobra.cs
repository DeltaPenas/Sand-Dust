using System.Collections.Generic;
using UnityEngine;

public class BossCobra : InimigoBase
{
    [Header("Referencias")]
    [SerializeField] private List<GameObject> segmentos;
    [SerializeField] public BossState currentState = BossState.Idle;



    public float distanciaAtaque = 1.5f;
    public float tempoEntreAtaques = 1f;
    public int dano = 0;

    private float proximoAtaque;

    public float velocidadeBoss = 1f;

    protected override void Start()
    {
        base.Start();

        proximoAtaque = Time.time + 0.5f;

        velocidade = velocidadeBoss;
    }

    protected override void Comportamento()
    {
        if (player == null) return;

        if (distanciaPlayer <= distanciaAtaque &&
            Time.time >= proximoAtaque)
        {
            AtacarMelee();

            proximoAtaque = Time.time + tempoEntreAtaques;
            
        }

        Vector3 direcao = player.position - transform.position;
        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angulo);


    }

    protected override Vector2 DirecaoBase()
    {
        if (distanciaPlayer > distanciaAtaque)
        {
            return (player.position - transform.position).normalized;
        }

        return Vector2.zero;
    }

    void AtacarMelee()
    {
        if (vidaDoPlayer != null)
        {
            vidaDoPlayer.DarDanoPlayer(dano);
        }
    }
    public void TrocarEstado(BossState novoEstado)
    {
        currentState = novoEstado;
    }
}