using UnityEngine;

public class InimigoInvocadoM: InimigoBase
{
    [Header("Ataque")]
    public float distanciaAtaque = 1.5f;
    public float tempoEntreAtaques = 1f;
    public int dano = 10;
    private float proximoAtaque;
protected override void Start()
{
    base.Start();

    proximoAtaque = Time.time + 0.5f;
}
// o tempo de 0.5f é para dar um tempinho pro inimigo começar a atacar, evitando que ele ataque imediatamente ao ser invocado
protected override void Comportamento()
    {
        if (player == null) return;

        if (distanciaPlayer <= distanciaAtaque && Time.time >= proximoAtaque)
        {
            Atacar();
            proximoAtaque = Time.time + tempoEntreAtaques;
        }
    }

    void Atacar()
    {
        if (vidaDoPlayer != null)
        {
            vidaDoPlayer.DarDanoPlayer(dano);
        }
    }

protected override Vector2 DirecaoBase()
    {
        if (distanciaPlayer > distanciaAtaque)
        {
            return (player.position - transform.position).normalized; // persegue
        }

        return Vector2.zero; // para pra atacar
    }

private void OnDestroy()
    {
        // avisa o invocador que morreu
        if (invocador != null)
        {
            invocador.InimigoMorreu();
        }
    }
}