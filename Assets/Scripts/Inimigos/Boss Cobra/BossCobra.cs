using System;
using System.Collections.Generic;
using UnityEngine;

public class BossCobra : InimigoBase
{
    [Header("Referencias")]
    [SerializeField] private List<GameObject> segmentos;
    [SerializeField] private List<Transform> posPonto;
    [SerializeField] private Transform pontoAtual;
    [SerializeField] private int refPontoAtual = 0;
    [SerializeField] public BossState currentState = BossState.Idle;
    [SerializeField] private GameObject bolaDeFogo;
    [SerializeField] private float velocidadeProjetil = 7f;


    public float distanciaAtaque = 1.5f;
    public float tempoEntreAtaques = 3f;
    public int dano = 2;
    public int danoBolaDeFogo = 1;
    private float proximoAtaque;

    public float velocidadeBoss = 1f;

    protected override void Start()
    {
        pontoAtual = posPonto[0];
        base.Start();

        proximoAtaque = Time.time + 0.5f;

        velocidade = velocidadeBoss;
    }

    private void Update()
    {
        Vector2 direcao = (pontoAtual.position - transform.position).normalized;

        transform.position += (Vector3)direcao * velocidadeBoss * Time.deltaTime;
        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angulo);
        Comportamento();

        if(Vector2.Distance(transform.position, pontoAtual.position) < 0.2f)
        {
            DefinirProximoLocal();
        }
    }

     void OnCollisionEnter2D(Collision2D alvo)
    {
        if (alvo.gameObject.CompareTag("Player"))
        {
            PlayerVida playerVida = alvo.gameObject.GetComponent<PlayerVida>();

            playerVida.DarDanoPlayer(dano);
        }
    }



    protected override void Comportamento()
    {
        if (player == null) return;

        
        if(vida.morreu) return;

        if (Time.time >= proximoAtaque)
        {
            EscolherAtaque();

            proximoAtaque = Time.time + tempoEntreAtaques;
        }
    }


    public void DefinirProximoLocal()
    {
       refPontoAtual++;
       velocidadeBoss = 5f;
       
        if(refPontoAtual >= posPonto.Count)
        {
            refPontoAtual = 0;
            velocidadeBoss = 30f;
            
        } 
       pontoAtual = posPonto[refPontoAtual];
    }

  
    public void TrocarEstado(BossState novoEstado)
    {
        currentState = novoEstado;
    }

    void EscolherAtaque()
    {
        int ataque = UnityEngine.Random.Range(0, 2);

        switch (ataque)
        {
            case 0:
                AtaqueBolasDeFogo();
                break;

            case 1:
                Debug.Log("ataque 2");
                break;
        }
    }

    void AtaqueBolasDeFogo()
    {
        Vector2 direcao = (player.position - transform.position).normalized;

        Vector2 spawnPos = (Vector2)transform.position + direcao * 0.6f;

        GameObject tiro = Instantiate(bolaDeFogo, spawnPos, Quaternion.identity);

        Collider2D colProjetil = tiro.GetComponent<Collider2D>();
        Collider2D colInimigo = GetComponent<Collider2D>();

        if (colProjetil != null && colInimigo != null)
        {
            Physics2D.IgnoreCollision(colProjetil, colInimigo);
        }

        ProjetilInimgioExplosivo proj = tiro.GetComponent<ProjetilInimgioExplosivo>();

        if (proj != null)
        {
            proj.Inicializar(direcao, velocidadeProjetil, (int)(dano+RunManager.Instance.currentRun.inimigoBuffDano));
        }
        Debug.Log("Atirou");
    }

    
}