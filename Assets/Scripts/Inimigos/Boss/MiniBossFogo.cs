using UnityEngine;

public class MiniBossFogo : InimigoAtirador
{
    [Header("MiniBoss")]
    public float tempoEntreAtaques = 3f;
    public int quantidadeTiros = 5;
    public float anguloTotal = 60f;
    
    [SerializeField] private GameObject boomerangPrefab;


    private float proximoAtaque;

 

    protected override void Comportamento()
    {
        if (player == null) return;

        if (!TemLinhaDeVisao()) return;
        if(vida.morreu) return;

        if (Time.time >= proximoAtaque)
        {
            EscolherAtaque();

            proximoAtaque = Time.time + tempoEntreAtaques;
        }
    }

    void EscolherAtaque()
    {
        int ataque = Random.Range(0, 2);

        switch (ataque)
        {
            case 0:
                AtaqueBolasDeFogo();
                break;

            case 1:
                AtaqueBoomerang();
                break;
        }
    }

    void AtaqueBolasDeFogo()
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

            ProjetilInimgioExplosivo p = proj.GetComponent<ProjetilInimgioExplosivo>();

            if (p != null)
            {
                p.Inicializar(direcaoRotacionada, velocidadeProjetil, (int)(dano+RunManager.Instance.currentRun.inimigoBuffDano));
            }
        }
        Debug.Log("ATIROU");
    }

    void AtaqueBoomerang()
    {
        GameObject obj = Instantiate(
        boomerangPrefab,
        pontoDisparo.position,
        Quaternion.identity
    );

    ProjetilInimigoBoomerang boom = obj.GetComponent<ProjetilInimigoBoomerang>();

    boom.Inicializar((player.position - pontoDisparo.position).normalized, transform);
    }
}